using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashKneelState : PlayerState
{
    #region Constructor
    public PlayerDashKneelState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        // Establece la duración del estado/animacion
        timer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        //Le damos velocidad y direccion al momento en que entra al dash
        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);


        // Si el timer termina y la tecla "S" está presionada, cambia al estado de agachado
        if (timer < 0 && Input.GetKey(KeyCode.S))
            stateMachine.ChangeState(player.kneelState);


        // Si el timer termina y no hay input hacia abajo, cambia al estado de idle
        if (timer < 0 && yInput >= 0)
            stateMachine.ChangeState(player.idleState);


        // Cambia al estado de deslizarse por la pared si no se detecta suelo y se detecta una pared
        if (!player.IsGroundDetected() && player.IsKneelWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

    }

    public override void Exit()
    {
        base.Exit();

        // Detiene el movimiento horizontal al salir del estado de dash
        player.SetVelocity(0, player.rigidBody.velocity.y);
    }


}
