using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    #region Constructor
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        // Agregar lógica de caer mas rapido 
        if (player.rigidBody.velocity.y < 0)
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1) * Time.deltaTime;
        
        
        // Si detecta una pared, cambiar al estado de deslizamiento
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);


        // Si el jugador toca el suelo, transita al estado de idle o de movimiento
        if (player.IsGroundDetected() || player.IsGround2Detected())
            stateMachine.ChangeState(player.idleState);
        

        // Si el jugador se mueve, aplica movimiento en el aire
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, player.rigidBody.velocity.y);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

}
