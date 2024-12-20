using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    public event Action<Vector2> Tapped;
    public event Action<Vector2> Released;
    public bool TapHeld { get; private set; } = false;
    public Vector2 StartTouchPosition { get; private set; } = new Vector2(0, 0);
    public Vector2 CurrentTouchPosition { get; private set; } = new Vector2(0,0);
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("Touched: " + touch.position);
                    TapHeld = true;
                    StartTouchPosition = touch.position;
                    CurrentTouchPosition = touch.position;

                    Tapped?.Invoke(touch.position);
                    break;

                case TouchPhase.Moved:
                    CurrentTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    //Debug.Log("Released: " + touch.position);
                    TapHeld = false;
                    StartTouchPosition = Vector2.zero;
                    CurrentTouchPosition = Vector2.zero;

                    Released?.Invoke(touch.position);
                    break;
            }
        }
    }
}
