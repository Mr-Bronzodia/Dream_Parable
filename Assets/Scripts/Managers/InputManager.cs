using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputControls _inputActions;

    public InputControls InputAction { get { return _inputActions; } }


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

    private void OnEnable()
    {
        _inputActions = new InputControls();
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

}
