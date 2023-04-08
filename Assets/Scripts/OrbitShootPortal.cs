using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitShootPortal : MonoBehaviour
{
    public GameObject BluePortal;
    public GameObject OrangePortal;

    private Vector2 _mousePosition;

    private Vector2 _portalLenghtAndWith;

    public void ShootBluePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

            if (hit2D == false)
            {
                return;
            }

            Vector2 otherColliderBoundsMax = hit2D.collider.bounds.max;
            Vector2 otherCOllidersBoundsMin = hit2D.collider.bounds.min;

            Vector2 portalScale = BluePortal.transform.localScale;

            Vector2 RaycastHitPoint = hit2D.point;

            if (WidthControll(hit2D.transform.localScale, BluePortal) && HeightControll(hit2D.transform.localScale, BluePortal))
            {
                if (RaycastHitPoint.x >= otherColliderBoundsMax.x)
                {
                    BluePortal.transform.position = new Vector2(otherColliderBoundsMax.x - (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherCOllidersBoundsMin, RaycastHitPoint, BluePortal));
                    return;
                }

                BluePortal.transform.position = new Vector2(otherCOllidersBoundsMin.x + (portalScale.x / 2),
                CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherCOllidersBoundsMin, RaycastHitPoint, BluePortal));
            }
        }
    }

    public void ShootOrangePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

            if (hit2D == false)
            {
                return;
            }

            Vector2 otherColliderBoundsMax = hit2D.collider.bounds.max;
            Vector2 otherCOllidersBoundsMin = hit2D.collider.bounds.min;

            Vector2 portalScale = OrangePortal.transform.localScale;

            Vector2 RaycastHitPoint = hit2D.point;

            if (WidthControll(hit2D.transform.localScale, OrangePortal) && HeightControll(hit2D.transform.localScale, OrangePortal))
            {
                if (RaycastHitPoint.x >= otherColliderBoundsMax.x)
                {
                    OrangePortal.transform.position = new Vector2(otherColliderBoundsMax.x - (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherCOllidersBoundsMin, RaycastHitPoint, BluePortal));
                    return;
                }

                OrangePortal.transform.position = new Vector2(otherCOllidersBoundsMin.x + (portalScale.x / 2),
                CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherCOllidersBoundsMin, RaycastHitPoint, BluePortal));
            }
        }
    }

    private bool WidthControll(Vector2 targetObject, GameObject portal){

        if (targetObject.x >= portal.transform.localScale.x)
        {
            return true;
        }

        return false;
    }

    private bool HeightControll(Vector2 targetObject, GameObject portal){

        if (targetObject.y >= portal.transform.localScale.y)
        {
            return true;
        }

        return false;
    }

    //stops portal to go over walls (vertical walls)
    private float CalculateMinAndMaxYAxis(Vector2 maxBounds, Vector2 minBounds, Vector2 raycastPoint, GameObject portal){

        float portalHalfLenght = portal.transform.localScale.y / 2;

        if (portalHalfLenght + raycastPoint.y > maxBounds.y)
        {
            return maxBounds.y - portalHalfLenght;
        }

        if (raycastPoint.y - portalHalfLenght < minBounds.y)
        {
            return minBounds.y + portalHalfLenght;
        }

        return raycastPoint.y;
    }
}
