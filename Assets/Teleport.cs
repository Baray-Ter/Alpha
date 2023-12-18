using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnEnable() {

        OrbitShootPortal.RaycastHitGameObject += Balls;
    }

    void Balls(GameObject gameObject){

        Debug.Log(gameObject);
    }
}
