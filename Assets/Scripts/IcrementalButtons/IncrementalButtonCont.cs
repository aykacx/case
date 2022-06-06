using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class IncrementalButtonCont : MonoBehaviour
{
    public IncrementalButtonData jumpIncremetalButton;
    public IncrementalButtonData jetpackIncrementalButton;
    public IncrementalButtonData moneyIncrementalButton;

    public HatModule hatModule;
    public Incremental_SO incremental_so;
    public CoinData_SO coinData;

    public int costMutltiplier;

    [Serializable]
    public struct IncrementalButtonData
    {

        public Button button;
        public Text levelTxt;
        public Text costTxt;

        public void UpdateLevelData(int level, int cost)
        {
            levelTxt.text ="Level: " + level.ToString();
            costTxt.text ="Price: " + cost.ToString();
        }

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

    private void Start()
    {
        jumpIncremetalButton.button.onClick.AddListener(UpgradeJump);
        jetpackIncrementalButton.button.onClick.AddListener(UpgradeJetpack);
        moneyIncrementalButton.button.onClick.AddListener(UpgradeMoney);
        hatModule.Init();

        var cost = incremental_so.jumpForce.cost = costMutltiplier * incremental_so.jumpForce.level;
        jumpIncremetalButton.UpdateLevelData(incremental_so.jumpForce.level, cost);

        cost = incremental_so.coinAmount.cost = costMutltiplier * incremental_so.coinAmount.level;
        moneyIncrementalButton.UpdateLevelData(incremental_so.coinAmount.level, cost);

        cost = incremental_so.jetpack.cost = costMutltiplier * incremental_so.jetpack.level;
        jetpackIncrementalButton.UpdateLevelData(incremental_so.jetpack.level, cost);
    }
    private void Update()
    {
        CheckCost(jumpIncremetalButton, incremental_so.jumpForce);
        CheckCost(moneyIncrementalButton, incremental_so.coinAmount);
        CheckCost(jetpackIncrementalButton, incremental_so.jetpack);
    }
    public void CheckCost(IncrementalButtonData incrementalButton,ItemData itemData)
    {
        if (coinData.totalCoin < itemData.cost)
        {
            incrementalButton.button.interactable = false;
        }
        else
        {
            incrementalButton.button.interactable = true;
        }
    }
    private void UpgradeJump()
    {
        incremental_so.jumpForce.mainForce *= 1.1f;
        incremental_so.jumpForce.level++;
        var cost = incremental_so.jumpForce.cost = costMutltiplier * incremental_so.jumpForce.level;
        jumpIncremetalButton.UpdateLevelData(incremental_so.jumpForce.level, cost);
        coinData.totalCoin -= cost;
        Debug.Log("jump ugrded");
    }

    private void UpgradeMoney()
    {
        incremental_so.coinAmount.coinDistanceMultiplier = incremental_so.coinAmount.coinDistanceMultiplier + 0.5f;
        incremental_so.coinAmount.level++;
        var cost = incremental_so.coinAmount.cost = costMutltiplier * incremental_so.coinAmount.level;
        moneyIncrementalButton.UpdateLevelData(incremental_so.coinAmount.level, cost);
        coinData.totalCoin -= cost;
        Debug.Log("gold income upgraded");
    }

    private void UpgradeJetpack()
    {
        incremental_so.jetpack.jetpackFuel *= 1.1f;
        incremental_so.jetpack.level++;
        var cost = incremental_so.jetpack.cost = costMutltiplier * incremental_so.jetpack.level;
        jetpackIncrementalButton.UpdateLevelData(incremental_so.jetpack.level, cost);
        coinData.totalCoin -= cost;
        Debug.Log("jetpack fuel upgraded");
    }


}
