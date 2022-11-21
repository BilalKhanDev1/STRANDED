using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 1000f;
    [SerializeField] float _moveSpeed = 5f;

    Rigidbody _rigidbody;
    float _mouseMovementX;
    float _mouseMovementY;

    void Awake() => _rigidbody = GetComponent<Rigidbody>();

    void Update()
    {
        if (ToggleablePanel.AnyVisible != true)
        Cursor.lockState = CursorLockMode.Locked;
        _mouseMovementX += Input.GetAxis("Mouse X");
        _mouseMovementY -= Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        if (ToggleablePanel.AnyVisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
            return;

        }

        transform.Rotate(0, _mouseMovementX * Time.deltaTime * _turnSpeed, _mouseMovementY * Time.deltaTime);

        _mouseMovementX = 0;
        _mouseMovementY = 0;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;

        _rigidbody.MovePosition(transform.position + offset);
    }
}