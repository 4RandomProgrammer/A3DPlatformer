using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 25f;
    public float jumpSpeed = 7f;
    public int maxJumps = 2;
    bool pressedJump = true;
    int jumpCounter = 0;
    Rigidbody rb;
    Collider coll;
    public AudioSource coinAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
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
        float jumpAxis = Input.GetAxis("Jump");
        bool isGrounded = checkGrounded();
        
        if (jumpAxis > 0f)
        {
            if (!pressedJump && (isGrounded || jumpCounter < maxJumps))
            {

                Vector3 jumpVector = new Vector3(0F, jumpSpeed, 0f);

                rb.velocity += jumpVector;

                jumpCounter += 1;
               
                pressedJump = true;
               
            }
            
        } 
        else
        {
            pressedJump = false;
        }

        if (isGrounded)
        {
            jumpCounter = 0;
        }
    }

    bool checkGrounded()
    {
        float sizeX = coll.bounds.size.x;
        float sizeY = coll.bounds.size.y;
        float sizeZ = coll.bounds.size.z;

        Vector3 corner1 = transform.position + new Vector3(sizeX/2, -sizeY/2 + 0.01f, sizeZ/2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, 0.01f);

        return (grounded1 || grounded2|| grounded3 || grounded4);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            print("Grabbing a Coin");

            GameManager.instance.IncreaseScore(1);

            coinAudioSource.Play();

            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.tag == "Enemy")
        {
            print("game over");
        }

        else if (collider.gameObject.tag == "Goal")
        {
            print("next level");
        }
    }
}
