using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed = 5f;
    public float jumpStrength = 30f;

    float horizontalMovement;

    bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       rb.linearVelocity = new Vector2(horizontalMovement*moveSpeed, rb.linearVelocityY);

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        animator.SetFloat("Y-Velocity", rb.linearVelocityY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpStrength);
        } else if (context.canceled)
        {
            rb.linearVelocityY *= .5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            animator.SetBool("isJumping", true);

        }
    }


}
