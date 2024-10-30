using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float swipeThreshold;
    private BoardManager BoardManager => BoardManager.Instance;
    private Vector2 _startTouchPosition, _endTouchPosition;
    [SerializeField] private TouchDirection testDirection;
    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BoardManager.OnMove(TouchDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BoardManager.OnMove(TouchDirection.Down);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            BoardManager.OnMove(TouchDirection.Up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BoardManager.OnMove(TouchDirection.Right);
        }
        
        // if (!Input.GetMouseButton(0)) return;
        // _startTouchPosition = Input.mousePosition;
        // if (Input.GetMouseButtonUp(0))
        // {
        //     _endTouchPosition = Input.mousePosition;
        //     DetectSwipeDirection();
        // }
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouchPosition = touch.position;
                break;

            case TouchPhase.Ended:
                _endTouchPosition = touch.position;
                DetectSwipeDirection();
                break;
        }
    }

    private void DetectSwipeDirection()
    {
        var swipeDirection = _endTouchPosition - _startTouchPosition;
        if (!(swipeDirection.magnitude >= swipeThreshold)) return;
        var x = swipeDirection.x;
        var y = swipeDirection.y;
        TouchDirection direction;
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            direction = x > 0 ? TouchDirection.Right : TouchDirection.Left;
        }
        else
        {
            direction = y > 0 ? TouchDirection.Up : TouchDirection.Down;
        }
        BoardManager.OnMove(direction);
    }
}