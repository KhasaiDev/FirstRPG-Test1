using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    #region Constructor
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        // Establece la duración del estado/animacion
        timer = .2f;

        // Ajustamos la velocidad para saltar en la direccion contraria de la muralla
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        // Cuando el timer acaba y la animacion termina de ejecutarse pasamos al airstate
        if (timer < 0)
            stateMachine.ChangeState(player.airState);


    }

    public override void Exit()
    {
        base.Exit();
    }


}
