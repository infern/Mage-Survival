using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.InputAction;


public class ReadPlayerInput : MonoBehaviour
{
    #region Variables


    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;

    [Header("Data")]    /********/
    Vector2 runDirection;
    Vector2 aimDirection;


    private InputMap keyMap;

    #endregion

    #region Base Methods
    void Awake()
    {
        keyMap = new InputMap();

    }
    void OnEnable()
    {
        keyMap.Enable();
    }
    void OnDisable()
    {
        keyMap.Disable();
    }


    #endregion


     
    #region Action Inputs

    public void RunContext(CallbackContext context)
    {
        runDirection = context.ReadValue<Vector2>();
        player.runScript.direction = runDirection;


    }

    public void AttackContext(CallbackContext context)
    {
        if (context.started) player.attackScript.ButtonDown();
        if (context.canceled) player.attackScript.ButtonUp();

    }


    public void PauseContext(CallbackContext context)
    {
        if (context.started) EventManager.PauseGame();

    }


    #endregion

}
