using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        if(!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);


        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);

        if (timer < 0 && xInput != 0)
            stateMachine.ChangeState(player.moveState);
        if (timer < 0 && xInput == 0)
            stateMachine.ChangeState(player.dashStopState);

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, player.rigidBody.velocity.y);

    }


}




