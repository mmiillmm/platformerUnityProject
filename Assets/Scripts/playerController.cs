using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 7;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isFacingRight;
    private float jump;

    private void Start()
    {
        isFacingRight = true;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y);
    }
}
