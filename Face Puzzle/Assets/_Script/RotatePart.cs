using System;
using DG.Tweening;
using UnityEngine;

public class RotatePart : MonoBehaviour
{
    public GameObject target;
    public bool isTouching;
    public LayerMask layerMask;
    public bool isFacingFront;
    public static RotatePart Instance;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Vector3 previousMousePos;
    private Vector3 mouseDelta;

    [SerializeField] private float speed = 500f;
    [SerializeField] private bool canSwipeUpDown;
    [SerializeField] private bool canSwipeLeftRight;

    public bool isTouchable;

    public ChangeColor changeColor;
    public PuzzleState puzzleState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        changeColor = GetComponent<ChangeColor>();
        puzzleState = GetComponentInParent<PuzzleState>();
    }

    private void Update()
    {
        Drag();
        Swipe();
        CheckIfIsTouchable();
        CheckIfFacingFront();
        ChangeColor();
       
    }

    private void ChangeColor()
    {
        if (isFacingFront)
        {
            changeColor.ColorChangeToWin();
        }
        else
        {
            changeColor.ColorChangeToLose();
        }
    }

    void CheckIfIsTouchable()
    {
        if (isTouchable == false)
        {
            isTouching = false;
        }
    }

    private void CheckIfFacingFront()
    {
        var roundX = (int) (target.transform.rotation.x) == 0;
        var roundY = (int) (target.transform.rotation.y) == 0;
        var roundZ = (int) (target.transform.rotation.z) == 0;
        if (roundX && roundY && roundZ)
        {
            isFacingFront = true;
        }
        else
        {
            isFacingFront = false;
        }
    }

    void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            //While the mouse is held down the cube can be moved around its central axis to provide visual feedback
            mouseDelta = Input.mousePosition - previousMousePos;
            mouseDelta *= 0.1f; // Reduction of rotation speed
            if (canSwipeUpDown && isTouching)
            {
                transform.rotation = Quaternion.Euler(-mouseDelta.y, 0, 0) * transform.rotation;
            }

            else if (canSwipeLeftRight && isTouching)
            {
                transform.rotation = Quaternion.Euler(0, -mouseDelta.x, 0) * transform.rotation;
            }
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
            else
            {
                isTouching = false;
            }
        }

        previousMousePos = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Get the 2D position of the first mouse click
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask) ? isTouching = true : isTouching = false) ;
        }

        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe =
                new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            if (canSwipeUpDown && isTouching)
            {
                if (SwipeDown(currentSwipe))
                {
                    target.transform.Rotate(180, 0, 0, Space.World);
                }

                else if (SwipeUp(currentSwipe))
                {
                    target.transform.Rotate(-180, 0, 0, Space.World);
                }

                else
                {
                    puzzleState.transform.DOShakePosition(0.25f, 0.01f, 20, 1, false);
                    Handheld.Vibrate();
                }
            }

            if (canSwipeLeftRight && isTouching)
            {
                if (SwipeLeft(currentSwipe))
                {
                    target.transform.Rotate(0, 180, 0);
                }

                else if (SwipeRight(currentSwipe))
                {
                    target.transform.Rotate(0, -180, 0);
                }
                else
                {
                    puzzleState.transform.DOShakePosition(0.25f, 0.01f, 20, 1, false);
                    Handheld.Vibrate();
                }
            }
        }

        SetFrontCounter();
    }

    private void SetFrontCounter()
    {
        if (puzzleState.isFunctionCalled)
        {
            puzzleState.isFunctionCalled = false;
        }

        if (!puzzleState.isFunctionCalled)
        {
            puzzleState.frontTimeCounter.Clear();
            puzzleState.UpdatePercentage();
        }
    }

    bool SwipeDown(Vector2 swipe)
    {
        return currentSwipe.y < 0 && -0.5f < currentSwipe.x && currentSwipe.x < 0.5f;
    }

    bool SwipeUp(Vector2 swipe)
    {
        return currentSwipe.y > 0 && -0.5f < currentSwipe.x && currentSwipe.x < 0.5f;
    }

    bool SwipeLeft(Vector2 swipe)
    {
        return currentSwipe.x < 0 && -0.5f < currentSwipe.y && currentSwipe.y < 0.5f;
    }

    bool SwipeRight(Vector2 swipe)
    {
        return currentSwipe.x > 0 && -0.5f < currentSwipe.y && currentSwipe.y < 0.5f;
    }
}