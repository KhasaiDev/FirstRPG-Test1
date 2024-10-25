using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    #region Constructor
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //Si apretamos la tecla hacia abajo comenzamos la animacion de agacharse
        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.ChangeState(player.enterKneelState);


        //Si estamos mirando hacia una pared y estamos colisionando con ella, cancelamos el movimiento y volvemos al idle
        if (xInput == player.facingDir && player.IsWallDetected())
            return;


        //Con esto evitamos que el personaje entre al idle por un breve segundo luego de finalizar un ataque
        if (xInput != 0 && !player.isBussy)
            stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
