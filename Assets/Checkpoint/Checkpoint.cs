using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RespawnOnFall : MonoBehaviour
{
    private Vector3 initialPosition;
    private CharacterController characterController;
    private Vector3 checkpointPosition;
    public GameObject statusTextObject;
    private TextMeshProUGUI statusText;
    public AudioClip respawnSound;
    public AudioClip checkpointSound;
    private AudioSource audioSource;

    void Start()
    {
        initialPosition = transform.position;
        checkpointPosition = initialPosition;
        characterController = GetComponent<CharacterController>();
        statusText = statusTextObject.GetComponent<TextMeshProUGUI>();
        statusTextObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Respawn()
    {
        characterController.enabled = false;
        transform.position = checkpointPosition != Vector3.zero ? checkpointPosition : initialPosition;
        characterController.enabled = true;
        audioSource.PlayOneShot(respawnSound);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        audioSource.PlayOneShot(checkpointSound);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCheckpoint(other.transform.position);
            statusText.text = "Checkpoint reached!";
            statusTextObject.SetActive(true);
        }
        else if (other.CompareTag("KillBox"))
        {
            Respawn();
        }
    }
}
