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

    public UnityEvent changeState;

    private bool canMove;
    private void Start()
    {
        
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
        changeState?.Invoke();
        moveSpeed = waterSpeed;
        rb.gravityScale = 0.5f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.sharedMaterial = waterMaterial;
        rb.mass = 5f;

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
        rb.mass = 30f;

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
        rb.mass = 1f;
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
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ClimateManager.Instance.currentState == ClimateManager.State.Gas && collision.gameObject.tag != "Wind")
        {
            ClimateManager.Instance.SetState(ClimateManager.State.Water);
        }
    }


    public void StopMovement()
    {
        canMove = false;
    }

    public void RestartMovement()
    {
        canMove = true;
    }
}
