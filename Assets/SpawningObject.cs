using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObject : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnObject();
    }

    void SpawnObject() 
    {
        GameObject cube;
        GameObject newObject;

        newObject = GameObject.Instantiate(Prefab);
        cube = GameObject.Instantiate(Cube);
        //newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        cube.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        newObject.transform.position = new Vector3(0, 0, 0);
        cube.transform.position = new Vector3(0, 0, 0);
        //newObject.AddComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnObject();
    }
}
