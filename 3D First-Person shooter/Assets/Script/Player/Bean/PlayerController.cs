using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public Transform Camera;
    public Rigidbody rb;

    [SerializeField] public UserPreferences userPreferences;
    [SerializeField] public CameraController cameraController;

    public float baseSpeed;
    public float speedModifier;
    bool isJumping = false;
    public float jumpTimer;
    public float jumpTime;
    private bool isGrounded;
    public float checkRadius;
    public Transform groundCheck;

    float horizontalInput = 0;
    float verticalInput = 0;
    public bool playerAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        PlayerInput();
        PlayerMovement();
    }

    //Fuctions used in update and start
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        playerAttack = Input.GetMouseButton(0);
    }
    private void PlayerMovement()
    {
        //Player angles = camera angles
        transform.localEulerAngles = new Vector3(0, cameraController.camRotY, 0);

        //Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift) && verticalInput == 1)
        {
            speedModifier = 1.3f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || verticalInput != 1)
        {
            speedModifier = 1;
        }

        //basic WASD movement
        if (verticalInput == 1)
        {
            transform.position += transform.forward * baseSpeed * speedModifier * Time.deltaTime;
        }
        else if (verticalInput == -1)
        {
            transform.position += -transform.forward * baseSpeed* speedModifier * Time.deltaTime;
        }
        if (horizontalInput == 1)
        {
            transform.position += transform.right * baseSpeed * speedModifier * Time.deltaTime;
        }
        else if (horizontalInput == -1)
        {
            transform.position += -transform.right * baseSpeed * speedModifier * Time.deltaTime;
        }

        //Jumping
        isGrounded = Physics.OverlapSphere(groundCheck.position, checkRadius).Length > 0;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimer = jumpTime;
            rb.linearVelocity = new Vector3(0, 1, 0) * speedModifier * 5;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimer >= 0)
            {
                rb.AddForce(new Vector3(0, 1, 0) * speedModifier * 5);
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
}