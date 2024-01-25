using UnityEngine;

public class SwipeDetection2 : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;

    private InputManager inputManager;
    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    public static SwipeDetection2 instance;
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }
    public void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition)>= minimumDistance && (endTime-startTime)<= maximumTime)
        {
            //Debug.Log("DetectSwipe");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    public delegate void SwipeAction(Vector2 direction);
    public event SwipeAction OnSwipe;
    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("SwipeUp");
            block.instance.MoveUp2();
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("SwipeDown");
            //OnSwipe?.Invoke(Vector2.down);
            block.instance.MoveDown2();
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("SwipeRight");
            //OnSwipe?.Invoke(Vector2.right);
            block.instance.MoveRight2();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("SwipeLeft");
            //OnSwipe?.Invoke(Vector2.left);
            block.instance.MoveLeft2();
        }
    }
}

