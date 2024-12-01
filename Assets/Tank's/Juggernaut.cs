using System;
using System.Collections;
using UnityEngine;

public class Juggernaut : MonoBehaviour
{
    public float speed = 2.4f;
    public float rotationSpeed = 130f;
    public float turnSpeed = 150f;

    private Transform muzzleTransform;
    private Rigidbody2D rb;

    void Start()
    {
        muzzleTransform = GameObject.Find("Juggernaut_muzzle").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        MoveTank();
        RotateTurret();

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

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity *= 0.5f;
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




}
