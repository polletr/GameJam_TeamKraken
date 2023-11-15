using System.Collections;
using System.Collections.Generic;
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

    public UnityEvent changeState;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    public void OnWater()
    {
        changeState?.Invoke();
        moveSpeed = waterSpeed;
        rb.gravityScale = 0.5f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (IsGrounded() && Input.GetKeyDown("space") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
