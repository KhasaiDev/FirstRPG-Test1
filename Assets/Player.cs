using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;


    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;


    #region Instancias

    public PlayerStateMachine stateMachine { get; private set; }


    #endregion
    #region Componentes

    public Animator anim { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }


    #endregion
    #region Estados

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDashStopState dashStopState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    


    #endregion



    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        dashStopState = new PlayerDashStopState(this, stateMachine, "DashStop");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
       
    }


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInParent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

 
    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();    
    }




    //OTHER FUNCTIONS:
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    #region Dash Input
    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;   
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0) 
                dashDirection = facingDir;            
            
            stateMachine.ChangeState(dashState);
        }
    }

    #endregion
    #region SetVelocity
  
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rigidBody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Gizmos Wall y Ground
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    #endregion
    #region Flip n Controller

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }


    private void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    #endregion

}

