using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    private float _turnSmoothVelocity;
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Invoke("TakeOutWeapon", _ctx.TimeToGetWeapon);
        _ctx.PlayerAnimator.IsEquiped(false);
        _ctx.PlayerAnimator.IsRuning(true);
    }
    public override void UpdateState()
    {
        OnMove();
        CheckSwitchStates();
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
        SwitchToIdle();
        SwitchToArmedIdle();
        SwitchToArmedRun();
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
        }
    }
    
    //CheckSwitchStates Methods
    #region
    private void SwitchToIdle()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Idle());
        }
    }
    private void SwitchToArmedIdle()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedIdle());
        }
    }
    private void SwitchToArmedRun()
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedRun());
        }
    }
    #endregion
}
