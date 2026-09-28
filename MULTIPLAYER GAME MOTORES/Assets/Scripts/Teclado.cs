using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Teclado : MonoBehaviour
{
    public PlayerInput playerInput;
    
    private void Start()
    {
        InputUser.PerformPairingWithDevice(Keyboard.current, playerInput.user, InputUserPairingOptions.None);
        Debug.Log(playerInput.currentControlScheme.ToString());
    }
}
