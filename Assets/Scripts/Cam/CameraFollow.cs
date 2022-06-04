using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    public float camSpeed;
    public Vector3 offset;


    private void Update()
    {
        var target = _target.transform.position + offset;
        var targetPos = new Vector3(transform.position.x, target.y, target.z);
        var smoothPos = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * camSpeed);
        transform.position = smoothPos;
    }
}
