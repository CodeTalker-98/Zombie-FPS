using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _spd = 2.5f;
    [SerializeField] private float _jumpHeight = 5.0f;
    [SerializeField] private float _gravity = 9.81f;
    private Vector3 _direction;
    private Vector3 _velocity;
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
    }

    private void Update()
    {
        CameraRotation();
        Movement();
    }

    void Movement()
    {
        if (_controller.isGrounded)
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");

             _direction = new Vector3(hInput, 0, vInput);
             _velocity = _direction * _spd;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        _velocity = transform.TransformDirection(_velocity);

        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCamRotation = _cam.gameObject.transform.localEulerAngles;
        currentCamRotation.x -= mouseY;
        _cam.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCamRotation.x, Vector3.right);
    }
}
