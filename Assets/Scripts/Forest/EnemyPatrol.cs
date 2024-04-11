using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    private Rigidbody2D rb;
    private Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Start target postion" + target.position);
        Debug.Log("Start transform postion" + transform.position);
        Debug.Log("Start B postion" + pointB.position);
        target.position = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        Debug.Log("target postion" + target.position);
        Debug.Log("transform postion" + transform.position);
        Debug.Log("B postion" + pointB.position);
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            if (target.position == pointB.position)
            {
                target.position = pointA.position;
            }
            else if (target.position == pointA.position)
            {
                target.position = pointB.position;
          }
        }
    }
}
