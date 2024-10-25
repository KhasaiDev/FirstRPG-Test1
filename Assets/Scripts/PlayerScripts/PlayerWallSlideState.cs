using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    #region Constructor
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //Si saltamos estando en este estado vamos a pasar al walljump, el return es para evitar que el resto del codigo afecte fisicamente al salto empleado
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }


        // Al apretar una tecla en la direccion contraria a la pared en la que estamos pegados nos vamos a salir 
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.moveState);


        //Al tocar el suelo saldremos al idle
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);


        //Si apretamos la tecla hacia abajo vamos a bajar mas rapido
        if (yInput < 0)
            player.rigidBody.velocity = new Vector2(0, player.rigidBody.velocity.y);
        //Si no apretamos la tecla hacia abajo vamos a bajar mas lento
        else
            player.rigidBody.velocity = new Vector2(0, player.rigidBody.velocity.y * .6f);

    }

    public override void Exit()
    {
        base.Exit();
    }


}
