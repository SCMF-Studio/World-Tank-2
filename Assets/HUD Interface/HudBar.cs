using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBar : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image contentHp;

    [SerializeField] private Image reload;
    void Start()
    {
        
    }

    
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        contentHp.fillAmount = Map(100,0,100,0,1);
        reload.fillAmount = Map(100, 0, 100, 0, 1);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
