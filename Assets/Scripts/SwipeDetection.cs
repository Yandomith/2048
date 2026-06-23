using UnityEngine;

public class SwipeDetection : MonoBehaviour
{



    [SerializeField]
    private float minSwipeDistance = 0.2f;// Minimum distance for a swipe to be considered valid

    [SerializeField]
    private float maxSwipeTime = 1f; // Maximum time for a swipe to be considered valid

    [SerializeField]
    private float swipeThreshold = 0.5f; // Threshold for determining swipe direction

    private Vector2 startTouchPosition;
    private float startTouchTime;

    private Vector2 endTouchPosition;

    private float endTouchTime;



    private void OnEnable()
    {
        TouchInputManager.OnTouchStartedEvent += SwipeStart;
        TouchInputManager.OnTouchEndedEvent += SwipeEnd;
    }

    private void OnDisable()
    {
        TouchInputManager.OnTouchStartedEvent -= SwipeStart;
        TouchInputManager.OnTouchEndedEvent -= SwipeEnd;
    }

    private void SwipeStart(Vector2 touchPosition, float touchTime)
    {
        Debug.Log("Swipe Started at: " + touchPosition + " Time: " + touchTime);
        startTouchPosition = touchPosition;
        startTouchTime = touchTime;


    }

    private void SwipeEnd(Vector2 touchPosition, float touchTime)
    {
        Debug.Log("Swipe Ended at: " + touchPosition + " Time: " + touchTime);
        endTouchPosition = touchPosition;
        endTouchTime = touchTime;
        DetectSwipe();

    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startTouchPosition, endTouchPosition) >= minSwipeDistance && (endTouchTime - startTouchTime) <= maxSwipeTime)
        {
            Vector2 swipeDirection = (endTouchPosition - startTouchPosition).normalized;
            Debug.Log("Swipe Detected! Direction: " + swipeDirection);
            SwipeDirection(swipeDirection);
            // You can add additional logic here to handle the swipe direction
            Debug.DrawLine(startTouchPosition, endTouchPosition, Color.red, 5f); // Draw a line in the Scene view for visualization
        }
        else
        {
            Debug.Log("Swipe not detected. Either too short or too slow.");
        }
    }

    private void SwipeDirection(Vector2 swipeDirection)
    {
        if (Vector2.Dot(swipeDirection, Vector2.up) > swipeThreshold)
        {
            Debug.Log("Swipe Up");
            GameManager.Instance.board.MoveTiles(Vector2Int.up, 0, 1, 1, 1);

        }
        else if (Vector2.Dot(swipeDirection, Vector2.down) > swipeThreshold)
        {
            Debug.Log("Swipe Down");
            GameManager.Instance.board.MoveTiles(Vector2Int.down, 0, 1, GameManager.Instance.board.grid.height - 2, -1);

        }
        else if (Vector2.Dot(swipeDirection, Vector2.left) > swipeThreshold)
        {
            Debug.Log("Swipe Left");
            GameManager.Instance.board.MoveTiles(Vector2Int.left, 1, 1, 0, 1);

        }
        else if (Vector2.Dot(swipeDirection, Vector2.right) > swipeThreshold)
        {
            Debug.Log("Swipe Right");
            GameManager.Instance.board.MoveTiles(Vector2Int.right, GameManager.Instance.board.grid.width - 2, -1, 0, 1);

        }
    }
}
