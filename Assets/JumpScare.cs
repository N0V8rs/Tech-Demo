using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject scareObject;
    public AudioSource scareAudio;
    public float duration = 5f; // Duration of the scare in seconds

    private PlayerController playerController;
    private Vector3 playerStartPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable player control and trigger scare
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerStartPosition = other.transform.position; // Store player's position before scare
                playerController.enabled = false;
                StartCoroutine(TriggerScare());
            }
        }
    }

    IEnumerator TriggerScare()
    {
        scareAudio.Play();
        scareObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // After the duration, deactivate the scare, reset player's position and velocity, and enable player control
        scareObject.SetActive(false);
        if (playerController != null)
        {
            playerController.enabled = true;
            Rigidbody playerRigidbody = playerController.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero; // Reset player's velocity
            }
            playerController.transform.position = playerStartPosition; // Reset player's position
        }
    }
}



