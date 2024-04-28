using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportGameObject : MonoBehaviour
{
    public GameObject OrangePortal;
    public GameObject BluePortal;

    public static TeleportGameObject Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Method that matches the delegate signature
    public void MyMethod(GameObject portal, GameObject exitingObject)
    {
        if (portal == BluePortal)
        {
            exitingObject.transform.position = OrangePortal.transform.position;
        }

        if (portal == OrangePortal)
        {
            exitingObject.transform.position = BluePortal.transform.position;
        }
    }
}
