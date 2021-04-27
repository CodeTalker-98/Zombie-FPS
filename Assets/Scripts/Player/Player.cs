using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _spd = 2.5f;
    [SerializeField] private float _jumpHeight = 5.0f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _sensitivity = 1.0f;
    private float _yVelocity;
    private CharacterController _controller;
    private Camera _cam;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cam = Camera.main;

        if (_controller == null)
        {
            Debug.LogError("The CHARACTER CONTROLLER is NULL");
        }

        if (_cam == null)
        {
            Debug.LogError("The MAIN CAMERA is NULL");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
        Movement();
        Controls();
    }

    void Movement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hInput, 0, vInput);
        Vector3 velocity = direction * _spd;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        velocity.y = _yVelocity;
        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _sensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCamRotation = _cam.gameObject.transform.localEulerAngles;
        currentCamRotation.x -= mouseY * _sensitivity;
        currentCamRotation.x = Mathf.Clamp(currentCamRotation.x, 0.0f, 20);
        _cam.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCamRotation.x, Vector3.right);
    }

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
