using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] Transform roadTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            roadTransform.position = new Vector3(roadTransform.position.x, roadTransform.position.y, roadTransform.position.z + 718);
        }
    }
}
