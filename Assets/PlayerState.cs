using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    #region Input Info

    protected float xInput;
    protected float yInput;

    #endregion
    #region Dash Timer

    protected float timer;

    #endregion



    public virtual void Enter() 
    {
        player.anim.SetBool(animBoolName, true); 
    }
    
    public virtual void Update() 
    {
        timer -= Time.deltaTime; 
        xInput = Input.GetAxisRaw("Horizontal"); 
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", player.rigidBody.velocity.y);
    }
    
    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false); 
    }
}
