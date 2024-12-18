﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdminConsole : MonoBehaviour
{
    public Button killPlayer1, killPlayer2, cntrlHP, cntrlReload, cntrlSpeed, 
        restart, respawnPlayer, spawnPoint, spawnBox, effectBox, cntrlDamage, ghost, time;

    private GameObject player;
    private ManagerGame managerGame;
    private HudBar hudBar;
    private BoxScript boxScript;

    public TMP_InputField inputReload, inputSpeed, inputSpeed_rotation, inputSpeed_turn, inputHp, inputMaxHp, inputDamage, inputSpeedArmo, inputTime;
    private float input_rl, input_sp, input_sp_rt, input_sp_turn, input_hp, input_max_hp, input_dm, input_spd_armo, input_time;

    public Button[] bt_sp, bt_box_sp;
    public int[] spawnCounts;
    public Transform[] spawnPos;
    public GameObject a_cs, a_rs, a_ss, a_hps, a_ds, a_t, a_box;
    public Button a_cs_exit, a_rs_exit, a_ss_exit, a_hps_exit, a_ds_exit, a_t_exit, a_box_exit;


    void Start()
    {
        // Spawn Box

        boxScript = FindObjectOfType<BoxScript>(); 

        for (int i = 0; i < bt_box_sp.Length; i++)
        {
            int index = i; // Локальная копия переменной для замыкания
            bt_box_sp[i].onClick.AddListener(() => SpawnBoxes(index));
        }

        // SpawnPoint
        for (int i = 0; i < bt_sp.Length; i++)
        {
            int index = i;
            bt_sp[i].onClick.AddListener(() => OnButtonClick(index));
        }

        managerGame = FindObjectOfType<ManagerGame>();
        hudBar = FindObjectOfType<HudBar>();
        boxScript = FindObjectOfType<BoxScript>();

        a_cs.SetActive(false);
        a_rs.SetActive(false);
        a_ss.SetActive(false);
        a_hps.SetActive(false);
        a_ds.SetActive(false);
        a_t.SetActive(false);
        a_box.SetActive(false);

        if (managerGame != null)
        {
            player = managerGame.GetSpawnedTank();
        }

        if (inputReload != null)
        {
            inputReload.onEndEdit.AddListener(OnReloadChange);
        }

        if (inputSpeed != null)
        {
            inputSpeed.onEndEdit.AddListener(OnSpeedChange);
        }

        if (inputSpeed_rotation != null)
        {
            inputSpeed_rotation.onEndEdit.AddListener(OnSpeedRotationChange);
        }

        if (inputSpeed_turn != null)
        {
            inputSpeed_turn.onEndEdit.AddListener(OnSpeedTurnChange);
        }


        if (inputHp != null)
        {
            inputHp.onEndEdit.AddListener(OnHPChange);
        }

        if (inputMaxHp != null)
        {
            inputMaxHp.onEndEdit.AddListener(OnHpMaxChange);
        }

        if (inputDamage != null)
        {
            inputDamage.onEndEdit.AddListener(OnDamageChange);
        }

        if (inputSpeedArmo != null)
        {
            inputSpeedArmo.onEndEdit.AddListener(OnSpeedArmoChange);
        }

        if (inputTime != null)
        {
            inputTime.onEndEdit.AddListener(OnTimeGame);
        }

    }


    public void KillTank()
    {
        if (player != null)
        {
            TS001 ts001 = player.GetComponent<TS001>();
            if (ts001 != null)
            {
                ts001.TakeDamage(ts001.maxHP); 
                return;
            }

            TH001 th001 = player.GetComponent<TH001>();
            if (th001 != null)
            {
                th001.TakeDamage(th001.maxHP);
                return;
            }

            TA001 ta001 = player.GetComponent<TA001>();
            if (ta001 != null)
            {
                ta001.TakeDamage(ta001.maxHP);
                return;
            }

            TL001 tl001 = player.GetComponent<TL001>();
            if (tl001 != null)
            {
                tl001.TakeDamage(tl001.maxHP);
                return;
            }
            return;
        }
        return;
    }

    // Reload Setting
    public void OnReloadChange(string inputText)
    {
        if (float.TryParse(inputText, out input_rl))
        {
            bool reloadSet = false;

            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.reloading = input_rl;
                reloadSet = true;
            }

            if (float.TryParse(inputText, out input_rl))
            {
                if (player.TryGetComponent<TH001>(out TH001 th001))
                {
                    th001.reloading = input_rl;
                    reloadSet = true;
                }
            }

            if (float.TryParse(inputText, out input_rl))
            {
                if (player.TryGetComponent<TA001>(out TA001 ta001))
                {
                    ta001.reloading = input_rl;
                    reloadSet = true;
                }
            }

            if (float.TryParse(inputText, out input_rl))
            {
                if (player.TryGetComponent<TL001>(out TL001 tl001))
                {
                    tl001.reloading = input_rl;
                    reloadSet = true;
                }
            }

            if (reloadSet)
            {
                hudBar.SetReloadTime(input_rl);
            }
        }
    }

    public void OnSpeedChange(string inputText)
    {
        if (float.TryParse(inputText, out input_sp))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.speed = input_sp;
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.speed = input_sp;
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.speed = input_sp;
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.speed = input_sp;
            }
        }

    }

    public void OnSpeedRotationChange(string inputText)
    {
        if (float.TryParse(inputText, out input_sp_rt))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.rotationSpeed = input_sp_rt;
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.rotationSpeed = input_sp_rt;
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.rotationSpeed = input_sp_rt;
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.rotationSpeed = input_sp_rt;
            }
        }
    }

    public void OnSpeedTurnChange(string inputText)
    {
        if (float.TryParse(inputText, out input_sp_turn))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.turnSpeed = input_sp_turn;
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.turnSpeed = input_sp_turn;
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.turnSpeed = input_sp_turn;
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.turnSpeed = input_sp_turn;
            }
        }
    }

    public void OnHPChange(string inputText)
    {
        if (float.TryParse(inputText, out input_hp))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.SetCurrentHP(input_hp);  
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.SetCurrentHP(input_hp);
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.SetCurrentHP(input_hp);
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.SetCurrentHP(input_hp);
            }
        }
    }

    public void OnHpMaxChange(string inputText)
    {
        if (float.TryParse(inputText, out input_max_hp))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.SetMaxHP(input_max_hp);  
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.SetMaxHP(input_max_hp);
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.SetMaxHP(input_max_hp);
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.SetMaxHP(input_max_hp);
            }
        }
    }

    public void OnDamageChange(string inputText)
    {
        if (float.TryParse(inputText, out input_dm))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.damage = input_dm;
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.damage = input_dm;
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.damage = input_dm;
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.damage = input_dm;
            }
        }
    }

    public void OnSpeedArmoChange(string inputText)
    {
        if (float.TryParse(inputText, out input_spd_armo))
        {
            if (player.TryGetComponent<TS001>(out TS001 ts001))
            {
                ts001.armoSpeed = input_spd_armo;
            }

            if (player.TryGetComponent<TH001>(out TH001 th001))
            {
                th001.armoSpeed = input_spd_armo;
            }

            if (player.TryGetComponent<TA001>(out TA001 ta001))
            {
                ta001.armoSpeed = input_spd_armo;
            }

            if (player.TryGetComponent<TL001>(out TL001 tl001))
            {
                tl001.armoSpeed = input_spd_armo;
            }
        }
    }

    // Restart Game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // SpawnPoint
    void OnButtonClick(int index)
    {
        TeleportPlayerToSpawn(index);
    }

    void TeleportPlayerToSpawn(int index)
    {
        if (index >= 0 && index < spawnPos.Length)
        {
            player.transform.position = spawnPos[index].position;
        }
    }

    // Spawn Box 
    private void SpawnBoxes(int boxType)
    {
        if (boxScript == null)
        {
            return;
        }

        int count = (boxType >= 0 && boxType < spawnCounts.Length) ? spawnCounts[boxType] : 1;

        switch (boxType)
        {
            case 0:
                boxScript.SpawnMultipleGreenBoxes(count);
                break;

            case 1:
                boxScript.SpawnMultipleYellowBoxes(count);
                break;

            case 2:
                boxScript.SpawnMultipleRedBoxes(count);
                break;

            default:
                break;
        }
    }
    public void SpawnPoint()
    {
        a_cs.SetActive(true);
    }

    public void SpawnPointExit()
    {
        a_cs.SetActive(false);
    }

    // Time Setting

    public void OnTimeGame(string inputText)
    {
        if (float.TryParse(inputText, out input_time))
        {
            hudBar.remainingTime = input_time;
        }
    }


    // Reload Setting
    public void ReloadShow()
    {
        a_rs.SetActive(true);
    }

    public void ReloadHide()
    {
        a_rs.SetActive(false);
    }

    // Speed Setting
    public void SpeedShow()
    {
        a_ss.SetActive(true);
    }

    public void SpeedHide()
    {
        a_ss.SetActive(false);
    }

    // HP Setting
    public void HPShow()
    {
        a_hps.SetActive(true);
    }

    public void HPHide()
    {
        a_hps.SetActive(false);
    }

    // Damage Setting
    public void DamageShow()
    {
        a_ds.SetActive(true);
    }

    public void DamageHide()
    {
        a_ds.SetActive(false);
    }

    public void TimeShow()
    {
        a_t.SetActive(true);
    }

    public void TimeHide()
    {
        a_t.SetActive(false);
    }

    // BoxSpawn

    public void BoxSpawnShow()
    {
        a_box.SetActive(true);
    }

    public void BoxSpawnHide()
    {
        a_box.SetActive(false);
    }
}
