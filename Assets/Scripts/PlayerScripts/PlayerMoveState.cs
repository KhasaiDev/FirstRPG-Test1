using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    #region Constructor
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //Le damos velocidad al personaje el direccion a la tecla que esta presionando
        player.SetVelocity(xInput * player.moveSpeed, player.rigidBody.velocity.y);


        //Mientras nos movemos y detectamos una muralla pasaremos al Idle para evitar correr hacia la muralla
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);


        //Si estamos corriendo y apretamos la tecla S comenzamos a agacharnos
        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.ChangeState(player.enterKneelState);


        //Si no hay movimiento pasamos al Idle
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();
    }


}
