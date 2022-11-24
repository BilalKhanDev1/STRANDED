using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 1000f;
    [SerializeField] float _moveSpeed = 5f;

<<<<<<< Updated upstream
    Rigidbody _rigidbody;
    float _mouseMovementX;
=======
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 6.0f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpForce = 4.0f;
    [SerializeField] private bool Sprinting = true;
    [SerializeField] private bool Jumping = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

>>>>>>> Stashed changes

    void Awake() => _rigidbody = GetComponent<Rigidbody>();

<<<<<<< Updated upstream
    void Update()
    {
        if (ToggleablePanel.AnyVisible != true)
        Cursor.lockState = CursorLockMode.Locked;
        _mouseMovementX += Input.GetAxis("Mouse X");
=======


    Camera playerCamera;
    CharacterController characterController;

    private Vector3 moveDir;
    private Vector2 currInput;

    float _mouseMovementX = 0;


    void Awake()
    {

        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYpos = playerCamera.transform.localPosition.y;
    }





    void Update()
    {
        if (ToggleablePanel.AnyVisible != true)
            Cursor.lockState = CursorLockMode.Locked;
        if (isMove)
        {
            MouseLook();
            Move();


            if (Jumping)
                Jump();

            if (canUseHeadbob)
                HeadBob();
        }

>>>>>>> Stashed changes
    }

    void FixedUpdate()
    {
        if (ToggleablePanel.AnyVisible)
        {
<<<<<<< Updated upstream
            Cursor.lockState = CursorLockMode.Confined;
            return;

        }

        transform.Rotate(0, _mouseMovementX * Time.deltaTime * _turnSpeed, 0);

        _mouseMovementX = 0;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
=======
            return;
        }

    }
    private void MouseLook()
    {
        _mouseMovementX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        _mouseMovementX = Mathf.Clamp(_mouseMovementX, -upperlookLimit, lowerlookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(_mouseMovementX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);

    }
    private void Jump()
    {
        if (isJumping)
            moveDir.y = jumpForce;
    }
    private void HeadBob()
    {
        if (!characterController.isGrounded) return;


        if (Mathf.Abs(moveDir.x) > 0.1f || Mathf.Abs(moveDir.z) > 0.1f)
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x, defaultYpos + Mathf.Sin(timer) * (isSprinting ? sprintBobValue : walkBobValue), playerCamera.transform.localPosition.z);

>>>>>>> Stashed changes
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;

        _rigidbody.MovePosition(transform.position + offset);
    }
<<<<<<< Updated upstream
}
=======



}

>>>>>>> Stashed changes
