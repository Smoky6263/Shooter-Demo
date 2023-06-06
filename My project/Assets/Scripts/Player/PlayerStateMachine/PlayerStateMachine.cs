using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerStateMachine : MonoBehaviour , IControllable
{
    private CharacterController _characterController;
    private PlayerAnimatorController _playerAnimator;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;


    [SerializeField]
    private Transform _playerCamera;

    [SerializeField]
    private float _runSpeed = 6f;

    [SerializeField]
    private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;

    private Vector2 _playerInput;
    private Vector3 _gravityForce = new Vector3(0f, -9.8f, 0f);

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public PlayerAnimatorController PlayerAnimator { get { return _playerAnimator; } }
    public Vector2 PlayerInput { get { return _playerInput; } }
    public Transform PlayerCamera { get { return _playerCamera; } }
    public float TurnSmoothVelocity { get { return _turnSmoothVelocity; } }
    public float TurnSmoothTime { get { return _turnSmoothTime; } }
    public float RunSpeed { get { return _runSpeed; } }


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimatorController>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    public void Jump()
    {
        
    }

    public void Move(Vector2 direction)
    {
        _playerInput = direction;
    }

    private void FixedUpdate()
    {        
        _currentState.UpdateState();        
    }

}
