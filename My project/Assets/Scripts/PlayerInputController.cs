using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private IControllable controllable;
    private PlayerInput playerInput;
    private void Awake()
    {
        controllable = GetComponent<IControllable>();

        playerInput = new PlayerInput();
        playerInput.Enable();

        if(controllable == null)
        {
            throw new Exception("There is no IControllable component on the object: " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }
    private void Update()
    {
        ReadMove();
    }

    private void ReadMove()
    {
        Vector2 input = playerInput.Character.Movement.ReadValue<Vector2>();
        controllable.Move(input);   
    }
    private void ReadJump()
    {

    }
}
