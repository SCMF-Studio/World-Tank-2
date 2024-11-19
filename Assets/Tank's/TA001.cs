using System.Collections;
using UnityEngine;

public class TA001 : MonoBehaviour
{
    public float hp = 70f;
    public float maxHP = 70f;
    public float currentHP;
    public float damage = 10f;
    public float speed = 1.5f;
    public float rotationSpeed = 130f;
    public float turnSpeed = 130f;
    public float armoSpeed = 6f;
    public float reloading = 2f;
    public float ReloadTime { get { return reloading; } }

    private Transform muzzleTransform; 
    private Transform shootPosOne, shootPosTwo; 
    [SerializeField] private GameObject bullet_standart; 

    private bool canShoot = true; 
    private bool isFirstBarrel = true; 

    private Rigidbody2D rb; 

    public ParticleSystem particleDownOne, particleDownTwo, particleUpOne, particleUpTwo; 
    public delegate void ReloadStartedHandler(float reloadTime);
    public event ReloadStartedHandler OnReloadStarted;



    // Boost System
    private float originalSpeed, originalHeal, originalSpeedArmo, originalReloading, originalAdditionalHP,
       originalSmallDamage,
       originalMediumDamage,
       originalHighDamage;


    void Start()
    {
        currentHP = maxHP;
        muzzleTransform = GameObject.Find("TA-001_muzzle").transform;
        shootPosOne = GameObject.Find("ShootPosOne").transform;
        shootPosTwo = GameObject.Find("ShootPosTwo").transform;
        rb = GetComponent<Rigidbody2D>();


        // Boost System
        originalSpeed = speed;
        originalHeal = hp;
        originalSmallDamage = damage;
        originalMediumDamage = damage;
        originalHighDamage = damage;
        originalSpeedArmo = armoSpeed;
        originalReloading = reloading;
        originalAdditionalHP = maxHP;
    }

