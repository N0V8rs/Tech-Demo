using UnityEngine;
using System.Collections;

public class PlatformTeleporter : MonoBehaviour
{
    public Transform teleportDestination;
    public KeyCode teleportKey = KeyCode.E;
    public GameObject actionText;
    public AudioClip teleportSound;
    private AudioSource audioSource;
    public GameObject effects;

    private bool playerInRange = false;
    private bool playerTeleported = false;
    public static bool playerTeleporting = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(teleportKey) && !playerTeleported)
        {
            audioSource.PlayOneShot(teleportSound);
            playerTeleporting = true;
            effects.SetActive(true);
            StartCoroutine(TeleportPlayerAfterDelay(4.0f));
            playerTeleported = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            actionText.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            actionText.SetActive(false);
            playerInRange = false;
            playerTeleported = false; // Reset the teleport status
        }
    }

    private void TeleportPlayer()
    {
        if (teleportDestination != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false;
                    player.transform.position = teleportDestination.position;
                    player.transform.rotation = teleportDestination.rotation;
                    characterController.enabled = true;
                    actionText.SetActive(false);
                    effects.SetActive(false);
                }
                else
                {
                    Debug.LogError("CharacterController component not found on player!");
                }
            }
            else
            {
                Debug.LogError("Player not found!");
            }
        }
        else
        {
            Debug.LogError("Teleport destination is not set!");
        }
    }

    private IEnumerator TeleportPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportPlayer();
        playerTeleporting = false; 
    }
}


