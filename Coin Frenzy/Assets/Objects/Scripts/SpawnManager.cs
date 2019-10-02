using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float OFFSET = 125;

    public GameObject coinPrefab;
    public GameObject powerupPrefab;
    public Terrain activeTerrain;

    public int coinsAmount;
    public int powerupsAmount;

    private Vector3 groundBounds;
    private Vector3 spawnPosition;
    private Vector3 tempPosition;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects(coinPrefab, coinsAmount);
        SpawnObjects(powerupPrefab, powerupsAmount);
    }

    void SpawnObjects(GameObject objectPrefab, int objectsAmount)
    {
        groundBounds = activeTerrain.GetComponent<TerrainCollider>().bounds.size;
        for (int i = 0; i < objectsAmount; i++)
        {
            spawnPosition = new Vector3(Random.Range(OFFSET, groundBounds.x - OFFSET) + activeTerrain.transform.position.x,
                objectPrefab.transform.position.y, 
                Random.Range(OFFSET, groundBounds.z - OFFSET) + activeTerrain.transform.position.z);
            spawnPosition.y += activeTerrain.SampleHeight(spawnPosition);

            Instantiate(objectPrefab, spawnPosition, objectPrefab.transform.rotation);
        }
    }
}
