using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 2f; 

    private int currentWaypointIndex = 0;
    private bool playerOnPlatform = false;
    private Transform playerTransform;
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();

        if (playerOnPlatform && playerTransform != null)
        {
            playerTransform.position += transform.position - previousPosition;
        }

        previousPosition = transform.position;
    }

    void MovePlatform()
    {
        if (waypoints.Length == 0)
            return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / Vector3.Distance(transform.position, targetPosition));


        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        if (playerOnPlatform && playerTransform != null)
        {
            Vector3 movementDelta = transform.position - previousPosition;

            float playerSpeed = movementDelta.magnitude / Time.deltaTime;
            float adjustmentRatio = playerSpeed > 0 ? speed / playerSpeed * 1f : 1f;

            movementDelta *= adjustmentRatio;

            playerTransform.GetComponent<CharacterController>().Move(movementDelta);
        }

        previousPosition = transform.position;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            playerTransform = other.transform;
            playerTransform.parent = transform;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerTransform.parent = null; 
            playerTransform = null;
        }
    }
}


