using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput !=0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (yInput < 0)
            player.rigidBody.velocity = new Vector2(0, player.rigidBody.velocity.y);
        else
            player.rigidBody.velocity = new Vector2(0, player.rigidBody.velocity.y * .6f);

    }

    public override void Exit()
    {
        base.Exit();
    }


}