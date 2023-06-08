using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private IControllable _controllable;
    private PlayerInput _playerInput;

    private bool _equipWeaponInput =false;

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();

        _playerInput = new PlayerInput();
        _playerInput.Enable();

        if(_controllable == null)
        {
            throw new Exception("There is no IControllable component on the object: " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        _playerInput.Character.EquipWeapon.performed += ReadWeaponEquip;
    }

    private void OnDisable()
    {
        _playerInput.Character.EquipWeapon.performed -= ReadWeaponEquip;
    }
    private void Update()
    {
        ReadMove();
    }

    private void ReadMove()
    {
        Vector2 input = _playerInput.Character.Movement.ReadValue<Vector2>();
        _controllable.Move(input);   
    }
    private void ReadJump()
    {

    }

    private void ReadWeaponEquip(InputAction.CallbackContext obj)
    {
        _equipWeaponInput = !_equipWeaponInput;
        _controllable.EquipWeapon(_equipWeaponInput);
    }
}
