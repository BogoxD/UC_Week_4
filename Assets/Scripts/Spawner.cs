using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    public float spawnInterval = 1f;
    public float spawnDelay = 0f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), spawnDelay, spawnInterval);
    }
    private void SpawnObject()
    {
        int i = Random.Range(0, prefabs.Length);
        GameObject spawnedObj = Instantiate(prefabs[i], transform.position, prefabs[i].transform.rotation);
    }

}
