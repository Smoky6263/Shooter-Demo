using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmedRunState : PlayerBaseState
{
    private float _turnSmoothVelocity;
    public PlayerArmedRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void CheckSwitchStates()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedIdle());
        }
        else if (_ctx.PlayerMoveInput.magnitude >= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Run());
        }
        else if (_ctx.PlayerMoveInput.magnitude <= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Idle());

        }
    }

    public override void EnterState()
    {
        _ctx.Invoke("TakeInWeapon", _ctx.TimeToGetWeapon);
        _ctx.PlayerAnimator.IsEquiped(true);
        _ctx.PlayerAnimator.IsRuning(true);
    }

    public override void ExitState()
    {        
        _ctx.PlayerAnimator.IsRuning(false);
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        OnMove();
        CheckSwitchStates();
    }

    private void OnMove()
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_ctx.PlayerMoveInput.x, _ctx.PlayerMoveInput.y) * Mathf.Rad2Deg + _ctx.PlayerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _ctx.TurnSmoothTime);

            _ctx.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _ctx.CharacterController.Move(moveDirection.normalized * _ctx.RunSpeed * Time.deltaTime);
            _ctx.PlayerAnimator.IsRuning(true);
            _ctx.PlayerAnimator.IsIdle(false);
        }
    }
}
