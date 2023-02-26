using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset actions;

    public Animator animator;

    private Rigidbody2D rb2d;
    private Vector2 moveDirection;

    public float speed;

    class AnimatorArrayNames
    {
        public static string Speed = "Speed";
    }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        transform.localScale = FlipSprite(moveDirection);
    }

    public Vector2 FlipSprite(Vector2 direction)
    {
        SelectAnimation(direction);

        if (direction.x != 0)
        {
            Vector2 flip = new Vector2(math.abs(transform.localScale.x) * direction.x, transform.localScale.y);

            return flip;
        }

        return transform.localScale;
    }

    private void SelectAnimation(Vector2 direction)
    {
        animator.SetFloat(AnimatorArrayNames.Speed, Math.Abs(direction.x));
    }


    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
}
