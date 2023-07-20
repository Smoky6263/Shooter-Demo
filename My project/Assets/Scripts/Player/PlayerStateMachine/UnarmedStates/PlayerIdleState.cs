using System.Collections;
using UnityEngine;
public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Invoke("TakeOutWeapon", _ctx.TimeToGetWeapon);
        _ctx.PlayerAnimator.IsEquiped(false);
        _ctx.PlayerAnimator.IsIdle(true);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        _ctx.PlayerAnimator.IsIdle(false);
    }


    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {

        SwitchToRun();
        SwitchToArmedRun();
        SwitchToArmedIdle();

    }

    //CheckSwitchStates Methods
    #region
    private void SwitchToRun()
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f && !_ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.Run());
        }
    }
    private void SwitchToArmedRun() 
    {
        if (_ctx.PlayerMoveInput.magnitude >= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedRun());
        }
    }
    private void SwitchToArmedIdle()
    {
        if (_ctx.PlayerMoveInput.magnitude <= 0.1f && _ctx.EquipWeaponInputPerformed)
        {
            SwitchState(_factory.ArmedIdle());
        }
    }
    #endregion
}
