using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
    }


    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected() && !player.IsGround2Detected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGround2Detected())
            stateMachine.ChangeState(player.jumpState);
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
