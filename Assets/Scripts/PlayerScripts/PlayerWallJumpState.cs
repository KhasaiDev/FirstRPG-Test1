using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = .2f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (timer < 0)
            stateMachine.ChangeState(player.airState);


    }

    public override void Exit()
    {
        base.Exit();
    }


}
