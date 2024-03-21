using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Collider2D ground;

    [SerializeField] private float speed = 5;

    private Vector3 targetPosition;
    private bool isMouseButtonDown = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }

        if (isMouseButtonDown)
        {
            // If the mouse button is held down, continuously update the target position
            UpdateTargetPosition();
        }

        MoveToTargetPosition();
    }

    private void UpdateTargetPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (ground.OverlapPoint(mousePosition)) // Clicked inside ground
        {
            targetPosition = mousePosition;
        }
        else // Clicked outside ground
        {
            // Convert mouse position to a point within the boundaries of the ground
            targetPosition = GetPointWithinBoundaries(mousePosition);
        }
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private Vector3 GetPointWithinBoundaries(Vector3 point)
    {
        var bounds = ground.bounds;

        // Clamp point coordinates to boundary box
        float clampedX = Mathf.Clamp(point.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(point.y, bounds.min.y, bounds.max.y);

        return new Vector3(clampedX, clampedY, 0);
    }
}