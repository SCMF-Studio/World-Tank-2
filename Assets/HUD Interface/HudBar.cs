using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBar : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image content;
    void Start()
    {

    }

    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
       // content.fillAmount = fillAmount;
    }
}
