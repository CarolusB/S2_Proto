using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameManagement
{
	public class FightStart : MonoBehaviour
	{
        #region Variables
        [SerializeField] int stocks = 3;
        [SerializeField] int respawnDelay = 150;
        [SerializeField] int respawnIntangibility = 180;
        #endregion

        private void Awake()
        {
            CharacterManager.stocks = stocks;
            CharacterManager.respawnDelay = respawnDelay;
            CharacterManager.respawnIntangibility = respawnIntangibility;
        }
    }
}

