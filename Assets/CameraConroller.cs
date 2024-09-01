using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform target;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
