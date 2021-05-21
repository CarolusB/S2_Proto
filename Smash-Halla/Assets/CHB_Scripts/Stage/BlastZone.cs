using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Stage
{
	public class BlastZone : MonoBehaviour
	{
        #region Variables

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<CharacterManager>().Respawn();
            }
        }
    }
}

