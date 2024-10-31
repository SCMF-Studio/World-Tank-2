using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneManager : MonoBehaviour
{
    public GameObject[] spawnTankZones;
    private Transform[] spawnZones;

    void Start()
    {
        spawnZones = new Transform[spawnTankZones.Length];

        for (int i = 0; i < spawnTankZones.Length; i++)
        {
            spawnZones[i] = spawnTankZones[i].transform;
        }

        SpawnTankInRandomZone();
    }

    private void SpawnTankInRandomZone()
    {
        int randomZoneIndex = Random.Range(0, spawnZones.Length);
        Transform selectedZone = spawnZones[randomZoneIndex];
        Vector3 spawnPosition = GetRandomPositionInZone(selectedZone);
        Debug.Log($"Tank spawned at: {spawnPosition}");
    }

    private Vector3 GetRandomPositionInZone(Transform zone)
    {
        float minX = zone.position.x - zone.localScale.x / 2;
        float maxX = zone.position.x + zone.localScale.x / 2;
        float minY = zone.position.y - zone.localScale.y / 2;
        float maxY = zone.position.y + zone.localScale.y / 2;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, zone.position.z);
    }
}
