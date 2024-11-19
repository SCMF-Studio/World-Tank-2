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

    private int greenNumBox, yellowNumBox, redNumBox;
    private float timeFallGreenBox, timeFallYellowBox, timeFallRedBox;
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

        if (isReloading)
        {
            currentReloadTime = 0f; 
            reload.fillAmount = 0f; 
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

    // Time Game and Spawn Box
    public IEnumerator TimerCountdown()
    {
        int spawnStageGreen = 1;
        int spawnStageYellow = 1;
        int spawnStageRed = 1;

        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;

            UpdateTimeText();

            // Spawn Green Box

            if (remainingTime == 298 && spawnStageGreen == 1)
            {
                timeFallGreenBox = rnd.Next(250, 290);
                Debug.Log("<color=green>Время прибытия Green бокса 1 волна:</color> " + timeFallGreenBox);
                spawnStageGreen++;
            }
            else if (remainingTime == 250 && spawnStageGreen == 2)
            {
                timeFallGreenBox = rnd.Next(200, 230);
                Debug.Log("<color=green>Время прибытия Green бокса 2 волна:</color> " + timeFallGreenBox);
                spawnStageGreen++;
            }
            else if (remainingTime == 199 && spawnStageGreen == 3)
            {
                timeFallGreenBox = rnd.Next(120, 170);
                Debug.Log("<color=green>Время прибытия Green бокса 3 волна</color> : " + timeFallGreenBox);
                spawnStageGreen++;
            }
            else if (remainingTime == 100 && spawnStageGreen == 4)
            {
                timeFallGreenBox = rnd.Next(50, 90);
                Debug.Log("<color=green>Время прибытия Green бокса 4 волна:</color> " + timeFallGreenBox);
                spawnStageGreen++;
            }

            if (Mathf.Approximately(remainingTime, timeFallGreenBox))
            {
                if (boxScript != null)
                {
                    int minBoxCount, maxBoxCount;

                    switch (spawnStageGreen)
                    {
                        case 2:
                            minBoxCount = 3;
                            maxBoxCount = 5;
                            break;
                        case 3:
                            minBoxCount = 2;
                            maxBoxCount = 8;
                            break;
                        case 4:
                            minBoxCount = 2;
                            maxBoxCount = 10;
                            break;
                        default:
                            minBoxCount = 2;
                            maxBoxCount = 4;
                            break;
                    }

                    greenNumBox = rnd.Next(minBoxCount, maxBoxCount);
                    Debug.Log($"<color=green>Green Ящиков выпало на этапе {spawnStageGreen - 1}: {greenNumBox}</color>");
                    boxScript.SpawnMultipleGreenBoxes(greenNumBox);
                }
            }

            // Spawn Yellow Box

            if (remainingTime == 181 && spawnStageYellow == 1)
            {
                timeFallYellowBox = rnd.Next(150, 180);
                Debug.Log("<color=yellow>Время прибытия Yellow бокса 1 волна:</color> " + timeFallYellowBox);
                spawnStageYellow++;
            }
            else if (remainingTime == 120 && spawnStageYellow == 2)
            {
                timeFallYellowBox = rnd.Next(70, 110);
                Debug.Log("<color=yellow>Время прибытия Yellow бокса 2 волна:</color> " + timeFallYellowBox);
                spawnStageYellow++;
            }
            else if (remainingTime == 71 && spawnStageYellow == 3)
            {
                timeFallYellowBox = rnd.Next(10, 40);
                Debug.Log("<color=yellow>Время прибытия  Yellow бокса 3 волна</color> : " + timeFallYellowBox);
                spawnStageYellow++;
            }

            if (Mathf.Approximately(remainingTime, timeFallYellowBox))
            {
                if (boxScript != null)
                {
                    int minBoxCount, maxBoxCount;

                    switch (spawnStageYellow)
                    {
                        case 2:
                            minBoxCount = 1;
                            maxBoxCount = 4;
                            break;
                        case 3:
                            minBoxCount = 2;
                            maxBoxCount = 5;
                            break;
                        default:
                            minBoxCount = 2;
                            maxBoxCount = 6;
                            break;
                    }

                    yellowNumBox = rnd.Next(minBoxCount, maxBoxCount);
                    Debug.Log($"<color=yellow>Yellow Ящиков выпало на этапе {spawnStageYellow - 1}: {yellowNumBox}</color>");
                    boxScript.SpawnMultipleYellowBoxes(yellowNumBox);
                }
            }



            // Spawn Red Box

            if (remainingTime == 121 && spawnStageRed == 1)
            {
                timeFallRedBox = rnd.Next(61, 120);
                Debug.Log("<color=red>Время прибытия Red бокса 1 волна:</color> " + timeFallRedBox);
                spawnStageRed++;
            }
            else if (remainingTime == 60 && spawnStageRed == 2)
            {
                timeFallRedBox = rnd.Next(10, 59);
                Debug.Log("<color=red>Время прибытия Red бокса 2 волна:</color> " + timeFallRedBox);
                spawnStageRed++;
            }

            if (Mathf.Approximately(remainingTime, timeFallRedBox))
            {
                if (boxScript != null)
                {
                    int minBoxCount, maxBoxCount;

                    switch (spawnStageRed)
                    {
                        case 2:
                            minBoxCount = 0;
                            maxBoxCount = 1;
                            break;
                        default:
                            minBoxCount = 1;
                            maxBoxCount = 2;
                            break;
                    }

                    redNumBox = rnd.Next(minBoxCount, maxBoxCount);
                    Debug.Log($"<color=red>Red Ящиков выпало на этапе {spawnStageRed - 1}: {redNumBox}</color>");
                    boxScript.SpawnMultipleRedBoxes(redNumBox);
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
