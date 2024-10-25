using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxScript : MonoBehaviour
{
    public GameObject boxPrefab;

    private List<BoxCollider2D> spawnZoneColliders;

    void Start()
    {
        spawnZoneColliders = new List<BoxCollider2D>(GetComponentsInChildren<BoxCollider2D>());

        SpawnBox();
    }

    public void SpawnBox()
    {
        BoxCollider2D selectedZone = spawnZoneColliders[Random.Range(0, spawnZoneColliders.Count)];

        Vector2 center = selectedZone.bounds.center;
        Vector2 size = selectedZone.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        Vector2 spawnPosition = new Vector2(randomX, randomY);


        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
    }
}

