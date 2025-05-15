using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static NewActions;

public interface IInputReader
{
    void EnablePlayerActions();
    void DisablePlayerActions();
}


[CreateAssetMenu(fileName = "InputReader", menuName = "Gameplay/InputReader", order = 0)]
public class InputReader : ScriptableObject, IInputReader, IGameplayActions
{
    public event Action One;
    public event Action Two;
    public event Action Rotation;
    
    public Vector2 mouseDelta => _inputActions.Gameplay.Delta.ReadValue<Vector2>();
    public Vector2 move => _inputActions.Gameplay.Move.ReadValue<Vector2>();
    public bool rotateIsBeingPressed => _inputActions.Gameplay.Rotate.inProgress;
    public bool speedUpIsBeingPressed => _inputActions.Gameplay.SpeedUp.inProgress;
    public float upDown => _inputActions.Gameplay.UpDown.ReadValue<float>();

    NewActions _inputActions;

    
    
    public void EnablePlayerActions()
    {
        if (_inputActions == null)
        {
            _inputActions = new NewActions();
            _inputActions.Gameplay.SetCallbacks(this);
        }
        _inputActions.Enable();
    }

    public void DisablePlayerActions()
    {
        _inputActions.Disable();
    }
    
    // -----------------
    
    public void OnRotate(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
        if (context.phase == InputActionPhase.Started)
        {
            Rotation?.Invoke();
        }
    }

    public void OnDelta(InputAction.CallbackContext context)
    {
        // Debug.Log(context.ReadValue<Vector2>());
        // throw new System.NotImplementedException();
    }

    public void On_1(InputAction.CallbackContext context)
    {
        // Debug.Log(context.ReadValue<bool>());
        if (context.phase == InputActionPhase.Started)
        {
            One?.Invoke();
        }
    }

    public void On_2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Two?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnSpeedUp(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }
}