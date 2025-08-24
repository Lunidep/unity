using UnityEngine;
using CW.Common;

namespace Destructible2D.Examples
{
	/// <summary>This component turns the current GameObject into a 2D bullet that moves and collides with the world.</summary>
	[ExecuteInEditMode]
	[HelpURL(D2dCommon.HelpUrlPrefix + "D2dBullet")]
	[AddComponentMenu(D2dCommon.ComponentMenuPrefix + "Bullet")]
	public class D2dBullet : MonoBehaviour
	{
		/// <summary>The tag this bullet cannot hit.</summary>
		public string IgnoreTag;

		/// <summary>The layers this bullet can hit.</summary>
		public LayerMask RaycastMask = -1;

		/// <summary>The prefab that gets spawned when this bullet hits something.</summary>
		public GameObject ExplosionPrefab;

		/// <summary>The distance this bullet moves each second.</summary>
		public float Speed;

		public GameObject pairedBullet;

		private Vector3 oldPosition;

		protected virtual void Start()
		{
			oldPosition = transform.position;
		}

		// Модифицируйте метод FixedUpdate:
		protected virtual void FixedUpdate()
		{
			var newPosition = transform.position;
			var rayDirection = (newPosition - oldPosition).normalized;
			var rayLength = (newPosition - oldPosition).magnitude;

			var filteredMask = RaycastMask & ~(1 << LayerMask.NameToLayer("Missile"));

			var hit = Physics2D.Raycast(oldPosition, rayDirection, rayLength, filteredMask);

			if (hit.collider != null)
			{
				if (hit.collider.CompareTag("Missile") == false &&
					(string.IsNullOrEmpty(IgnoreTag) == true || hit.collider.tag != IgnoreTag))
				{
					if (ExplosionPrefab != null)
					{
						Instantiate(ExplosionPrefab, hit.point, Quaternion.identity);
					}

					// Уничтожаем парную пулю если она существует
					if (pairedBullet != null)
					{
						Destroy(pairedBullet);
					}
					Destroy(gameObject);
				}
			}

			oldPosition = newPosition;
		}

		// Добавьте метод OnDestroy:
		private void OnDestroy()
		{
			// При уничтожении также уничтожаем парную пулю
			if (pairedBullet != null)
			{
				Destroy(pairedBullet);
			}
		}

		protected virtual void Update()
		{
			transform.Translate(0.0f, Speed * Time.deltaTime, 0.0f);
		}
	}
}

#if UNITY_EDITOR
namespace Destructible2D.Examples
{
	using UnityEditor;
	using TARGET = D2dBullet;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class D2dBullet_Editor : CwEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			Draw("IgnoreTag", "The tag this bullet cannot hit.");
			Draw("RaycastMask", "The layers this bullet can hit.");
			Draw("ExplosionPrefab", "The prefab that gets spawned when this bullet hits something.");
			Draw("Speed", "The distance this bullet moves each second.");
			Draw("pairedBullet", "pairedBullet");
		}
	}
}
#endif