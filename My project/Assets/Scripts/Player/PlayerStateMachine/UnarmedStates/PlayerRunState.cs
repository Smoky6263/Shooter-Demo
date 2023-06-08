using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    private float _turnSmoothVelocity;
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.PlayerAnimator.IsRuning(true);
        _turnSmoothVelocity = _ctx.TurnSmoothVelocity;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        OnMove();
    }

    public override void ExitState()
    {
        _ctx.PlayerAnimator.IsRuning(false);
    }

    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f)
        {
            SwitchState(_factory.Idle());
        }

        if (_ctx.EquipWeaponInput)
        {
            _ctx.PlayerAnimator.IsEquiped(true);
        }
        else 
        {
            _ctx.PlayerAnimator.IsEquiped(false);
        }
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
