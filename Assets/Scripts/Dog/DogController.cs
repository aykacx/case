using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] Transform playerTransform;

    [SerializeField] float forwardSpeed;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
            Vector3 moveVector = transform.forward * forwardSpeed * Time.deltaTime;
            Vector3 movePos = transform.position + moveVector;
            rb.MovePosition(movePos);
    }
    void Update()
    {
        
    }
}
