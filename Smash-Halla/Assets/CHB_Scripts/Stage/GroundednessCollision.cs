using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Stage
{
	public class GroundednessCollision : MonoBehaviour
	{
		#region Variables
		//public LayerMask playerLayer;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player")/*.layer == playerLayer*/)
            {
                PlayerController playerCaught = collision.gameObject.GetComponent<PlayerController>();
                playerCaught.inAir = false;
                playerCaught.ResetJumps();
                StartCoroutine(playerCaught.JumpAgainDelay());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")/*.layer == playerLayer*/)
            {
                collision.gameObject.GetComponent<PlayerController>().inAir = true;
            }
        }
    }
}

