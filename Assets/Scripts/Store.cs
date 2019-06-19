using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public int shield , rocket, doubleGold;

    private int gold;
    public GameObject goldError;
    public Text goldText;


    private void Start()
    {

        shield = PlayerPrefs.GetInt("shield", 0);
        rocket = PlayerPrefs.GetInt("rocket", 0);
        doubleGold = PlayerPrefs.GetInt("doubleGold", 0);
    }
    private void Update()
    {
        gold = PlayerPrefs.GetInt("gold", 0);
        
    }

    public void ShieldPwrInc()
    {
        if (gold < 40)
        {
            goldError.SetActive(true);
        }
        else
        {
            gold = gold - 40;
            shield++;
            PlayerPrefs.SetInt("shield", shield);
            PlayerPrefs.SetInt("gold", gold);
            goldText.text = gold.ToString();
        }

    }

    public void RocketPwrInc()
    {
        if (gold < 25)
        {
            goldError.SetActive(true);
        }
        else
        {
            rocket++;
            gold = gold - 25;
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.SetInt("rocket", rocket);
            goldText.text = gold.ToString();
        }
    }

    public void GoldPwrInc()
    {
        if (gold < 30)
        {
            goldError.SetActive(true);
        }
        else
        {
            doubleGold++;
            gold = gold - 30;
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.SetInt("doubleGold", doubleGold);
            goldText.text = gold.ToString();
        }
    }

}

