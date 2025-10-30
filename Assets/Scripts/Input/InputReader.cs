using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public bool IsShooting { get; private set; }
    public event Action DodgeEvent;
    public event Action ReloadEvent;
    private Controls _controls;

    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsShooting = true;
        }
        else if (context.canceled)
        {
            IsShooting = false;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ReloadEvent?.Invoke();
    }
}
