using UnityEngine;

namespace Destructible2D.Examples
{
    public class MissileController : MonoBehaviour
    {
        public float missileSpeed = 25f;
        public GameObject pairedBullet; // Ссылка на парную пулю
        public GameObject explosion;
        void Update()
        {
            transform.Translate(0.0f, missileSpeed * Time.deltaTime, 0.0f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                // Уничтожаем парную пулю если она существует
                if (pairedBullet != null)
                {
                    Destroy(pairedBullet);
                }
                GameObject gm = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gm, 2f);
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }

        private void OnDestroy()
        {
            // При уничтожении также уничтожаем парную пулю
            if (pairedBullet != null)
            {
                Destroy(pairedBullet);
            }
        }
    }
}