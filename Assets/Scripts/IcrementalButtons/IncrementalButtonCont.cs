using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class IncrementalButtonCont : MonoBehaviour
{
    public Button jumpIncremetalButton;
    public Button jetpackIncrementalButton;
    public Button moneyIncrementalButton;

    public HatModule hatModule;
    public Incremental_SO incremental_so;

    private void Start()
    {
        jumpIncremetalButton.onClick.AddListener(UpgradeJump);
        jetpackIncrementalButton.onClick.AddListener(UpgradeJetpack);
        moneyIncrementalButton.onClick.AddListener(UpgradeMoney);
        hatModule.Init();
    }

    private void UpgradeJump()
    {
        incremental_so.jumpForce.mainForce *= 1.1f;
        Debug.Log("Jumpforce upgraded");
    }

    private void UpgradeMoney()
    {
        incremental_so.coinAmount.coinAmount = incremental_so.coinAmount.coinAmount + 0.5f;
        Debug.Log("gold income upgraded");
    }

    private void UpgradeJetpack()
    {
        incremental_so.jetpack.jetpackFuel *= 1.1f;
        Debug.Log("jetpack fuel upgraded");
    }

    [Serializable]
    public struct HatModule
    {
        public Button hatChanger;
        public Material hatMaterial;

        public Incremental_SO incremental_SO;

        public List<Color> colorList;
        public void Init()
        {
            hatChanger.onClick.AddListener(ChangeColor);
            hatMaterial.color = incremental_SO.color;
        }
        public void ChangeColor()
        {
            var index = UnityEngine.Random.Range(0, colorList.Count);
            hatMaterial.color = colorList[index];
            incremental_SO.color = hatMaterial.color;
        }
    }
}
