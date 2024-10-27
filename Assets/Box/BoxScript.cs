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

    private List<BoxCollider2D> spawnZoneColliders;

    void Start()
    {
        spawnZoneColliders = new List<BoxCollider2D>(GetComponentsInChildren<BoxCollider2D>());

        SpawnBoxInZone(greenZone, greenBox);
        SpawnBoxInZone(yellowZone, yellowBox);
        SpawnBoxInZone(redZone, redBox);
    }

    private void SpawnBoxInZone(GameObject zone, GameObject[] boxArray)
    {
        BoxCollider2D zoneCollider = zone.GetComponent<BoxCollider2D>();

        int boxIndex = rnd.Next(0, boxArray.Length);

        Vector2 center = zoneCollider.bounds.center;
        Vector2 size = zoneCollider.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(boxArray[boxIndex], spawnPosition, Quaternion.identity);
    }
}

