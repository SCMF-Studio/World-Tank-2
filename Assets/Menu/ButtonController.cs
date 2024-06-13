using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    public GameObject button1, button2, button3, button4; 


    public void SinglePlayer()
    {
        SceneManager.LoadScene("Sample Scene");
        Debug.Log("Single work");

    }
    
    public void MultiPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Multiplayer loading...");
        }
    }
    public void Options()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Open option and use ghost menu");
        }
    }

    void Quit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }


}
