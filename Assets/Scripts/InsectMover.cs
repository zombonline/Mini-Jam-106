using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectMover : MonoBehaviour
{
    public Vector2 destination, offscreenDestination;
    public float speed;
    int distanceFromDestination;
    public bool moving = true;
    private void Start()
    {
        float distanceFromDestination = transform.position.x - destination.x;
        offscreenDestination = new Vector2(-transform.position.x, destination.y);
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            offscreenDestination,
            speed * Time.deltaTime);
        }
    }
}
