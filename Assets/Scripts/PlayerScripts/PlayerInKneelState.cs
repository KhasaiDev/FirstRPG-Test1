using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInKneelState : PlayerState
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
        // Solo voltea en la dirección del input, pero no aplica movimiento
        if (xInput != 0)
        {
            player.FlipController(xInput);  // Voltea el personaje hacia la dirección del input
        }


        if (Input.GetKeyUp(KeyCode.S) && xInput == 0)
            stateMachine.ChangeState(player.idleState);

        if (Input.GetKeyUp(KeyCode.S) && xInput != 0)
            stateMachine.ChangeState(player.moveState);

        if (!player.IsFrontGroundDetected() && player.IsKneelWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

    }

    public override void Exit()
    {
        base.Exit();
    }


}
