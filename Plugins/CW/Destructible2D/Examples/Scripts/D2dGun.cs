using UnityEngine;
using CW.Common;

namespace Destructible2D.Examples
{
	/// <summary>This component implements a basic 2D gun that spawns a prefab where you click on the screen.</summary>
	[HelpURL(D2dCommon.HelpUrlPrefix + "D2dGun")]
	[AddComponentMenu(D2dCommon.ComponentMenuPrefix + "Gun")]
	public class D2dGun : MonoBehaviour
	{
		/// <summary>Minimum time between each shot in seconds.</summary>
		public float ShootDelay = 0.1f;

		/// <summary>The bullet prefab spawned when shooting.</summary>
		public GameObject BulletPrefab;

		public GameObject DummyBulletPrefab;

		/// <summary>The muzzle prefab spawned on the gun when shooting.</summary>
		public GameObject MuzzleFlashPrefab;

		// Seconds until next shot is available
		[SerializeField]
		private float cooldown;

		public bool CanShoot
		{
			get
			{
				return cooldown <= 0.0f;
			}
		}

		public void Shoot()
		{
			if (cooldown <= 0.0f)
			{
				cooldown = ShootDelay;

				if (BulletPrefab != null && DummyBulletPrefab != null)
				{
					// Создаем обе пули
					GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
					GameObject dummyBullet = Instantiate(DummyBulletPrefab, transform.position, transform.rotation);

					// Связываем их между собой
					D2dBullet bulletScript = bullet.GetComponent<D2dBullet>();
					MissileController missileScript = dummyBullet.GetComponent<MissileController>();

					if (bulletScript != null && missileScript != null)
					{
						bulletScript.pairedBullet = dummyBullet;
						missileScript.pairedBullet = bullet;
					}
				}

				if (MuzzleFlashPrefab != null)
				{
					Instantiate(MuzzleFlashPrefab, transform.position, transform.rotation);
				}
			}
		}

		protected virtual void Update()
		{
			cooldown -= Time.deltaTime;
		}
	}
}

#if UNITY_EDITOR
namespace Destructible2D.Examples
{
	using UnityEditor;
	using TARGET = D2dGun;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class D2dGun_Editor : CwEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			BeginError(Any(tgts, t => t.ShootDelay < 0.0f));
				Draw("ShootDelay", "Minimum time between each shot in seconds.");
			EndError();
			Draw("BulletPrefab", "The bullet prefab spawned when shooting.");
			Draw("DummyBulletPrefab", "The dummy bullet prefab spawned when shooting."); // Добавьте эту строку
			Draw("MuzzleFlashPrefab", "The muzzle prefab spawned on the gun when shooting.");
		}
	}
}
#endif