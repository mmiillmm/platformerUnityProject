using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] private float jumpHeight = 150;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isFacingRight;
    private bool isGrounded;

    private void Start()
    {
        isFacingRight = true;
        isGrounded = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            rb.AddForce(new(0, jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(move));

        rb.velocity = new(
            x: move * maxSpeed,
            rb.velocity.y);

        if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, .1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("vertical speed", rb.velocity.y);
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }
}