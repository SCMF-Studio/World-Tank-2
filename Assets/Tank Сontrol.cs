using System.Collections;
using System.Collections.Generic;
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
    float reloading = 2f;

  

    void Start()
    {
        muzzleTransform = GameObject.Find("TS-001_muzzle").transform;
    }

    
    void Update()
    {
        MoveTank();
        RotateTurret();
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

        transform.Translate(Vector3.up *MoveVerticalInput * speed * Time.deltaTime); 
        transform.Rotate(Vector3.forward, MoveHorizontalInput * turnSpeed * Time.deltaTime);

       
    }

    void RotateTurret()
    {
        // Controlling the muzzle tank with the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        muzzleTransform.rotation = Quaternion.Slerp(muzzleTransform.rotation, rotation, rotationMuzzle * Time.deltaTime);
    }

}
