using Unity;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    public bool isMove { get; private set; } = true;
    private bool isSprinting => Sprinting && Input.GetKey(sprintKey);
    private bool isJumping => Input.GetKeyDown(jumpKey) && characterController.isGrounded;
    [Header("Look Settings")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperlookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerlookLimit = 80.0f;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

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


    [Header("Headbob")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobValue = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobValue = 0.05f;
    private float defaultYpos = 0;
    private float timer;



    CharacterController characterController;

    private Vector3 moveDir;
    private Vector2 currInput;

    float _mouseMovementX = 0;


    void Awake()
    {

        characterController = GetComponent<CharacterController>();
        defaultYpos = virtualCamera.transform.localPosition.y;
    }

    void Update()
    {
        if (ToggleablePanel.AnyVisible != true)
        {
            isMove = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (isMove)
        {
            MouseLook();
            Move();


            if (Jumping)
                Jump();

            if (canUseHeadbob)
                HeadBob();
        }

    }

    void FixedUpdate()
    {
        if (ToggleablePanel.AnyVisible)
        {
            isMove = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private void MouseLook()
    {
        _mouseMovementX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        _mouseMovementX = Mathf.Clamp(_mouseMovementX, -upperlookLimit, lowerlookLimit);
        virtualCamera.transform.localRotation = Quaternion.Euler(_mouseMovementX, 0, 0);
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
            virtualCamera.transform.localPosition = new Vector3(
                virtualCamera.transform.localPosition.x, defaultYpos + Mathf.Sin(timer) * (isSprinting ? sprintBobValue : walkBobValue), virtualCamera.transform.localPosition.z);

        }
    }
    private void Move()
    {
        if (characterController.isGrounded)
            currInput = new Vector2((isSprinting ? sprintSpeed : moveSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintSpeed : moveSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDir.y;
        if (characterController.isGrounded)
            moveDir = (transform.TransformDirection(Vector3.forward) * currInput.x) + (transform.TransformDirection(Vector3.right) * currInput.y);
        moveDir.y = moveDirectionY;

        if (!characterController.isGrounded)
            moveDir.y -= gravity * Time.deltaTime;
        characterController.Move(moveDir * Time.deltaTime);
    }
}


