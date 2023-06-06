using UnityEngine;
public class PlayerGroundedState : PlayerBaseState
{

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
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
        if(_ctx.PlayerInput.magnitude >= 0.1f)
        {
            SwitchState(_factory.Walk());
        }
    }

}
