using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField] private float _spd = 2.0f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _fireRate = 1.0f;
    private float _attackTime = -1.0f;
    private Vector3 _velocity;
    private Transform _player;
    private Health _playerHealth;
    private CharacterController _controller;
    [SerializeField] private EnemyState _currentState = EnemyState.Chase;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();

        if (_controller == null)
        {
            Debug.LogError("The CHARACTER CONTROLLER is NULL");
        }

        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("The PLAYER is NULL");
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                //Nothing yet
                break;
            case EnemyState.Chase:
                Movement();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    void Movement()
    {
        if (_controller.isGrounded)
        {
            Vector3 direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _spd;
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Attack()
    {
        if (Time.time > _attackTime)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Damage(25);
            }
            _attackTime = Time.time + _fireRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentState = EnemyState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentState = EnemyState.Chase;
        }
    }
}
