using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rigidBody.velocity = new Vector2(player.rigidBody.velocity.x, player.jumpForce); 
    }

    public override void Update()
    {
        base.Update();
        if (player.rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (player.rigidBody.velocity.y < 0)
            stateMachine.ChangeState(player.airState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, player.rigidBody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
