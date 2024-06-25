using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject button1, button2, button3, button4;
    public GameObject option_menu;


    public void SinglePlayer()
    {
        
        SceneManager.LoadScene("SampleScene");

    }
     public void MultiPlayer()
    {
     
        Debug.Log("Multiplayer loading...");

    }
    public void Options()
    {

        option_menu.SetActive(true);
    }
    public void ExitOptions()
    {
        option_menu.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    } 


}
