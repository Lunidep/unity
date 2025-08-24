using UnityEngine;

public class SuperEnemyController : MonoBehaviour
{
    public float speed;
    public GameObject fragmentPrefab;
    private int fragmentsCount = 3;
    private float fragmentSpeedMultiplier = 4;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            Debug.Log("Missile hit the super enemy");
            CreateFragments();
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    void CreateFragments()
    {
        for (int i = 0; i < fragmentsCount; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

            Vector2 randomDirection = new Vector2(
                Random.Range(-0.2f, 0.2f),
                -1f
            ).normalized;

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = randomDirection * speed * fragmentSpeedMultiplier;
            }
            else
            {
                fragment.GetComponent<EnemyController>().speed = speed * fragmentSpeedMultiplier;
            }
        }
    }
}