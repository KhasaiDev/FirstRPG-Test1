using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStopState : PlayerGroundedState
{
    #region Constructor
    public PlayerDashStopState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        // Establece la duración del estado/animacion
        timer = .4f;


        // Aplica una leve velocidad en la dirección del input horizontal
        player.SetVelocity(1 * xInput, 0);
    }

    public override void Update()
    {
        base.Update();

        // Si el tiempo ha terminado, cambia al estado de inactividad
        if (timer < 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
