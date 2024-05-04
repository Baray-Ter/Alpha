using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExit : MonoBehaviour
{
    /*    private void OnTriggerEnter2D(Collider2D other)
        {
            TeleportGameObject.Instance.OnPortalExit(other.gameObject.GetComponent<Collider2D>().bounds, other.gameObject, gameObject.transform.parent.gameObject);
        }*/

    /*    void OnCollisionStay(Collision collision)
        {
            TeleportGameObject.Instance.OnPortalExit(collision.gameObject.GetComponent<Collider2D>().bounds, collision.gameObject, gameObject.transform.parent.gameObject);
            foreach (ContactPoint contact in collision.contacts)
            {
                print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                // Visualize the contact point
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }*/

    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            print(collision.GetContact(0));

            ContactPoint2D[] contactPoint = collision.GetContact();

            foreach (ContactPoint2D contact in collision.contacts)
            {
                print(contact.collider.name + " hit " + contact.otherCollider.name);
                // Visualize the contact point
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }

            TeleportGameObject.Instance.OnPortalExit(collision.gameObject.GetComponent<Collider2D>().bounds, collision.gameObject, gameObject.transform.parent.gameObject);

        }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // I only want one contact, so that's why I initialise it with capacity 1
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);
        // you can get the point with this:
        var contactPoint = contacts[0].point;
        //"(5.89, -3.97)"

        TeleportGameObject.Instance.OnPortalExit(contactPoint, collision.gameObject, gameObject.transform.parent.gameObject);

        //TeleportGameObject.Instance.OnPortalExit(kontakt, collision.gameObject, gameObject.transform.parent.gameObject);
        //ContactPoint2D contactPoint2D = collision.GetContact(1);
    }
}
