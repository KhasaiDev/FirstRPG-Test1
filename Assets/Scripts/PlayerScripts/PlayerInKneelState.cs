using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInKneelState : PlayerGroundedState
{
    public PlayerInKneelState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();

        if (yInput >= 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
