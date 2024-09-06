using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        walkHandler();

        jumpHandler();
    }

    void walkHandler()
    {

        float distance = walkSpeed * Time.deltaTime;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalAxis, 0f, verticalAxis).normalized * distance;
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + movement;

        rb.MovePosition(newPosition);
    }

    void jumpHandler()
    {

    }
}
