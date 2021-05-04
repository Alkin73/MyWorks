using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform cameraTransform;
    
    float verticalLookDirection;
    CharacterController charController;
    float groundCheckRadius = 0.2f;
    bool isGrounded;


    public Transform groundTouchCheck;    
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float mouseSensitivity = 150;
    public float moveSpeed = 10;
    public float jumpPower = 5;
    private new ParticleSystem particleSystem;

    public Vector3 fallVelocity = Vector3.zero;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        charController = GetComponent<CharacterController>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {        
        PlayerLook();
        PlayerMove();
        PlayerGravity();
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerShot();
        }
    }

    void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        transform.Rotate(Vector3.up, mouseX);

        verticalLookDirection -= mouseY;

        verticalLookDirection = Mathf.Clamp(verticalLookDirection, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookDirection, 0f, 0f);
    }

    void PlayerMove()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * movementZ) + (transform.right * movementX);
        charController.Move(moveDirection * moveSpeed * Time.deltaTime);

        //jumping:
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity.y = jumpPower;
        }
    }

    void PlayerGravity()
    {
        isGrounded = Physics.CheckSphere(groundTouchCheck.position, groundCheckRadius, groundMask);
        if (isGrounded && fallVelocity.y < 0)
        {
            fallVelocity = Vector3.zero;
        }
        else
        {
            fallVelocity.y += gravity * Time.deltaTime;
            charController.Move(fallVelocity * Time.deltaTime);
        }
    }

    void PlayerShot()
    {
        particleSystem.Play();
    }
}
