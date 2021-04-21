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

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("The CHARACTER CONTROLLER is NULL");
        }
    }

    private void Update()
    {
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

        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
