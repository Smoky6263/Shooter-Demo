using UnityEngine;
public class PlayerWalkState : PlayerBaseState
{

    private float _turnSmoothVelocity;
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
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
        if(_ctx.PlayerInput.magnitude <= 0.1f)
        {
            SwitchState(_factory.Idle());
        }
    }

    private void OnMove()
    {
        if (_ctx.PlayerInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_ctx.PlayerInput.x, _ctx.PlayerInput.y) * Mathf.Rad2Deg + _ctx.PlayerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _ctx.TurnSmoothTime);

            _ctx.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _ctx.CharacterController.Move(moveDirection.normalized * _ctx.RunSpeed * Time.deltaTime);
            _ctx.PlayerAnimator.IsRuning(true);
            _ctx.PlayerAnimator.IsIdle(false);
        }
    }

}
