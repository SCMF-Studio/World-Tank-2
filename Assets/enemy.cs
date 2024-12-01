using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float currentHP = 1000f;
    public float damage = 10f;
    public float speed = 1.5f;
    public float armoSpeed = 6f;
    public float reloading = 2f;
    public float moveDuration = 2f; 

    private Transform muzzleTransform;
    private Transform shootPosOne, shootPosTwo;
    [SerializeField] private GameObject bullet_standart;

    private bool canShoot = true;
    private bool movingForward = true; 
    private float moveTimer = 0f; 

    private Rigidbody2D rb;

    void Start()
    {
        muzzleTransform = GameObject.Find("TA-001_muzzle Enemy").transform;
        shootPosOne = GameObject.Find("ShootPosOne Enemy").transform;
        shootPosTwo = GameObject.Find("ShootPosTwo Enemy").transform;
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(AutoShoot());
    }

    void Update()
    {
        MoveTank();
    }

    private void MoveTank()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration)
        {
            movingForward = !movingForward;
            moveTimer = 0f;
        }

        float moveDirection = movingForward ? 1f : -1f;
        transform.Translate(Vector3.up * moveDirection * speed * Time.deltaTime);
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            if (canShoot)
            {
                canShoot = false;
                Shoot();
                yield return new WaitForSeconds(reloading);
                canShoot = true;
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        Debug.Log("Враг получил урон: " + damageAmount + ", текущее HP: " + currentHP); 
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Shoot()
    {
        GameObject newRocket = Instantiate(bullet_standart, shootPosOne.position, muzzleTransform.rotation);
        Rigidbody2D rocketRb = newRocket.GetComponent<Rigidbody2D>();
        rocketRb.velocity = muzzleTransform.up * armoSpeed;

        var bulletHandler = newRocket.GetComponent<BulletDamageHandler>();
        if (bulletHandler != null)
        {
            bulletHandler.Initialize(gameObject); 
        }
    }
   

    private void Die()
    {
        Debug.Log("Враг уничтожен!");
        Destroy(gameObject);
    }


    private Coroutine speedReductionCoroutine;

    public void ApplySpeedReduction(float reductionFactor, float duration)
    {
        if (speedReductionCoroutine != null)
        {
            StopCoroutine(speedReductionCoroutine);
        }
        speedReductionCoroutine = StartCoroutine(SpeedReductionCoroutine(reductionFactor, duration));
    }

    private IEnumerator SpeedReductionCoroutine(float reductionFactor, float duration)
    {
        float originalSpeed = speed;
        speed *= reductionFactor;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        speedReductionCoroutine = null;
    }



}
