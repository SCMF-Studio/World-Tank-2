using System.Collections;
using UnityEngine;

public class TA001 : MonoBehaviour
{
    float xp = 70f; // Жизни
    float damage = 10f; // Урон
    float speed = 1.5f; // Скорость танка
    float turnSpeed = 130f; // Скорость вращение танка по оси
    private float rotationSpeed = 130f; // Скорость вращение дула
    float armoSpeed = 6f; // Скорость патрона
    float reloading = 2f; // Перезарядка
    public float ReloadTime { get { return reloading; } }

    private Transform muzzleTransform; // Трансформ дула (мушка)
    private Transform shootPosOne, shootPosTwo; // Позиции для стрельбы
    [SerializeField] private GameObject bullet_standart; // Префаб снаряда

    private bool canShoot = true; // Флаг для проверки возможности стрельбы
    private bool isFirstBarrel = true; // Флаг для отслеживания текущего ствола

    private Rigidbody2D rb; // Rigidbody танка для физики

    public ParticleSystem particleDownOne, particleDownTwo, particleUpOne, particleUpTwo; // Частицы для движения

    public delegate void ReloadStartedHandler(float reloadTime);
    public event ReloadStartedHandler OnReloadStarted;

    void Start()
    {
        // Инициализация трансформов и Rigidbody
        muzzleTransform = GameObject.Find("TA-001_muzzle").transform;
        shootPosOne = GameObject.Find("ShootPosOne").transform;
        shootPosTwo = GameObject.Find("ShootPosTwo").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Обработка движения танка и вращения дула
        MoveTank();
        RotateTurret();
        ParcticleSysteme();

        // Обработка стрельбы
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void MoveTank()
    {
        // Управление движением танка по вертикали и горизонтали
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

        // Движение и вращение танка
        transform.Translate(Vector3.up * MoveVerticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, MoveHorizontalInput * turnSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Обработка столкновений
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    void RotateTurret()
    {
        // Управление вращением дула по мыши
        float rotationInput = 0f;
        if (Input.GetMouseButton(0))
        {
            rotationInput = 1f;
        }
        else if (Input.GetMouseButton(1))
        {
            rotationInput = -1f;
        }

        // Вращение дула
        muzzleTransform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        if (canShoot)
        {
            canShoot = false;

            // Стреляем из соответствующей позиции
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

            // Переключаем ствол
            isFirstBarrel = !isFirstBarrel;

            // Если оба ствола отстрелялись, начинается перезарядка
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
        // Управление частицами при движении вперёд
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

        // Управление частицами при движении назад
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
