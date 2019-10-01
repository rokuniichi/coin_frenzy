using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject coinPrefab;
    public GameObject powerupPrefab;
    public int coinsAmount;
    public int powerupsAmount;

    private Vector3 groundBounds;
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects(coinPrefab, coinsAmount);
        SpawnObjects(powerupPrefab, powerupsAmount);
    }

    void SpawnObjects(GameObject objectPrefab, int objectsAmount)
    {
        groundBounds = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshCollider>().bounds.size;
        for (int i = 0; i < objectsAmount; i++)
        {
            spawnPosition = new Vector3(Random.Range(-groundBounds.x / 2.0f, groundBounds.x / 2.0f),
                objectPrefab.transform.position.y, Random.Range(-groundBounds.z / 2.0f, groundBounds.z / 2.0f));

            Instantiate(objectPrefab, spawnPosition, objectPrefab.transform.rotation);
        }
    }
}
