using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            Debug.Log("Missile hit the enemy");
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
