using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpMode
{
    Nice,
    Cool,
    Perfect,
    CantJump
}

public class PlayerController : MonoBehaviour
{
    public MoveModule moveModule;
    public CoinModule coinModule;
    public GameOverModule gameOver;

    private bool _speedCutted = false;


    [Serializable]
    public class MyDictionary<TK, TV>
    {
        public TK key;
        public TV value;
    }

    [Serializable]
    public struct MoveModule
    {
        public Incremental_SO incremental_Data;
        public Rigidbody rigidbody;
        public int jumpCount;
        public Vector3 mainForce;
        public Vector3 jetpackMainForce;
        public float forwardSpeed;
        public float cutSpeed; // foward speedin kesilme s�resi veya h�z� 
        public float jetpackFuelCutSpeed; // �sttekinin ayn�s�n�n jetpack i�in olan�

        public float jetpackFuel;

        public List<MyDictionary<JumpMode, float>> multiplierList;

        public Vector3 CurrentForce { get; set; }
        public Vector3 CurrentJetpackForce { get; set; }
        public float CurrentMultiplier { get; set; }
        public float CurrentForwardSpeed { get; set; }
        public float CurrentJetpackFuel { get; set; }
        public int CurrentJumpCount { get; private set; }
        public float CurrentDistance { get; private set; }
        public JumpMode CurrentJumpMode { get; set; }
        public bool CanMove { get; set; }
        public void Init()
        {
            CanMove = true;
            CurrentJumpCount = jumpCount;
            CurrentForce = incremental_Data.jumpForce.mainForce;
            CurrentJetpackFuel = incremental_Data.jetpack.jetpackFuel;
            CurrentJetpackForce = jetpackMainForce;
            CurrentForwardSpeed = forwardSpeed;
            SetJumpMultiplier();
        }
        public void Jump()
        {
            if (CurrentJumpCount > 0 && CurrentJumpMode != JumpMode.CantJump && rigidbody.transform.position.y < 0.1)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(CurrentForce * CurrentMultiplier, ForceMode.Impulse);
                CurrentJumpCount--;
                CanMove = false;
            }
        }
        public void Move(Transform transform)
        {
            Vector3 moveVector = transform.forward * CurrentForwardSpeed * Time.deltaTime;
            Vector3 movePos = transform.position + moveVector;
            rigidbody.MovePosition(movePos);
        }
        public void SetJumpMultiplier()
        {
            for (int i = 0; i < multiplierList.Count; i++)
            {
                if (multiplierList[i].key == CurrentJumpMode)
                    CurrentMultiplier = multiplierList[i].value;
            }
        }
        public void JetPack()
        {
            if (CurrentJetpackFuel > 0 && CurrentJumpCount <= 0)
            {
                rigidbody.AddForce(CurrentJetpackForce * CurrentMultiplier, ForceMode.Impulse);

                CurrentJetpackFuel = Mathf.Lerp(CurrentJetpackFuel, 0, Time.deltaTime * jetpackFuelCutSpeed);
                if (CurrentJetpackFuel < 1)
                {
                    CurrentJetpackFuel = 0;
                }
            }
        }
    }
    [Serializable]
    public struct CoinModule
    {
        public Incremental_SO incrementalData;
        public CoinData_SO coinData;

        private float _currrentPos;
        public float coinPerDistance;

        public void AddCoin(float lastPos)
        {
            if (lastPos - _currrentPos >= coinPerDistance)
            {
                _currrentPos = lastPos;
                coinData.totalCoin += (int)incrementalData.coinAmount.coinAmount;
            }
        }
    }
    [Serializable]
    public struct GameOverModule
    {
        public MoveModule moveModule;

        public float overTime;

        public void GameOver()
        {
            if (moveModule.CurrentJetpackFuel == 0)
            {
                overTime = Mathf.Lerp(overTime, 0, Time.deltaTime * overTime);
                if (overTime <= 0.1)
                {
                    Debug.Log("game over");
                }
            }
        }
    }
    void Start()
    {
        moveModule.Init();
    }
    public void Jump()
    {
        moveModule.Jump();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            moveModule.JetPack();

        if (moveModule.CanMove)
            moveModule.Move(transform);
    }
    private void Update()
    {
        if (_speedCutted)
            moveModule.CurrentForwardSpeed = Mathf.Lerp(moveModule.CurrentForwardSpeed, 0, Time.deltaTime * moveModule.cutSpeed);

        coinModule.AddCoin(transform.position.z);

        gameOver.GameOver();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CoolLine")
        {
            moveModule.CurrentJumpMode = JumpMode.Cool;
            moveModule.SetJumpMultiplier();
        }
        if (other.gameObject.name == "PerfectLine")
        {
            moveModule.CurrentJumpMode = JumpMode.Perfect;
            moveModule.SetJumpMultiplier();
        }
        if (other.gameObject.name == "SpeedCutter")
        {
            _speedCutted = true;
        }
    }
}
