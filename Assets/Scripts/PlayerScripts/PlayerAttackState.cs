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

        // Agregamos un timer que establece cuando dura la animacion de este estado
        timer = .1f;


        // Reinicia el combo si se excede el límite de ataques o la ventana de tiempo
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;


        // Actualiza el contador de combo en la animación
        player.anim.SetInteger("ComboCounter", comboCounter);


        // Determina la dirección del ataque (dirección de cara o input horizontal)
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;


        // Establece la velocidad para el movimiento del ataque
        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, player.rigidBody.velocity.y);
    }


    public override void Update()
    {
        base.Update();

        // Detiene el movimiento del jugador cuando el tiempo del ataque ha terminado
        if (timer < 0)
            player.rigidBody.velocity = new Vector2(0, 0);


        // Cambia al estado de inactividad si se ha activado el trigger
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }


    public override void Exit()
    {
        base.Exit();

        // Inicia una pequeña espera para impedir otras acciones mientras el ataque termina
        player.StartCoroutine("BussyFor", 0.15f);


        // Incrementa el contador de combo y registra el tiempo del ataque
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

}
