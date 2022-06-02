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
    public JumpMode currentJumpMode;

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

    public void Jump()
    {
        rb.AddForce(moveModule.mainForce * moveModule.currentMultiplier);
        Debug.Log(currentJumpMode);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumpMode = JumpMode.Nice;
    }

    private void FixedUpdate()
    {

        Multiplier();
        Vector3 movePos = new Vector3
            ((transform.position + MovePosition()).x,
            (transform.position + MovePosition()).y,
            (transform.position + MovePosition()).z);
        rb.MovePosition(movePos);
    }

    private void Multiplier()
    {
        if (currentJumpMode == JumpMode.Nice)
        {
            moveModule.currentMultiplier = 1;
        }
        else if (currentJumpMode == JumpMode.Cool)
        {
            moveModule.currentMultiplier = 2;
        }
        else
        {
            moveModule.currentMultiplier = 3;
        }
    }
    private Vector3 MovePosition()
    {
        Vector3 moveVector = transform.forward * moveModule.forwardSpeed * Time.deltaTime;
        return moveVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CoolLine")
        {
            currentJumpMode = JumpMode.Cool;
        }
        if (other.gameObject.name == "PerfectLine")
        {
            currentJumpMode = JumpMode.Perfect;
        }
    }
}
