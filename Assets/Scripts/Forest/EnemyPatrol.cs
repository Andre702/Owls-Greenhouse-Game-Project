using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    private Transform target;
    public float speed;

    private void Awake()
    {
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        //Debug.Log("Distance " + Vector3.Distance(transform.position, target.position));

        if (Vector3.Distance(transform.position, target.position) < 0.9f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}
