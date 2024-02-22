using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnClick : MonoBehaviour
{
    [HideInInspector]
    private Vector2 screenPosition;
    [HideInInspector]
    private Vector2 worldPosition;
    [HideInInspector]
    private float speed = 2f;
    public Vector3 targetPoint;

    void Update()
    {
        // Posiion of the mouse on screen
        screenPosition = Input.mousePosition;
        // Translate to position in the world
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        // On left button mouse input:
        if (Input.GetMouseButtonDown(0))
        {
            targetPoint = worldPosition;
        }

        // Every frame move object towards the target untill it is within 0.01 units
        if (targetPoint != null)
        {
            Vector2 direction = (targetPoint - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, targetPoint);

            if (distanceToTarget > 0.01f)
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
