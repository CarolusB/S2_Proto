using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class CharacterManager : MonoBehaviour
	{
		#region Variables
		public float damage;
		public static int stocks;
		[SerializeField] protected Rigidbody2D characterRb;
		public Transform respawnPoint;
		public static int respawnDelay;
		public static int respawnIntangibility;
		protected bool invincible = false;
        #endregion

        protected virtual void Start()
        {
			damage = 0f;
        }

        public virtual void Eject(Vector2 _vector, float damageInput, bool giveIntangibility, int intangibilityFrames)
        {
			if (invincible)
				return;
			damage += damageInput;
			characterRb.AddForce(_vector * damage, ForceMode2D.Impulse);
		}

		public virtual void Respawn()
        {
			StopAllCoroutines();
			stocks--;
			damage = 0f;

			if(stocks > 0)
            {
				StartCoroutine(RespawnDelay());
            }
        }

		int respawnDelayFrameCount;
		IEnumerator RespawnDelay()
        {
			respawnDelayFrameCount = 0;
			
			while(respawnDelayFrameCount < respawnDelay)
            {
				yield return new WaitForFixedUpdate();
				respawnDelayFrameCount++;
            }

			transform.position = respawnPoint.position;

			StartCoroutine(IntangibleTime(respawnIntangibility));
        }

		int intangibleFrameCount;
		protected virtual IEnumerator IntangibleTime(int time)
        {
			intangibleFrameCount = 0;
			invincible = true;

			while(intangibleFrameCount < time)
            {
				yield return new WaitForFixedUpdate();
				intangibleFrameCount++;
            }

			invincible = false;
        }
	}
}

