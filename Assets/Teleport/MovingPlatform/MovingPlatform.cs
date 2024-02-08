using UnityEngine;

public class MovingCubePlatform : MonoBehaviour
{
    public Transform startPoint; // The point where the platform starts
    public Transform endPoint;   // The point where the platform ends
    public float speed = 2f;     // Speed of the platform

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;
    private Vector3 playerOffset; // Offset between the player and the platform

    void Start()
    {
        startPos = startPoint.position;
        endPos = endPoint.position;

        // Calculate initial player offset
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerOffset = player.transform.position - transform.position;
        }
    }

    void Update()
    {
        if (movingToEnd)
        {
            MovePlatform(startPos, endPos);
        }
        else
        {
            MovePlatform(endPos, startPos);
        }
    }

    void MovePlatform(Vector3 from, Vector3 to)
    {
        transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);

        if (transform.position == to)
        {
            // If the platform reaches the end point, switch direction
            movingToEnd = !movingToEnd;
        }

        // Move the player along with the platform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = transform.position + playerOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}


