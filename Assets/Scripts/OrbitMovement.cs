using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitMovement : MonoBehaviour
{
    public Transform Player;

    [Tooltip("Higher is faster")]
    public float speed;

    private Rigidbody2D _rb2d;
    private Vector2 _refVelocity;

    private void Start() {
        
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {

        transform.position = Vector2.SmoothDamp(transform.position, Player.position, ref _refVelocity, speed);
    }

    public void MoveCommand(InputAction.CallbackContext context){

        if (context.started)
        {
            Debug.Log(context);
        }
    }
}
