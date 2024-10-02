using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 25f;
    public float jumpSpeed = 7f;
    public int maxJumps = 2;
    public AudioSource coinAudioSource;
    public HudManager hud;
    float errorMargin = 0.01f;
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

        Vector3 movement = new Vector3(horizontalAxis, 0f, verticalAxis).normalized * distance;
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + movement;

        rigidBody.MovePosition(newPosition);
    }

    void jumpHandler()
    {
        float jumpAxis = Input.GetAxis("Jump");
        bool isGrounded = checkGrounded();

        if (jumpAxis > 0f)
        {
            if (!hasPressedJump && (isGrounded || jumpCounter < maxJumps))
            {

                Vector3 jumpVector = new Vector3(0F, jumpSpeed, 0f);

                rigidBody.velocity += jumpVector;

                jumpCounter += 1;

                hasPressedJump = true;

            }

        }
        else
        {
            hasPressedJump = false;
        }

        if (isGrounded)
        {
            jumpCounter = 0;
        }
    }

    bool checkGrounded()
    {
        float sizeX = collisionDetection.bounds.size.x;
        float sizeY = collisionDetection.bounds.size.y;
        float sizeZ = collisionDetection.bounds.size.z;

        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + errorMargin, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + errorMargin, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + errorMargin, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + errorMargin, -sizeZ / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down, errorMargin);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, errorMargin);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, errorMargin);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, errorMargin);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    private void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag);
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
