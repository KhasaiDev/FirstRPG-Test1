using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterKneelState : PlayerGroundedState
{
    public PlayerEnterKneelState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = .3f;
    }

    public override void Update()
    {
        base.Update();
        if (timer < 0)
            stateMachine.ChangeState(player.kneelState);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
