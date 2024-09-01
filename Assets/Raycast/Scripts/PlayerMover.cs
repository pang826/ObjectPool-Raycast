using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    Vector3 destination;

    [SerializeField]
    float moveSpeed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed *  Time.deltaTime);
    }
    public void Move(Vector3 destination)
    {
        this.destination = destination;
    }
}
