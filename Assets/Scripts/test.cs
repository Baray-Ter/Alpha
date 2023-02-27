using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAudio), typeof(PlayerInput),
typeof(PlayerMovementA))]
public class test : MonoBehaviour
{
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovementA playerMovement;

    public float speed = 1.0f;

    public string balls;
    private void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovementA>();
    }
}
public class PlayerAudio : MonoBehaviour
{
    public test testtest;

    private void Start()
    {
        testtest.speed
    }
}
public class PlayerInput : MonoBehaviour
{

}
public class PlayerMovementA : MonoBehaviour
{

}