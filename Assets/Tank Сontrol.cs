using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TankСontrol : MonoBehaviour
{

    float xp = 100f;
    float damage = 10f;
    float speed = 2f;
    float turnSpeed = 200f;
    float rotationMuzzle = 5f;
    private Transform muzzleTransform;
    float armoSpeed = 5f;
    private Transform shootPos;
    [SerializeField] private GameObject bullet_standart;
    private bool canShoot = true;
    float reloading = 2f;

    private Rigidbody rb;


    void Start()
    {
        muzzleTransform = GameObject.Find("TS-001_muzzle").transform;
        shootPos = GameObject.Find("ShootPos").transform;
        rb = GetComponent<Rigidbody>();

    }

    
    void Update()
    {
        MoveTank();
        RotateTurret();

        // Shooting
        if (Input.GetMouseButton(0))
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
            Debug.Log("sad"); Debug.Log("sad");
        }
    }


    void RotateTurret()
    {
        // Controlling the muzzle tank with the mouse

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        muzzleTransform.rotation = Quaternion.Slerp(muzzleTransform.rotation, rotation, rotationMuzzle * Time.deltaTime);

        // Rotation shootPos for mouse and muzzle
        mousePos.z = 0f; 
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        shootPos.rotation = Quaternion.Slerp(shootPos.rotation, targetRotation, rotationMuzzle * Time.deltaTime);

    }

    private IEnumerator Shoot()
    {

        // Shooting
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

    
}
