using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private float moveSpeed;

    [SerializeField] private float waterSpeed;
    [SerializeField] private float gasSpeed;
    [SerializeField] private float iceSpeed;

    [SerializeField] private float jumpForce;

    private bool canJump;

    [SerializeField] private Sprite cloudSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite iceSprite;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D waterMaterial;

    private CircleCollider2D waterCollider;
    private BoxCollider2D iceCollider;
    private PolygonCollider2D cloudCollider;

    [SerializeField] Transform teleportPosition;

    [SerializeField] float teleportDelay = 1.0f;

    [SerializeField] float iceMass;
    [SerializeField] float gasMass;
    [SerializeField] float waterMass;
    private bool isFacingRight;



    public UnityEvent changeState;
    [SerializeField] float gizmoRadius = 0.5f; // Adjust this to change gizmo size

    private Animator anim;


    private bool canMove;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        waterCollider = GetComponent<CircleCollider2D>();
        iceCollider = GetComponent<BoxCollider2D>();
        cloudCollider = GetComponent<PolygonCollider2D>();
    }

    public void OnWater()
    {
        anim.runtimeAnimatorController = Resources.Load("Assets/Animations/Water_Player.controller") as RuntimeAnimatorController;
        changeState?.Invoke();
        moveSpeed = waterSpeed;
        rb.gravityScale = 0.5f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.sharedMaterial = waterMaterial;
        rb.mass = waterMass;

        iceCollider.enabled = false;
        cloudCollider.enabled = false;
        waterCollider.enabled = true;

        canJump = false;
        playerSprite.sprite = waterSprite;
    }

    public void OnIce()
    {
        changeState?.Invoke();
        moveSpeed = iceSpeed;
        rb.gravityScale = 1f;
        rb.angularDrag = 0.2f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.sharedMaterial = iceMaterial;
        rb.mass = iceMass;

        iceCollider.enabled = true;
        cloudCollider.enabled = false;
        waterCollider.enabled = false;


        canJump = true;
        playerSprite.sprite = iceSprite;

    }

    public void OnGas()
    {
        changeState?.Invoke();
        moveSpeed = gasSpeed;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.mass = gasMass;
        iceCollider.enabled = false;
        cloudCollider.enabled = true;
        waterCollider.enabled = false;

        canJump = false;
        playerSprite.sprite = cloudSprite;

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Flip();
        if (!IsGrounded())
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }


        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal != 0)
            {
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }

            if (ClimateManager.Instance.currentState >0)
            {
                rb.AddForce(new Vector2(moveSpeed * horizontal, 0), ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            }

            if (IsGrounded() && Input.GetKeyDown("space") && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

    }


    private void Flip()
    {
        if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ClimateManager.Instance.currentState == ClimateManager.State.Gas && collision.gameObject.tag != "Wind")
        {
            ClimateManager.Instance.SetState(ClimateManager.State.Water);
        }
    }


    public void Die()
    {
        canMove = false;
        Invoke("TeleportWithDelay", teleportDelay);
    }

    public void StopMovement()
    {
        canMove = false;
        rb.velocity = new Vector2 (0, 0);
    }

    public void RestartMovement()
    {
        canMove = true;

    }

    private void TeleportWithDelay()
    {
        this.transform.position = teleportPosition.position;
        RestartMovement();
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo sphere to visualize the teleport position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportPosition.position, gizmoRadius);
    }


}
