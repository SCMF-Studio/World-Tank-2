using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class HudBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image contentHp;
    [SerializeField] private Image reload;
    public InputField a_password;
    public RectTransform a_list;  

    private float reloadTime = 2f;
    private float currentReloadTime = 0f;
    private bool isReloading = false;

    private float animationDuration = 1f; 

    public void SetReloadTime(float newReloadTime)
    {
        reloadTime = newReloadTime;
    }

    void Start()
    {
        HandleBar();
        HideObject();

        if (a_password != null)
        {
            a_password.onEndEdit.AddListener(CheckPassword);
        }

        if (a_list != null)
        {
            a_list.pivot = new Vector2(0.5f, 1f); 
            a_list.localScale = new Vector3(1, 0, 1); 
            a_list.gameObject.SetActive(false);  
        }
    }

    void Update()
    {
        HandleBar();

        if (isReloading)
        {
            HandleReload();
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

    public void StartReload()
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
        reload.fillAmount = 1f;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void ShowObject()
    {
        if (a_password != null)
        {
            a_password.gameObject.SetActive(true);
        }
    }

    public void HideObject()
    {
        if (a_password != null)
        {
            a_password.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowObject();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideObject();
    }

    private IEnumerator AnimateScrollDown()
    {
        float elapsedTime = 0f;
        a_list.gameObject.SetActive(true);  

        while (elapsedTime < animationDuration)
        {
            float newScaleY = Mathf.Lerp(0, 1, elapsedTime / animationDuration); 
            a_list.localScale = new Vector3(1, newScaleY, 1); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        a_list.localScale = new Vector3(1, 1, 1);  
    }

    private IEnumerator AnimateScrollUp()
    {
        float elapsedTime = 0f;
        Vector3 currentScale = a_list.localScale;  

        while (elapsedTime < animationDuration)
        {
            float newScaleY = Mathf.Lerp(currentScale.y, 0, elapsedTime / animationDuration);  
            a_list.localScale = new Vector3(1, newScaleY, 1);  
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        a_list.localScale = new Vector3(1, 0, 1);  
        a_list.gameObject.SetActive(false);  
    }

    private void CheckPassword(string input)
    {
        if (input == "ad1")
        {
            Debug.Log("Админ-режим активирован!");
            StartCoroutine(AnimateScrollDown()); 
        }
        else if (input == "ad0")
        {
            Debug.Log("Админ-режим деактивирован!");
            StartCoroutine(AnimateScrollUp());  
        }
        else
        {
            Debug.Log("Введена неверная команда.");
        }

        a_password.text = "";
    }
}
