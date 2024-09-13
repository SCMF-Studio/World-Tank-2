using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminConsole : MonoBehaviour
{
    public Button killPlayer1, killPlayer2, cntrlHP, cntrlReload, cntrlSpeed, 
        restart, respawnPlayer, spawnPoint, spawnBox, effectBox, cntrlDamage, ghost;


    public void KillPlayer1()
    {
        Debug.Log("Kill TAnk");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
