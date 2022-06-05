using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    private Text moneyTxt;

    public CoinData_SO coinData;

    void Start()
    {
        moneyTxt = GetComponent<Text>();
    }

    void Update()
    {
        moneyTxt.text = coinData.totalCoin.ToString();
    }
}
