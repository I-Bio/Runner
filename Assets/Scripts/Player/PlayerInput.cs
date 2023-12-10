using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public event Action<MoveType> Moved;
    
    public enum MoveType
    {
        Left,
        Right,
        Up,
        Down
    }
    
    private void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    break;
                
                case TouchPhase.Ended:
                    _endTouchPosition = touch.position;
                    float horizontalLength = Math.Abs(_endTouchPosition.x - _startTouchPosition.x);
                    float verticalLength = Math.Abs(_endTouchPosition.y - _startTouchPosition.y);
                    
                    if (horizontalLength >= verticalLength)
                    {
                        if (_endTouchPosition.x < _startTouchPosition.x)
                            Moved?.Invoke(MoveType.Right);

                        if (_endTouchPosition.x > _startTouchPosition.x)
                            Moved?.Invoke(MoveType.Left);
                    }
                    else
                    {
                        if (_endTouchPosition.y < _startTouchPosition.y)
                            Moved?.Invoke(MoveType.Down);
                            
                        if (_endTouchPosition.y > _startTouchPosition.y)
                            Moved?.Invoke(MoveType.Up);
                    }

                    break;
            }
        }
    }
}
