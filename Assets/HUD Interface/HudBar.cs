using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBar : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image contentHp;
    [SerializeField] private Image reload;

    private float reloadTime = 2f; 
    private float currentReloadTime = 0f; 
    private bool isReloading = false; 

    void Start()
    {
        HandleBar();
    }

    void Update()
    {
        HandleBar();

        if (isReloading)
        {
            HandleReload();
        } 
        if (Input.GetKey(KeyCode.Space))
        {
            StartReload();
        }
    }

    private void HandleBar()
    {
        contentHp.fillAmount = Map(100, 0, 100, 0, 1);
    }

    private void HandleReload()
    {
        currentReloadTime += Time.deltaTime;
        reload.fillAmount = Map(currentReloadTime, 0, reloadTime, 0, 1);

        if (currentReloadTime >= reloadTime)
        {
            FinishReload();
        }
    }

    private void StartReload()
    {
        if (!isReloading)
        {
            isReloading = true;
            currentReloadTime = 0f;
            reload.fillAmount = 0f; 
        }
    }

    private void FinishReload()
    {
        isReloading = false;
        currentReloadTime = 0f;
        reload.fillAmount = 2f; 
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
