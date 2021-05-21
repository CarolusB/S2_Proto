using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	//[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerManager : CharacterManager
	{
		#region Variables
		public PlayerController playerController;
		[SerializeField] int hitStunBaseLag = 15;
		Coroutine currentHitStun;

		bool respawning = false;
        #endregion

        // Start is called before the first frame update
        protected override void Start()
		{
			base.Start();
			characterRb = playerController.playerRb;
		}

		public override void Eject(Vector2 _vector, float damageInput, bool giveIntangibility, int intangibilityFrames)
        {
			if (invincible)
				return;
			base.Eject(_vector, damageInput, giveIntangibility, intangibilityFrames);
			if(playerController.currentAttack != null) playerController.currentAttack.Stop(false);
			SetHitStun();
        }

		void SetHitStun()
        {
			playerController.hitStun = true;

			if (currentHitStun != null) StopCoroutine(currentHitStun);
			currentHitStun = StartCoroutine(HitStunRecovery());
        }

		int hitStunFrameCount;
		int hitStunTotal;
		IEnumerator HitStunRecovery()
        {
			hitStunFrameCount = 0;
			hitStunTotal = hitStunBaseLag + (int)(damage * 0.41f);
			while (hitStunFrameCount < hitStunTotal)
            {
				yield return new WaitForFixedUpdate();
				hitStunFrameCount++;
            }
			//yield return new WaitForSeconds(0.3f + damage * 0.0065f);
			playerController.hitStun = false;
        }

        public override void Respawn()
        {
			respawning = true;
			base.Respawn();
        }
        protected override IEnumerator IntangibleTime(int time)
        {
            StartCoroutine(base.IntangibleTime(time));

			if (respawning)
			{
				yield return new WaitUntil(() => !invincible || playerController.currentAttack != null);
				invincible = false;
			}
			else yield return null;
        }
    }
}

