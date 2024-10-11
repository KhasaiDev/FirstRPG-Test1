using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    #region Constructor
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    #endregion

    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 1;


    public override void Enter()
    {
        base.Enter();
        timer = .1f;

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;
        if(xInput != 0)
            attackDir = xInput;

        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, player.rigidBody.velocity.y);
    }


    public override void Update()
    {
        base.Update();
        if(timer < 0)
            player.rigidBody.velocity = new Vector2(0, 0);

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }


    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BussyFor", 0.15f);

        comboCounter++;
        lastTimeAttacked = Time.time;

    }

}
