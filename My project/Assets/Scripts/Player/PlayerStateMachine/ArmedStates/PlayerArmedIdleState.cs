using System.Collections;
using UnityEngine;

public class PlayerArmedIdleState : PlayerBaseState
{
    public PlayerArmedIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Invoke("TakeInWeapon", _ctx.TimeToGetWeapon);
        _ctx.PlayerAnimator.IsEquiped(true);
        _ctx.PlayerAnimator.IsIdle(true);
    }
    public override void ExitState()
    {
      _ctx.PlayerAnimator.IsIdle(false);
    }
    public override void UpdateState()
    {
        
        CheckSwitchStates();
    }


    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        SwitchToArmedRun();
        SwitchToRun();
        SwitchToIdle();
    }
    //CheckSwitchStates Methods
    #region
    private void SwitchToArmedRun()
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedRun());
        }
    }
    private void SwitchToRun()
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Run());
        }
    }
    private void SwitchToIdle()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Idle());

        }
    }
    #endregion
}
