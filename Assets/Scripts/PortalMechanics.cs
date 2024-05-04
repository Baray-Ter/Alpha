using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalMechanics : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //TeleportGameObject.Instance.momentum = other.gameObject.GetComponent<Rigidbody2D>().velocity;
        other.gameObject.layer = 7;
        //TeleportGameObject.Instance.OnPortalEnter(other.transform.localScale, other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //TeleportGameObject.Instance.momentum = Vector2.zero;
        //other.gameObject.layer = 6;
        //TeleportGameObject.Instance.OnPortalExit(other.transform.localScale, other.gameObject, gameObject);
    }
}
