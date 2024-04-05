using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; 
    public List<GameObject> spawnLocationObjects; 

    void Start()
    {
        for (int i = 0; i < prefabsToSpawn.Count && i < spawnLocationObjects.Count; i++)
        {
            Vector3 spawnPosition = spawnLocationObjects[i].transform.position;
            Instantiate(prefabsToSpawn[i], spawnPosition, Quaternion.identity);
        }
    }
}
