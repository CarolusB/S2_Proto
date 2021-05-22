using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using System;

namespace GameManagement
{
    public class FighterLife : MonoBehaviour
    {
        #region Variables
        public Text damageText;
        public Text stocksText;
        #endregion

        public void UpdateDamages(float newDamage)
        {
            damageText.text = Math.Round(newDamage, 1, MidpointRounding.AwayFromZero).ToString() + " %";
        }

        public void UpdateStocks(int newStockCount)
        {
            stocksText.text = newStockCount.ToString();
        }
    }
}

