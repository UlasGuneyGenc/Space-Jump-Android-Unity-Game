using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItemCounts : MonoBehaviour
{
    public Text item1, item2, item3;

    private void Update()
    {
        item1.text = PlayerPrefs.GetInt("rocket", 0).ToString();
        item2.text = PlayerPrefs.GetInt("doubleGold", 0).ToString();
        item3.text = PlayerPrefs.GetInt("shield", 0).ToString();

    }

}
