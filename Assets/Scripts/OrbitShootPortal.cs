using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitShootPortal : MonoBehaviour
{
    public GameObject BluePortal;
    public GameObject OrangePortal;

    private Vector2 _mousePosition;

    public void ShootBluePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {   
            Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

            if (hit2D)
            {
                Instantiate(BluePortal, hit2D.point, Quaternion.identity);
            }
        }
        
    }

    private void FixedUpdate() {

        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

        Debug.DrawLine(transform.position, hit2D.point);
    }

    public void ShootOrangePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {   
            Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

            if (hit2D)
            {
                Instantiate(OrangePortal, hit2D.point, Quaternion.identity);
            }
        }
    }
}
