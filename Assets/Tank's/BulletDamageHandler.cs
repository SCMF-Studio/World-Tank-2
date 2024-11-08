using UnityEngine;

public class BulletDamageHandler : MonoBehaviour
{
    public float damage_bullet; 
    private GameObject shooter;

    public void Initialize(GameObject tankShooter)
    {
        shooter = tankShooter;
        if (shooter.GetComponent<TS001>() != null)
        {
            damage_bullet = shooter.GetComponent<TS001>().damage;
        }
        else if (shooter.GetComponent<TH001>() != null)
        {
            damage_bullet = shooter.GetComponent<TH001>().damage;
        }
        else if (shooter.GetComponent<TA001>() != null)
        {
            damage_bullet = shooter.GetComponent<TA001>().damage;
        }
        else if (shooter.GetComponent<TL001>() != null)
        {
            damage_bullet = shooter.GetComponent<TL001>().damage;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            var targetTankTS = collision.GetComponent<TS001>();
            var targetTankTH = collision.GetComponent<TH001>();
            var targetTankTA = collision.GetComponent<TA001>();
            var targetTankTL = collision.GetComponent<TL001>();
            var targetEnemy = collision.GetComponent<enemy>();

            if (targetTankTS != null)
            {
                targetTankTS.TakeDamage(damage_bullet);
            }
            else if (targetTankTH != null)
            {
                targetTankTH.TakeDamage(damage_bullet);
            }
            else if (targetTankTA != null)
            {
                targetTankTA.TakeDamage(damage_bullet);
            }
            else if (targetTankTL != null)
            {
                targetTankTL.TakeDamage(damage_bullet); 
            }
            else if (targetEnemy != null)
            {
                targetEnemy.TakeDamage(damage_bullet);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }
}
