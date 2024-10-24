using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region Componentes

    public PlayerStateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }


    #endregion
    public bool isBussy { get; private set; }



    [Header("Attack Info")]
    public float[] attackMovement;


    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;


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
    [SerializeField] private Transform groundCheck2;



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

    public PlayerAttackState primaryAttack { get; private set; }
    


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
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        enterKneelState = new PlayerEnterKneelState(this, stateMachine, "EnterKneel");
        kneelState = new PlayerInKneelState(this, stateMachine, "InKneel");
       

        primaryAttack = new PlayerAttackState(this, stateMachine, "Attack");

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

    #region ZeroVelocity

    public void ZeroVelocity() 
    {
        rigidBody.velocity = new Vector2(0, 0);
    }

    #endregion
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
    #region Ground & Wall Detection
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsGround2Detected() => Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    #endregion
    #region Ground & Wall Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck2.position, new Vector3(groundCheck2.position.x, groundCheck2.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }

    #endregion
    #region Dash Input
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

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
    #region Flip Info

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

