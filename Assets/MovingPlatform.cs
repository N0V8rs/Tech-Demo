using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public Transform startMovement;
    [SerializeField] public Transform endMovement;
    [SerializeField] public float speed = 1.0f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool Moving;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement(startPos, endPos);
    }

    void Movement(Vector3 from, Vector3 to)
    {
        transform.position = from;
        transform.position += to;
    }
}
