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

    void Start()
    {
        currentHP = maxHP;
        muzzleTransform = GameObject.Find("TA-001_muzzle").transform;
        shootPosOne = GameObject.Find("ShootPosOne").transform;
        shootPosTwo = GameObject.Find("ShootPosTwo").transform;
        rb = GetComponent<Rigidbody2D>();
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
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
            }
            else
            {
                GameObject newRocketTwo = Instantiate(bullet_standart, shootPosTwo.position, muzzleTransform.rotation);
                Rigidbody2D rocketRbTwo = newRocketTwo.GetComponent<Rigidbody2D>();
                rocketRbTwo.velocity = muzzleTransform.up * armoSpeed;
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
}
