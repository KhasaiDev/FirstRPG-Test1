using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStopState : PlayerGroundedState
{
    public PlayerDashStopState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = .4f;
        player.SetVelocity(1 * xInput, 0);
    }

    public override void Update()
    {
        base.Update();
        if (timer < 0)
            stateMachine.ChangeState(player.idleState);
    }


    public override void Exit()
    {
        base.Exit();
    }


}
