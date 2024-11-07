using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    #region Constructor
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        //Detenemos la velocidad al entrar en este estado para evitar que el personaje se deslice o tenga movimientos raros
        player.ZeroVelocity();
    }


    public override void Update()
    {
        base.Update();

        // Restablece el contador de coyote time si el jugador est� en el suelo
        if (player.IsFrontGroundDetected() || player.IsBackGroundDetected())
            player.coyoteTimeCounter = player.coyoteTime;
        else
            player.coyoteTimeCounter -= Time.deltaTime; // Disminuye el contador si no est� tocando el suelo


        // Transici�n al estado de ataque al hacer click
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);


        // Transici�n al estado de salto si se presiona espacio dentro del tiempo de coyote
        if (Input.GetKeyDown(KeyCode.Space) && player.coyoteTimeCounter > 0)
            stateMachine.ChangeState(player.jumpState);
        

        // Si el jugador no est� tocando el suelo y se acab� el coyote time, transita al estado a�reo
        if (!player.IsFrontGroundDetected() && !player.IsBackGroundDetected() && player.coyoteTimeCounter <= 0)
            stateMachine.ChangeState(player.airState);

        if(Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttack);
        
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
