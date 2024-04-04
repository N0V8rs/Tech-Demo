using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public GameObject Door;
    public float speed = 1.0f; // Speed of the door's movement
    public float targetHeight = 1.0f; // Target height for the door
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool doorMovingUp = false;
    private bool doorMovingDown = false;
    public AudioSource doorOpenng;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Door.transform.position;
        targetPosition = new Vector3(Door.transform.position.x, Door.transform.position.y + targetHeight, Door.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (doorMovingUp)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, targetPosition, Time.deltaTime * speed);
            if (Door.transform.position.y >= targetPosition.y)
            {
                doorMovingUp = false;
            }
        }
        else if (doorMovingDown)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, initialPosition, Time.deltaTime * speed);
            if (Door.transform.position.y <= initialPosition.y)
            {
                doorMovingDown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorMovingUp = true;
        doorMovingDown = false;
        doorOpenng.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        doorMovingDown = true;
        doorMovingUp = false;
        doorOpenng.Play();
    }
}
