using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportGameObject : MonoBehaviour
{
    public GameObject OrangePortal;
    public GameObject BluePortal;

    private Vector2 m_EnteringPosition;
    private Vector2 m_ExitingPosition;

    public Vector2 momentum;

    public static TeleportGameObject Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnPortalEnter(Vector2 localScale, GameObject referencedGameObject)
    {
        referencedGameObject.layer = 7;
        m_EnteringPosition = localScale;
    }

    public void OnPortalExit(Vector2 colliderContactPosition, GameObject referencedGameObject, GameObject portal)
    {
        referencedGameObject.layer = 6;


        print("Entrence " + portal.GetComponent<Collider2D>().ClosestPoint(colliderContactPosition));
        CalculateTeleportation(colliderContactPosition, portal, referencedGameObject);
    }

    // Method that matches the delegate signature
    public void CalculateTeleportation(Vector2 colliderContactPosition, GameObject portal, GameObject referencedGameObject)
    {
        if (portal == BluePortal)
        {
            //referencedGameObject.GetComponent<Rigidbody2D>().velocity = momentum;
            referencedGameObject.transform.position = OrangePortal.GetComponent<Collider2D>().ClosestPoint(colliderContactPosition * new Vector2(-1, 1));
            print("Exit " + (Vector2)referencedGameObject.transform.position);
            //referencedGameObject.transform.position = new Vector2(OrangePortal.GetComponent<Collider2D>().bounds.min);
        }

        if (portal == OrangePortal)
        {
            //referencedGameObject.GetComponent<Rigidbody2D>().velocity = momentum;
            //print(OrangePortal.GetComponent<Collider2D>().ClosestPoint(colliderContactPosition));
            referencedGameObject.transform.position = BluePortal.GetComponent<Collider2D>().ClosestPoint(colliderContactPosition);
        }

        m_EnteringPosition = Vector2.zero;
        m_ExitingPosition = Vector2.zero;
    }
}
