using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{

    private Vector3 initialPosition;
    private CharacterController characterController;
    private Vector3 checkpointPosition;
    public GameObject text;



    void Start()
    {

        initialPosition = transform.position;
        checkpointPosition = initialPosition;
        characterController = GetComponent<CharacterController>();
    }




    void Update()
    {

        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }


    void Respawn()
    {

        characterController.enabled = false;
        transform.position = checkpointPosition != Vector3.zero ? checkpointPosition : initialPosition;
        characterController.enabled = true;
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {

            SetCheckpoint(other.transform.position);
            text.SetActive(true);
        }

        if (other.CompareTag("Checkpoint2"))
        {

            SetCheckpoint(other.transform.position);
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
