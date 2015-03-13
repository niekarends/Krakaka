using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    public GameObject[] tree;
    public Vector3 spawnValuesMin;
    public Vector3 spawnValuesMax;
    public float maxSpawnInterval;

    void Start()
    {
        StartCoroutine(spawnWaves());
    }
    IEnumerator spawnWaves()
    {
        while (true)
        {
            float xRandom = 0;
            if (Random.Range(0, 127) % 2 == 0)
            {
                xRandom = Random.Range(spawnValuesMin.x, spawnValuesMax.x);
            }
            else
            {
                xRandom = Random.Range(-spawnValuesMin.x, -spawnValuesMax.x);
            }
            Vector3 spawnPosition = new Vector3(xRandom, spawnValuesMin.y, spawnValuesMin.z);
            Quaternion spawnRotation = Quaternion.identity;
            spawnRotation.y = Random.rotation.y;
            Instantiate(tree[Random.Range(0,4)], spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(.0f, maxSpawnInterval));
        }

    }
}