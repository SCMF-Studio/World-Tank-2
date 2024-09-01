using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSpawn : MonoBehaviour
{
    
    public Transform SpawnOne, SpawnTwo, SpawnThree, SpawnFour, SpawnFive, SpawnSix, SpawnSeven;
    private int pos;

    
    System.Random rnd = new System.Random();

    
    


    void Start()
    {
        int value = rnd.Next(1, 8);
        pos = value;
        Debug.Log(value);
        
    }

    private void Update()
    {
        
    }


    public void Possition(GameObject tank)
    {
        switch (pos)
        {
            case 1:
                tank.transform.position = SpawnOne.transform.position;
                break;
            case 2:
                tank.transform.position = SpawnTwo.transform.position;
                break;
            case 3:
                tank.transform.position = SpawnThree.transform.position;
                break;
            case 4:
                tank.transform.position = SpawnFour.transform.position;
                break;
            case 5:
                tank.transform.position = SpawnFive.transform.position;
                break;
            case 6:
                tank.transform.position = SpawnSix.transform.position;
                break;
            case 7:
                tank.transform.position = SpawnSeven.transform.position;
                break;


        }
    }
}
