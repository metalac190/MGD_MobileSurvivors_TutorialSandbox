using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions _inputSystemActions;

    public Action<Vector2> TouchStarted;
    public Action<Vector2> TouchEnded;
    public Vector2 TouchStartPosition { get; private set; }
    public Vector2 TouchCurrentPosition { get; private set; }
    public bool TouchHeld { get; private set; } = false;

    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.TouchPoint.performed += OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled += OnTouchCancelled;
    }

    private void OnDisable()
    {
        _inputSystemActions.Player.TouchPoint.performed -= OnTouchPerformed;
        _inputSystemActions.Player.TouchPoint.canceled -= OnTouchCancelled;
        _inputSystemActions.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch");
        TouchHeld = true;
        Vector2 TouchPosition 
            = _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();
        TouchStartPosition = TouchPosition;
        TouchCurrentPosition = TouchPosition;

        TouchStarted?.Invoke(TouchPosition);
        //Debug.Log("Touch Start Pos: " + TouchStartPosition);
    }
    private void OnTouchCancelled(InputAction.CallbackContext context)
    {
        //TODO figure out how to call this
        //Debug.Log("Release");
        TouchHeld = false;
        Vector2 EndPosition 
            = _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();
        TouchEnded?.Invoke(EndPosition);

        TouchStartPosition = Vector2.zero;
        TouchCurrentPosition = Vector2.zero;
    }

    private void Update()
    {
        if (TouchHeld)
        {
            TouchCurrentPosition 
                = _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();
            //Debug.Log("CurrenPos: " + TouchCurrentPosition);
        }
    }
}
