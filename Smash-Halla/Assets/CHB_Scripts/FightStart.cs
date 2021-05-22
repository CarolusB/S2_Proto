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
        [SerializeField] List<CharacterManager> players;
        #endregion

        private void Awake()
        {
            foreach(CharacterManager player in players)
            {
                player.stocks = stocks;
            }
            CharacterManager.respawnDelay = respawnDelay;
            CharacterManager.respawnIntangibility = respawnIntangibility;
        }
    }
}

