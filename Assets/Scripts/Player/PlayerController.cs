using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpMode
{
    Nice,
    Cool,
    Perfect
}

public class PlayerController : MonoBehaviour
{

    public MoveModule moveModule;

    private Rigidbody rb;

    [Serializable]
    public class MyDictionary<TK, TV>
    {
        public TK key;
        public TV value;
    }

    [Serializable]
    public struct MoveModule
    {

        public Vector3 mainForce;
        public float forwardSpeed;
        public List<MyDictionary<string, float>> multiplierList;

        public float currentForce { get; set;}
        public float currentMultiplier { get; set; }
    }

    void Jump()
    {
        rb.AddForce(moveModule.mainForce /** moveModule.currentMultiplier*/);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Jump();
    }
}
