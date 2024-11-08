using UnityEngine;

public class RicochetBullet : MonoBehaviour
{
    public float damage_bullet;
    public int maxRicochets = 3;
    private int ricochetCount = 0;
    private GameObject shooter;
    private Rigidbody2D rb;
    private float initialSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialSpeed = rb.velocity.magnitude; 
    }

    public void Initialize(GameObject tankShooter)
    {
        shooter = tankShooter;
        if (shooter.GetComponent<TS001>() != null)
        {
            damage_bullet = shooter.GetComponent<TS001>().damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            var target = collision.gameObject.GetComponent<TS001>();
            if (target != null) target.TakeDamage(damage_bullet);

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (ricochetCount < maxRicochets)
            {
                ricochetCount++;
                Vector2 reflectDir = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
                rb.velocity = reflectDir * initialSpeed;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
