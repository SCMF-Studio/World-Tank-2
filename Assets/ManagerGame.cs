﻿using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;

public class ManagerGame : MonoBehaviour
{
    // Start Game
    public GameObject ready, hud, choise;
    [SerializeField] private GameObject tank_ts0001, tank_th0001, tank_ta0001, tank_tl0001;
    public TextMeshProUGUI info;
    private bool isTankSelected = false;
    private GameObject selectedTank;
    private GameObject spawnedTank;
    //Camera
    public CameraConroller cameraController;

    //Random Spawn
    public Transform SpawnOne, SpawnTwo, SpawnThree, SpawnFour, SpawnFive, SpawnSix, SpawnSeven;
    private int pos;
    System.Random rnd = new System.Random();

    // SetReload Int

    [SerializeField] private HudBar hudBar;

    // Respawn
    private Dictionary<string, float> tankCooldowns = new Dictionary<string, float>();
    private float cooldownDuration = 60f;
    [SerializeField] private TextMeshProUGUI cooldownText_TS, cooldownText_TH, cooldownText_TA, cooldownText_TL;



    void Start()
    {
        // Start Game
        if (info == null)
        {
            info = GetComponent<TextMeshProUGUI>();
        }

        hud.SetActive(false);
        choise.SetActive(true);
        info.text = "Your choice: No tank selected";

        //Random Spawn
        int value = rnd.Next(1, 7);
        pos = value;
        Debug.Log(value);
    }
    void Update()
    {

        UpdateCooldownText("TS-0001", cooldownText_TS);
        UpdateCooldownText("TH-0001", cooldownText_TH);
        UpdateCooldownText("TA-0001", cooldownText_TA);
        UpdateCooldownText("TL-0001", cooldownText_TL);
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            
            transform.position = target.position + new Vector3(0, 0, -10); 
        }
    }

    public void ChangeText(string newText)
    {
        info.text = newText;
    }

    public void Ready()
    {
        if (isTankSelected)
        {
            hud.SetActive(true);
            choise.SetActive(false);
        }
        else
        {
            info.text = "Your choice: No tank selected";
        }
    }
    private string currentTankName;
    private void SelectTank(string choiceText, GameObject tankPrefab, string tankName)
    {
        if (!IsTankAvailable(tankName))
        {
            float timeLeft = GetTankCooldownTimeLeft(tankName);
            info.text = $"Tank {tankName} is on cooldown. Time left: {timeLeft:F1} seconds.";
            return;
        }

        // Если танк доступен
        info.text = choiceText;
        isTankSelected = true;

        if (spawnedTank != null)
        {
            Destroy(spawnedTank);
        }

        spawnedTank = Instantiate(tankPrefab);
        Possition(spawnedTank);
        spawnedTank.name = tankName;
        cameraController.SetTarget(spawnedTank.transform);

        selectedTank = tankPrefab;
        currentTankName = tankName; // Сохраняем имя текущего танка

        float reloadTime = GetReloadTime(spawnedTank);
        hudBar.SetReloadTime(reloadTime);

        if (spawnedTank.TryGetComponent<TS001>(out TS001 ts001))
            ts001.OnReloadStarted += (reloadTime) => hudBar.StartReload();
        if (spawnedTank.TryGetComponent<TH001>(out TH001 th001))
            th001.OnReloadStarted += (reloadTime) => hudBar.StartReload();
        if (spawnedTank.TryGetComponent<TA001>(out TA001 ta001))
            ta001.OnReloadStarted += (reloadTime) => hudBar.StartReload();
        if (spawnedTank.TryGetComponent<TL001>(out TL001 tl001))
            tl001.OnReloadStarted += (reloadTime) => hudBar.StartReload();
    }



    private bool IsTankAvailable(string tankName)
    {
        if (tankCooldowns.TryGetValue(tankName, out float cooldownEndTime))
        {
            return Time.time >= cooldownEndTime; 
        }
        return true; 
    }


    private void SetTankCooldown(string tankName)
    {
        tankCooldowns[tankName] = Time.time + cooldownDuration; 
    }

    private float GetTankCooldownTimeLeft(string tankName)
    {
        if (tankCooldowns.TryGetValue(tankName, out float cooldownEndTime))
        {
            return Mathf.Max(0, cooldownEndTime - Time.time);
        }
        return 0f; 
    }
    private void UpdateCooldownText(string tankName, TextMeshProUGUI textObject)
    {
        float timeLeft = GetTankCooldownTimeLeft(tankName);

        if (timeLeft > 0)
        {
            textObject.text = $"Cooldown: {timeLeft:F1}s";
        }
        else
        {
            textObject.text = "Available";
        }
    }
    private float GetReloadTime(GameObject tank)
    {

        if (tank.TryGetComponent<TH001>(out TH001 th001))
        {
            return th001.ReloadTime;
        }
        else if (tank.TryGetComponent<TS001>(out TS001 ts001))
        {
            return ts001.ReloadTime;
        }
        else if (tank.TryGetComponent<TA001>(out TA001 ta001)) 
        {
            return ta001.ReloadTime;
        }
        else if (tank.TryGetComponent<TL001>(out TL001 tl001)) 
        {
            return tl001.ReloadTime;
        }
        return 0f;
    }


    public void TankStandart0001()
    {
        SelectTank("Your choice: TS-0001", tank_ts0001, "TS-0001");
    }

    public void TankHuge0001()
    {
        SelectTank("Your choice: TH-0001", tank_th0001, "TH-0001");
    }

    public void TankAverage0001()
    {
        SelectTank("Your choice: TA-0001", tank_ta0001, "TA-0001");
    }

    public void TankLittle0001()
    {
        SelectTank("Your choice: TL-0001", tank_tl0001, "TL-0001");
    }

    public GameObject GetSpawnedTank()
    {
        return spawnedTank;
    }

    public void Possition(GameObject tankPos)
    {
        switch (pos)
        {
            case 1:
                tankPos.transform.position = SpawnOne.transform.position;
                break;
            case 2:
                tankPos.transform.position = SpawnTwo.transform.position;
                break;
            case 3:
                tankPos.transform.position = SpawnThree.transform.position;
                break;
            case 4:
                tankPos.transform.position = SpawnFour.transform.position;
                break;
            case 5:
                tankPos.transform.position = SpawnFive.transform.position;
                break;
            case 6:
                tankPos.transform.position = SpawnSix.transform.position;
                break;
            case 7:
                tankPos.transform.position = SpawnSeven.transform.position;
                break;
        }
    }

    public void OnTankDeath()
    {
        if (!string.IsNullOrEmpty(currentTankName))
        {
            SetTankCooldown(currentTankName); 
        }

        choise.SetActive(true);
        hud.SetActive(false);
        isTankSelected = false;
        spawnedTank = null;
        info.text = "Your choice: No tank selected";

        currentTankName = null; 
    }




}