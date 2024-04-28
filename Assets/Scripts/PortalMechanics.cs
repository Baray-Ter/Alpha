using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalMechanics : MonoBehaviour
{
    TeleportGameObject teleport;

    private Vector2 m_EnteringPosition;
    private Vector2 m_ExitingPosition;

    private void Start()
    {
        teleport = gameObject.AddComponent<TeleportGameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_EnteringPosition = other.transform.localScale;
        other.gameObject.layer = 7;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        m_ExitingPosition = other.transform.localScale;
        other.gameObject.layer = 6;

        if (m_EnteringPosition.x == m_ExitingPosition.x)
        {
            TeleportGameObject.Instance.MyMethod(gameObject, other.gameObject);
        }
    }
}
