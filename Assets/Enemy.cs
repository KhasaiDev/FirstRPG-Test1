using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }

    public EnemyStateMachine stateMachine { get; private set; }

    private void Awake()
    { 
        stateMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        // Aqui estamos accediendo al estado actual y luego al Update del estado actual, el motivo del porque lo tenemos aqui, es para que los estados 
        // no tengan que llamar al Update de manera individual para ejecutarse en ciertos momentos, ya que, el PLayer lo llama en cada frame.
        stateMachine.currentState.Update(); 
                                           
    }
}
