using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 25f;
    public float jumpSpeed = 7f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public int maxJumps = 2;
    public int gravityDirection = 1;
    public AudioSource coinAudioSource;
    public HudManager hud;
    public Transform cameraTransform;
    float errorMargin = 0.01f;
    float coyoteTimeCounter;
    float jumpBufferCounter;
    bool hasPressedJump = true;
    int jumpCounter = 0;

    Rigidbody rigidBody;
    Collider collisionDetection;
    

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collisionDetection = GetComponent<Collider>();

        hud.Refresh();
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
        Vector3 CameraForward = cameraTransform.forward;
        Vector3 CameraRight = cameraTransform.right;

        CameraForward.y = 0f;
        CameraRight.y = 0f;

        CameraForward.Normalize();
        CameraRight.Normalize();

        Vector3 movement = (CameraForward * verticalAxis + CameraRight * horizontalAxis).normalized * distance;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + movement;

        rigidBody.MovePosition(newPosition);

        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }

    void jumpHandler()
    {
        float jumpAxis = Input.GetAxis("Jump");
        bool isGrounded = checkGrounded();

        if (jumpAxis > 0f)
        {
            bool jumpLogic = (isGrounded || coyoteTimeCounter > 0f || jumpCounter < maxJumps);

            jumpBufferCounter = jumpBufferTime;

            if (!hasPressedJump && jumpLogic)
            {
                executeJump();

                jumpCounter += 1;

                hasPressedJump = true;

                coyoteTimeCounter = 0f;

            }

        }
        else
        {
            hasPressedJump = false;
        }

        if (isGrounded)
        {
            jumpCounter = 0;
            coyoteTimeCounter = coyoteTime;

            if (jumpBufferCounter > 0f) 
            {
                executeJump();
                jumpBufferCounter = 0f;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        jumpBufferCounter -= Time.deltaTime;
    }

    void executeJump()
    {
        Vector3 jumpVector = new Vector3(0f, jumpSpeed * gravityDirection, 0f);
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.velocity += jumpVector;
    }

    bool checkGrounded()
    {
        float sizeX = collisionDetection.bounds.size.x;
        float sizeY = collisionDetection.bounds.size.y;
        float sizeZ = collisionDetection.bounds.size.z;

        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, (-sizeY / 2 + errorMargin) * gravityDirection, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, (-sizeY / 2 + errorMargin) * gravityDirection, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, (-sizeY / 2 + errorMargin) * gravityDirection, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, (-sizeY / 2 + errorMargin) * gravityDirection, -sizeZ / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down * gravityDirection, errorMargin);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down * gravityDirection, errorMargin);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down * gravityDirection, errorMargin);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down * gravityDirection, errorMargin);

        bool isGrounded = (grounded1 || grounded2 || grounded3 || grounded4);

        return isGrounded;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Coin")
        {

            GameManager.instance.IncreaseScore(1);

            hud.Refresh();

            coinAudioSource.Play();

            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.tag == "Enemy")
        {

            SceneManager.LoadScene("GameOver");
        }

        else if (collider.gameObject.tag == "Goal")
        {
            GameManager.instance.IncreaseLevel();
        }
    }
}
