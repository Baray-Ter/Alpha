using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPos : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerStartPos;
    
    private void Awake()
    {
        playerStartPos = player.transform.position;
    }

    public void onClick()
    {
        player.transform.position = playerStartPos;
    }
}
