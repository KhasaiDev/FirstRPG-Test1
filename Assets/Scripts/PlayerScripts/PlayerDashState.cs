using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    #region Constructor
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        //Establece la duracion de este estado
        timer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        // Si no hay suelo pero se detecta una pared, cambia al estado de deslizarse por la pared
        if (!player.IsFrontGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);


        // Establece la velocidad en la dirección del dash
        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);


        // Si el dash termina y el jugador está moviéndose, cambia al estado de movimiento
        if (timer < 0 && xInput != 0)
            stateMachine.ChangeState(player.moveState);


        // Si el dash termina y el jugador no se mueve, cambia al estado de detener el dash
        if (timer < 0 && xInput == 0)
            stateMachine.ChangeState(player.dashStopState);
    }

    public override void Exit()
    {
        base.Exit();

        // Detiene el movimiento horizontal al salir del estado de dash
        player.SetVelocity(0, player.rigidBody.velocity.y);
    }


}




