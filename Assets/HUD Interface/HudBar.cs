using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HudBar : MonoBehaviour
{
    // Hud Player
    [SerializeField] private float fillAmount;
    [SerializeField] private Image contentHp;
    [SerializeField] private Image reload;

    [SerializeField] public Image a_password;

    private float reloadTime = 2f;
    private float currentReloadTime = 0f;
    private bool isReloading = false;

    public void SetReloadTime(float newReloadTime)
    {
        reloadTime = newReloadTime;
    }

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
            a_password.enabled = true;
        }
    }

    public void HideObject()
    {
        if (a_password != null)
        {
            a_password.enabled = false;
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

}
