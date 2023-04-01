using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public LayerMask SelectGroundLayer;
    public InputActionAsset actions;

    public Animator animator;

    private Rigidbody2D rb2d;
    private Vector2 moveDirection;

    public float speed;
    public float jumpPower;
    public float doubleJumpPower;
    public float gravity;

    private bool doubleJump;

    private BoxCollider2D boxCollider2D;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb2d.gravityScale = gravity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        transform.localScale = FlipSprite(moveDirection);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded())
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            animator.SetBool(AnimatorVariableNames.Jump, true);

            return;
        }

        if (context.started && doubleJump)
        {
            AirJump();
            doubleJump = false;
        }
    }

    public void AirJump(){

        rb2d.velocity = Vector2.zero;

        rb2d.AddForce(Vector2.up * doubleJumpPower, ForceMode2D.Impulse);

        animator.SetBool(AnimatorVariableNames.AirJump, true);
    }

    private void FallAnimation()
    {
        animator.SetBool(AnimatorVariableNames.Fall, true);
    }

    private Vector2 FlipSprite(Vector2 direction)
    {
        SelectAnimation(direction);

        if (direction.x != 0)
        {
            Vector2 flip = new Vector2(math.abs(transform.localScale.x) * direction.x, transform.localScale.y);

            return flip;
        }

        return transform.localScale;
    }
    
    //decides if player idling or running
    private void SelectAnimation(Vector2 direction)
    {
        animator.SetFloat(AnimatorVariableNames.Speed, Math.Abs(direction.x));
    }
    
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveDirection.x * speed, rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        animator.SetBool(AnimatorVariableNames.Jump, false);
        animator.SetBool(AnimatorVariableNames.Fall, false);
        animator.SetBool(AnimatorVariableNames.AirJump, false);

        doubleJump = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!animator.GetBool(AnimatorVariableNames.Jump))
        {
            animator.SetBool(AnimatorVariableNames.Fall, true);
        }
    }

    private bool IsGrounded(){

       return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, SelectGroundLayer);
    }

    private class AnimatorVariableNames
    {
        public static string Speed = "Speed";
        public static string Jump = "Jump";
        public static string Fall = "Fall";
        public static string AirJump = "AirJump";
    }

    private class AnimatorAnimationNames{

        public static string Speed = "Player_idle";
        public static string Jump = "Player_Jump";
        public static string Fall = "Player_Fall";
        public static string AirJump = "Player_AirJump";
    }
}