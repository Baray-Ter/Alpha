using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitShootPortal : MonoBehaviour
{
    public delegate void raycast(GameObject gameObject);
    public static event raycast RaycastHitGameObject;


    public GameObject BluePortal;
    public GameObject OrangePortal;

    public GameObject portalProjectileBlue;
    public GameObject portalProjectileOrange;

    private GameObject selectedProjectile;

    private bool sendSelectedPortal = false;

    private GameObject selectedPortal;

    private Vector2 selectedPortalEndPoint;
    private Vector2 OrbitPosition;
    private Vector2 portalScale;

    public float speed;

    private float time = 0;

    private void Awake()
    {
        portalScale = OrangePortal.transform.localScale = BluePortal.transform.localScale;
    }

    public void ShootBluePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _mousePosition - (Vector2)transform.position);

            Debug.DrawRay(transform.position, _mousePosition - (Vector2)transform.position, Color.red);      

            if (hit2D == false)
            {
                return;
            }

            Vector2 otherColliderBoundsMax = hit2D.collider.bounds.max;
            Vector2 otherColliderBoundsMin = hit2D.collider.bounds.min;

            Debug.Log(hit2D.collider.name);

            Vector2 hit2DPoint = hit2D.point;

            if (WidthControll(hit2D.transform.localScale, portalScale) && HeightControll(hit2D.transform.localScale, portalScale))
            {
                //remove it and put back otherColliderBoundsMax for an awsome bug
                float converter = Convert.ToSingle(Math.Round(otherColliderBoundsMax.x, 2, MidpointRounding.AwayFromZero));

                if (hit2DPoint.x >= converter && !sendSelectedPortal)
                {
                    selectedPortalEndPoint = new Vector2(otherColliderBoundsMax.x - (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherColliderBoundsMin, hit2DPoint, portalScale));
                    //for more pricise portal locations change portal scale with 

                    Debug.Log(selectedPortalEndPoint);

                    OrbitPosition = transform.position;

                    selectedPortal = BluePortal;
                    selectedProjectile = portalProjectileBlue;

                    sendSelectedPortal = true;

                    RaycastHitGameObject.Invoke(hit2D.collider.gameObject);

                    return; 
                }

                if (!sendSelectedPortal)
                {
                    selectedPortalEndPoint = new Vector2(otherColliderBoundsMin.x + (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherColliderBoundsMin, hit2DPoint, portalScale));

                    OrbitPosition = transform.position;

                    selectedPortal = BluePortal;
                    selectedProjectile = portalProjectileBlue;

                    sendSelectedPortal = true;
                }
            }
        }
    }

    //ORANGE PORTAL
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
            Vector2 otherColliderBoundsMin = hit2D.collider.bounds.min;

            Vector2 hit2DPoint = hit2D.point;

            Debug.DrawRay(transform.position, _mousePosition - (Vector2)transform.position, Color.red);

            if (WidthControll(hit2D.transform.localScale, portalScale) && HeightControll(hit2D.transform.localScale, portalScale))
            {
                //remove it and put back otherColliderBoundsMax for an awsome bug
                float converter = Convert.ToSingle(Math.Round(otherColliderBoundsMax.x, 2, MidpointRounding.AwayFromZero));

                if (hit2DPoint.x >= converter && !sendSelectedPortal)
                {
                    selectedPortalEndPoint = new Vector2(otherColliderBoundsMax.x - (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherColliderBoundsMin, hit2DPoint, portalScale));

                    OrbitPosition = transform.position;

                    selectedPortal = OrangePortal;
                    selectedProjectile = portalProjectileOrange;

                    sendSelectedPortal = true;

                    /* BluePortal.transform.position = new Vector2(otherColliderBoundsMax.x - (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherColliderBoundsMin, hit2DPoint, BluePortal));*/
                    return;
                }

                if (!sendSelectedPortal)
                {
                    selectedPortalEndPoint = new Vector2(otherColliderBoundsMin.x + (portalScale.x / 2),
                    CalculateMinAndMaxYAxis(otherColliderBoundsMax, otherColliderBoundsMin, hit2DPoint, portalScale));

                    OrbitPosition = transform.position;

                    selectedPortal = OrangePortal;
                    selectedProjectile = portalProjectileOrange;

                    sendSelectedPortal = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (sendSelectedPortal)
        {
            time += speed * Time.deltaTime;
            selectedProjectile.transform.position = Vector2.MoveTowards(OrbitPosition, selectedPortalEndPoint, time);

            if ((Vector2)selectedProjectile.transform.position == selectedPortalEndPoint)
            {
                selectedPortal.transform.position = selectedPortalEndPoint;

                time = 0;
                sendSelectedPortal = false;
            }
        }
    }

    private bool WidthControll(Vector2 targetObject, Vector2 portalScale) {

        if (targetObject.x >= portalScale.x)
        {
            return true;
        }

        return false;
    }

    private bool HeightControll(Vector2 targetObject, Vector2 portalScale)
    {

        if (targetObject.y >= portalScale.y)
        {
            return true;
        }

        return false;
    }

    //stops portal to go over walls (vertical walls)
    private float CalculateMinAndMaxYAxis(Vector2 otherColliderBoundsMax, Vector2 otherColliderBoundsMin, Vector2 raycastPoint, Vector2 portalScale)
    {
        float portalHalfLenght = portalScale.y / 2;

        if (portalHalfLenght + raycastPoint.y > otherColliderBoundsMax.y)
        {
            return otherColliderBoundsMax.y - portalHalfLenght;
        }

        if (raycastPoint.y - portalHalfLenght < otherColliderBoundsMin.y)
        {
            return otherColliderBoundsMin.y + portalHalfLenght;
        }

        return raycastPoint.y;
    }
}
