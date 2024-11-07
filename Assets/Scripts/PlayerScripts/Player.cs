using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    public PlayerStateMachine stateMachine { get; private set; }
    public bool isBussy { get; private set; }



    [Header("Attack Info")]
    public float[] attackMovement;
    public float counterAttackDuration;


    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    [SerializeField] public float fallMultiplier;
    [SerializeField] public float lowJumpMultiplier;
    [HideInInspector] public float coyoteTime = 0.1f;
    [HideInInspector] public float coyoteTimeCounter;


    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }


    [Header("Player Collision Info")]
    [SerializeField] private Transform kneelWallCheck;
    [SerializeField] private float kneelWallCheckDistance;



    //Primero instanciar los estados aqui
    #region Estados

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDashStopState dashStopState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerEnterKneelState enterKneelState { get; private set; }
    public PlayerInKneelState kneelState { get; private set; }
    public PlayerDashKneelState dashKneel { get; private set; }

    public PlayerAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }



    #endregion

    //Luego los iniciamos aqui
    #region Awake


    protected override void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        dashStopState = new PlayerDashStopState(this, stateMachine, "DashStop");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        enterKneelState = new PlayerEnterKneelState(this, stateMachine, "EnterKneel");
        kneelState = new PlayerInKneelState(this, stateMachine, "InKneel");
        dashKneel = new PlayerDashKneelState(this, stateMachine, "DashKneel");
       

        primaryAttack = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

    }

    #endregion

    //Aqui iniciamos los componentes (desde el entity)
    #region Start
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    #endregion


    protected override void Update()
    {
        base.Update();  
        stateMachine.currentState.Update(); //Aqui accedemos al Update del Estado actual que la StateMachine se encuentre ejecutando y usamos ese Update para darle vida al player
        CheckForDashInput();    
    }


    //Otras Funciones:
    #region Corrutina isBussy

    public IEnumerator BussyFor(float _secodnds) 
    {
        isBussy = true;
        yield return new WaitForSeconds(_secodnds);
        isBussy = false;
    }

    #endregion
    #region Animation Finish Trigger
    public void AnimationTrigger() => stateMachine.currentState.AnimationTriggerFinish();

    #endregion
    #region Wall Detection in Kneel
    public bool IsKneelWallDetected() => Physics2D.Raycast(kneelWallCheck.position, Vector2.right * facingDir, kneelWallCheckDistance, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(kneelWallCheck.position, new Vector3(kneelWallCheck.position.x + kneelWallCheckDistance * facingDir, kneelWallCheck.position.y));
    }

    #endregion
    #region Dash Input
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashUsageTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0 && stateMachine.currentState == kneelState)
        {
            dashUsageTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDir;
            FlipController(dashDirection);

            stateMachine.ChangeState(dashKneel);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDir;
            FlipController(dashDirection);
            stateMachine.ChangeState(dashState);
        }
        
    }

    #endregion



}

