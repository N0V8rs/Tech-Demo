using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public Transform teleportDestination; // Destination to teleport the player
    public GameObject pressE; // UI element to prompt the player to press E

    void Start()
    {
        // Ensure that the UI element is initially inactive
        pressE.SetActive(false);
    }

    void Update()
    {
        // Check for the E key press when the UI element is active and the player is inside the trigger zone
        if (pressE.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Activate the UI element to prompt the player to press E
            pressE.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Deactivate the UI element
            pressE.SetActive(false);
        }
    }

    void TeleportPlayer()
    {
        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Ensure the player GameObject exists and the teleport destination is set
        if (player != null && teleportDestination != null)
        {
            // Teleport the player to the specified destination
            player.transform.position = teleportDestination.position;
        }
    }
}

