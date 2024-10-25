using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterKneelState : PlayerGroundedState
{
    #region Constructor
    public PlayerEnterKneelState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    #endregion

    public override void Enter()
    {
        base.Enter();

        // Establece la duración del estado/animacion
        timer = .1f;
    }

    public override void Update()
    {
        base.Update();

        //Al terminar la animacion vamos al estado de agachados
        if (timer < 0)
            stateMachine.ChangeState(player.kneelState);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
