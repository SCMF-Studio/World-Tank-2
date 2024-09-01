using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS001 : MonoBehaviour
{
    float xp = 100f;
    float damage = 10f;
    float speed = 2f;
    private float rotationSpeed = 200f;
    float turnSpeed = 200f;
    float armoSpeed = 5f;
    float reloading = 2f;
    public float ReloadTime { get { return reloading; } }
    private Transform muzzleTransform;
    private Transform shootPos;
    [SerializeField] private GameObject bullet_standart;
    private bool canShoot = true;
    private Rigidbody2D rb;
    public ParticleSystem particleDownOne, particleDownTwo, particleUpOne, particleUpTwo;

    void Start()
    {
        muzzleTransform = GameObject.Find("TS-001_muzzle").transform;
        shootPos = GameObject.Find("ShootPos").transform;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        MoveTank();
        RotateTurret();
        ParcticleSysteme();

        // Shooting
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    void MoveTank()
    {
        // Controlling the body tank using keys
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity = Vector3.zero;
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

        // Поворачиваем дуло на основе ввода
        muzzleTransform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject newRocket = Instantiate(bullet_standart, shootPos.position, muzzleTransform.rotation);
            Rigidbody2D rocketRb = newRocket.GetComponent<Rigidbody2D>();
            rocketRb.velocity = muzzleTransform.right * armoSpeed;
            yield return new WaitForSeconds(reloading);
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
            if (!particleDownOne.isEmitting)
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
            if (!particleUpOne.isEmitting)
            {
                particleUpOne.Stop();
                particleUpTwo.Stop();

            }
        }
    }
}
