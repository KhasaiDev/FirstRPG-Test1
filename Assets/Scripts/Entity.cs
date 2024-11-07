using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Componentes

    public Animator anim { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }
    public EntityFlashFX fx {get; private set;}

    #endregion

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform frontGroundCheck;
    [SerializeField] protected Transform backGroundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDireccion;
    protected bool isKnocked;


    protected virtual void Awake()
    {

    }


    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInParent<Rigidbody2D>();
        fx = GetComponent<EntityFlashFX>();
    }


    protected virtual void Update()
    {
        
    }

    //Otras funciones que heredan al Player y a los Enemigos
    #region SetVelocity

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;

        rigidBody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region ZeroVelocity

    public void ZeroVelocity()
    {
        if (isKnocked)
            return;

        rigidBody.velocity = new Vector2(0, 0);
    }

    #endregion
    #region Gizmos

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(frontGroundCheck.position, new Vector3(frontGroundCheck.position.x, frontGroundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(backGroundCheck.position, new Vector3(backGroundCheck.position.x, backGroundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion
    #region Ground & Wall Raycast


    public virtual bool IsFrontGroundDetected() => Physics2D.Raycast(frontGroundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsBackGroundDetected() => Physics2D.Raycast(backGroundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);


    #endregion
    #region Flip Info

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    #endregion
    #region Damage
    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name + "  ha sido dañado");
    }
    #endregion

    protected virtual IEnumerator HitKnockback()
    { 
        isKnocked = true;

        rigidBody.velocity =  new Vector2(knockbackDireccion.x * -facingDir, knockbackDireccion.y);

        yield return new WaitForSeconds(.07f);

        isKnocked = false;
    }
}
