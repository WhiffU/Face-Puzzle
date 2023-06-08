using System;
using UnityEngine;

public class RotatePart : MonoBehaviour
{
    public GameObject target;
    public bool isFacingFront;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Vector3 previousMousePos;
    private Vector3 mouseDelta;

    [SerializeField] private float speed = 400f;

    private MeshRenderer _meshRenderer;

    [SerializeField] private bool canSwipeUpDown;

    private bool isDragging = false;
    private Rigidbody rb;

    private void Start()
    {
        isFacingFront = false;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        isDragging = true;
    }

    private void Update()
    {
        Swipe();
        Drag();
    }


    void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            //While the mouse is held down the cube can be moved around its central axis to provide visual feedback
            mouseDelta = Input.mousePosition - previousMousePos;
            mouseDelta *= 1f; // Reduction of rotation speed
            if (canSwipeUpDown)
            {
                transform.rotation = Quaternion.Euler(mouseDelta.x, 0, 0) * transform.rotation;
            }
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe =
                new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            if (SwipeDown(currentSwipe))
            {
                target.transform.Rotate(-180, 0, 0, Space.World);
                
            }

            if (SwipeUp(currentSwipe))
            {
                target.transform.Rotate(180, 0, 0, Space.World);
               
            }
        }
    }
    bool SwipeDown(Vector2 swipe)
    {
        return currentSwipe.y < 0;
    }

    bool SwipeUp(Vector2 swipe)
    {
        return currentSwipe.y > 0;
    }
}