    void Update()
    {
        MoveTank();
        RotateTurret();
        ParcticleSysteme();

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void MoveTank()
    {

        float MoveVerticalInput = Input.GetAxis("Vertical");
        float MoveHorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            MoveVerticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveVerticalInput = -1f;
        }
        else
        {
            MoveVerticalInput = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontalInput = 1f;
        }
        else
        {
            MoveHorizontalInput = 0f;
        }

        transform.Translate(Vector3.up * MoveVerticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, MoveHorizontalInput * turnSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        if (currentHP <= 0)
        {
            Die();
        }
        UpdateHUD();
    }


    private void Die()
    {
        Debug.Log("Танк уничтожен!");
        Destroy(gameObject);
    }

    private void UpdateHUD()
    {
        if (FindObjectOfType<HudBar>() != null)
        {
            HudBar hudBar = FindObjectOfType<HudBar>();
            hudBar.UpdateHPBar(currentHP, maxHP);
        }
    }

    public void SetMaxHP(float newMaxHP)
    {
        maxHP = newMaxHP;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateHUD();
    }

    public void SetCurrentHP(float newCurrentHP)
    {
        currentHP = Mathf.Clamp(newCurrentHP, 0, maxHP);

        UpdateHUD();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10f);
        }
    }

    void RotateTurret()
    {
        float rotationInput = 0f;
        if (Input.GetMouseButton(0))
        {
            rotationInput = 1f;
        }
        else if (Input.GetMouseButton(1))
        {
            rotationInput = -1f;
        }

        muzzleTransform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        if (canShoot)
        {
            canShoot = false;

            if (isFirstBarrel)
            {
                GameObject newRocketOne = Instantiate(bullet_standart, shootPosOne.position, muzzleTransform.rotation);
                Rigidbody2D rocketRbOne = newRocketOne.GetComponent<Rigidbody2D>();
                rocketRbOne.velocity = muzzleTransform.up * armoSpeed;

                var bulletHandler = newRocketOne.GetComponent<BulletDamageHandler>();
                if (bulletHandler != null)
                {
                    bulletHandler.Initialize(gameObject);
                }
            }
            else
            {
                GameObject newRocketTwo = Instantiate(bullet_standart, shootPosTwo.position, muzzleTransform.rotation);
                Rigidbody2D rocketRbTwo = newRocketTwo.GetComponent<Rigidbody2D>();
                rocketRbTwo.velocity = muzzleTransform.up * armoSpeed;

                var bulletHandler = newRocketTwo.GetComponent<BulletDamageHandler>();
                if (bulletHandler != null)
                {
                    bulletHandler.Initialize(gameObject);
                }
            }

            isFirstBarrel = !isFirstBarrel;

           

            if (isFirstBarrel)
            {
                OnReloadStarted?.Invoke(reloading);
                yield return new WaitForSeconds(reloading);
            }

            canShoot = true;
        }
    }

    void ParcticleSysteme()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!particleDownOne.isEmitting)
            {
                particleDownOne.Play();
                particleDownTwo.Play();
            }
        }
        else
        {
            if (particleDownOne.isEmitting)
            {
                particleDownOne.Stop();
                particleDownTwo.Stop();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!particleUpOne.isEmitting)
            {
                particleUpOne.Play();
                particleUpTwo.Play();
            }
        }
        else
        {
            if (particleUpOne.isEmitting)
            {
                particleUpOne.Stop();
                particleUpTwo.Stop();
            }
        }
    }

    // Boost System
    public void ApplySpeedBoost(float duration)
    {
        StopCoroutine("SpeedBoostCoroutine");
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        speed *= 1.50f;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    public void ApplyHealBoost()
    {
        float healAmount = maxHP * 0.25f;
        currentHP += healAmount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHUD();
    }

    public void ApplySmallDamageBoost(float duration)
    {
        StopCoroutine("SmallDamageBoostCoroutine");
        StartCoroutine(SmallDamageBoostCoroutine(duration));
    }

    private IEnumerator SmallDamageBoostCoroutine(float duration)
    {
        damage *= 1.30f;
        yield return new WaitForSeconds(duration);
        damage = originalSmallDamage;
    }

    public void ApplyMediumDamageBoost(float duration)
    {
        StopCoroutine("MediumDamageBoostCoroutine");
        StartCoroutine(MediumDamageBoostCoroutine(duration));
    }

    private IEnumerator MediumDamageBoostCoroutine(float duration)
    {
        damage *= 1.70f;
        yield return new WaitForSeconds(duration);
        damage = originalMediumDamage;
    }

    public void ApplyHighDamageBoost(float duration)
    {
        StopCoroutine("HighDamageBoostCoroutine");
        StartCoroutine(HighDamageBoostCoroutine(duration));
    }

    private IEnumerator HighDamageBoostCoroutine(float duration)
    {
        damage *= 3.00f;
        yield return new WaitForSeconds(duration);
        damage = originalHighDamage;
    }

    public void ApplySpeedArmoBoost(float duration)
    {
        StopCoroutine("SpeedArmoBoostCoroutine");
        StartCoroutine(SpeedArmoBoostCoroutine(duration));
    }

    private IEnumerator SpeedArmoBoostCoroutine(float duration)
    {
        armoSpeed *= 1.70f;
        reloading /= 1.80f;

        HudBar hudBar = FindObjectOfType<HudBar>();
        if (hudBar != null)
        {
            hudBar.SetReloadTime(reloading);
            hudBar.StartReload();
        }

        yield return new WaitForSeconds(duration);

        armoSpeed = originalSpeedArmo;
        reloading = originalReloading;

        if (hudBar != null)
        {
            hudBar.SetReloadTime(reloading);
        }
    }

    public void ApplyAdditionalHPBoost(float duration)
    {
        StopCoroutine("AdditionalHPBoostCoroutine");
        StartCoroutine(AdditionalHPBoostCoroutine(duration));
    }

    private IEnumerator AdditionalHPBoostCoroutine(float duration)
    {
        maxHP += 50f;
        currentHP = maxHP;
        UpdateHUD();

        yield return new WaitForSeconds(duration);

        maxHP = originalAdditionalHP;


        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHUD();
    }
}
