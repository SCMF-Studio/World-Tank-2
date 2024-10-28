using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;
using static UnityEditor.PlayerSettings;
using System.Text.RegularExpressions;

public class HudBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float fillAmount;
    [SerializeField] private float fillHP;
    [SerializeField] public Image contentHp;
    [SerializeField] public Image reload;
    public InputField a_password;
    public RectTransform a_list;

    private float reloadTime = 2f;
    private float currentReloadTime = 0f;
    private bool isReloading = false;

    private float animationDuration = 1f;

    public TextMeshProUGUI time_game;
    // 300f - 5 минут
    public float remainingTime = 300f;
    private BoxScript boxScript;

    private int greenNumBox;
    private float timeFallBox;
    System.Random rnd = new System.Random();
    



    void Start()
    {
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

        boxScript = FindObjectOfType<BoxScript>();
        UpdateTimeText();
        StartCoroutine(TimerCountdown());
    }

    void Update()
    {
        if (isReloading)
        {
            HandleReload();
        }
    }



    public void UpdateHPBar(float currentHP, float maxHP)
    {
        contentHp.fillAmount = Mathf.Clamp01(currentHP / maxHP);  
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

    public void SetReloadTime(float newReloadTime)
    {
        reloadTime = newReloadTime;

        StartReload();
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

    // Time Game and Spawn Box
    public IEnumerator TimerCountdown()
    {
        int spawnStage = 1;

        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;

            UpdateTimeText();

            if (remainingTime == 298 && spawnStage == 1)
            {
                timeFallBox = rnd.Next(290, 298);
                Debug.Log("Время прибытия бокса 1: " + timeFallBox);
                spawnStage++;
            }
            else if (remainingTime == 290 && spawnStage == 2)
            {
                timeFallBox = rnd.Next(280, 290);
                Debug.Log("Время прибытия бокса 2: " + timeFallBox);
                spawnStage++;
            }
            else if (remainingTime == 200 && spawnStage == 3)
            {
                timeFallBox = rnd.Next(190, 200);
                Debug.Log("Время прибытия бокса 3: " + timeFallBox);
                spawnStage++;
            }
            else if (remainingTime == 150 && spawnStage == 4)
            {
                timeFallBox = rnd.Next(140, 150);
                Debug.Log("Время прибытия бокса 4: " + timeFallBox);
                spawnStage++;
            }

            if (Mathf.Approximately(remainingTime, timeFallBox))
            {
                if (boxScript != null)
                {
                    int minBoxCount, maxBoxCount;

                    switch (spawnStage)
                    {
                        case 2:
                            minBoxCount = 1;
                            maxBoxCount = 4;
                            break;
                        case 3:
                            minBoxCount = 5;
                            maxBoxCount = 8;
                            break;
                        case 4:
                            minBoxCount = 2;
                            maxBoxCount = 5;
                            break;
                        default:
                            minBoxCount = 3;
                            maxBoxCount = 6;
                            break;
                    }

                    greenNumBox = rnd.Next(minBoxCount, maxBoxCount);
                    Debug.Log($"Ящиков выпало на этапе {spawnStage - 1}: " + greenNumBox);
                    boxScript.SpawnMultipleGreenBoxes(greenNumBox);
                }
            }

            if (remainingTime == 100)
            {
                if (boxScript != null)
                {
                    boxScript.SpawnMultipleYellowBoxes(2);
                }
            }

            if (remainingTime == 50)
            {
                if (boxScript != null)
                {
                    boxScript.SpawnMultipleRedBoxes(2);
                }
            }

            if (remainingTime <= 0)
            {
                Debug.Log("Время вышло!");
                break;
            }
        }
    }


    public void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        time_game.text = $"{minutes:D2}:{seconds:D2}";
    }


    private void CheckPassword(string input)
    {
        if (input == "ad1")
        {
            StartCoroutine(AnimateScrollDown()); 
        }
        else if (input == "ad0")
        {
            StartCoroutine(AnimateScrollUp());  
        }
        else
        {
            Debug.Log("Админ консоль выдает ошибку");
        }

        a_password.text = "";
    }
}
