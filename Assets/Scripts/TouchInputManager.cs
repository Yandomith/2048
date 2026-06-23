using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputManager : MonoBehaviour
{

    public static TouchInputManager Instance { get; private set; }
    private CustomInput_Action customInputAction;
    private Camera mainCamera;

    #region Events
    public delegate void TouchStartedEvent(Vector2 touchPosition, float touchTime);
    public static event TouchStartedEvent OnTouchStartedEvent;

    public delegate void TouchEndedEvent(Vector2 touchPosition, float touchTime);
    public static event TouchEndedEvent OnTouchEndedEvent;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        customInputAction = new CustomInput_Action();
        mainCamera = Camera.main;

        if (customInputAction == null)
        {
            Debug.LogError("CustomInput_Action reference is missing!");
        }
    }

    private void OnEnable()
    {
        customInputAction.Enable();
    }

    private void OnDisable()
    {
        customInputAction.Disable();
    }

    private void Start()
    {
        customInputAction.Touch.TouchPress.started += ctx => OnTouchStarted(ctx);
        customInputAction.Touch.TouchPress.canceled += ctx => OnTouchEnded(ctx);
    }

    private void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        if (OnTouchStartedEvent != null)
        {
            OnTouchStartedEvent(Utils.ScreenToWorldPoint(mainCamera, customInputAction.Touch.TouchPressPos.ReadValue<Vector2>()), (float)ctx.startTime);
        }
    }

    private void OnTouchEnded(InputAction.CallbackContext ctx)
    {
        if (OnTouchEndedEvent != null)
        {
            OnTouchEndedEvent(Utils.ScreenToWorldPoint(mainCamera, customInputAction.Touch.TouchPressPos.ReadValue<Vector2>()), (float)ctx.time);
        }
    }


    private Vector2 GetTouchPosition()
    {
        return Utils.ScreenToWorldPoint(mainCamera, customInputAction.Touch.TouchPressPos.ReadValue<Vector2>());
    }
}