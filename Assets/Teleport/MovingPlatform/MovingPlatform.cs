using UnityEngine;

public class MovingCubePlatform : MonoBehaviour
{
    public Transform startPoint; // The point where the platform starts
    public Transform endPoint;   // The point where the platform ends
    public float speed = 2f;     // Speed of the platform

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    void Start()
    {
        startPos = startPoint.position;
        endPos = endPoint.position;
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
    }
}

