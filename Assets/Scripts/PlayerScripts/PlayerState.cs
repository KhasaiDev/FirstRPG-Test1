using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    #region Input Info

    protected float xInput;
    protected float yInput;

    #endregion

    protected float timer;

    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }




    public virtual void Enter() 
    {
        player.anim.SetBool(animBoolName, true); 
        triggerCalled = false;
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

    public virtual void AnimationTriggerFinish()
    {
        triggerCalled = true;
    }
}
