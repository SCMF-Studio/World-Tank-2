using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public class BoxScript : MonoBehaviour
{
    public GameObject greenZone, yellowZone, redZone;
    public GameObject[] greenBox;
    public GameObject[] yellowBox;
    public GameObject[] redBox;

    public int numBoxGreen, numBoxYellow, numBoxRed;
    System.Random rnd = new System.Random();

    private List<BoxCollider2D> greenZoneColliders;
    private List<BoxCollider2D> yellowZoneColliders;
    private List<BoxCollider2D> redZoneColliders;
    private HudBar hudBar;



    void Start()
    {
        greenZoneColliders = new List<BoxCollider2D>(greenZone.GetComponentsInChildren<BoxCollider2D>());
        yellowZoneColliders = new List<BoxCollider2D>(yellowZone.GetComponentsInChildren<BoxCollider2D>());
        redZoneColliders = new List<BoxCollider2D>(redZone.GetComponentsInChildren<BoxCollider2D>());
        hudBar = FindObjectOfType<HudBar>();

    }

    private void SpawnBoxInZone(List<BoxCollider2D> zoneColliders, GameObject[] boxArray)
    {
        int colliderIndex = rnd.Next(zoneColliders.Count);
        BoxCollider2D zoneCollider = zoneColliders[colliderIndex];

        int boxIndex = rnd.Next(boxArray.Length);

        Vector2 center = zoneCollider.bounds.center;
        Vector2 size = zoneCollider.bounds.size;
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(boxArray[boxIndex], spawnPosition, Quaternion.identity);
    }

    public void SpawnMultipleGreenBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBoxInZone(greenZoneColliders, greenBox);
        }
    }

    public void SpawnMultipleYellowBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBoxInZone(yellowZoneColliders, yellowBox);
        }
    }

    public void SpawnMultipleRedBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBoxInZone(redZoneColliders, redBox);
        }
    }


}

