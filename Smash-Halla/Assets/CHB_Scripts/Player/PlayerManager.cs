using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	//[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerManager : MonoBehaviour
	{
		#region Variables
		public PlayerController playerController;
		public float damage;

		Coroutine currentHitStun;
        #endregion

        // Start is called before the first frame update
        void Start()
		{
			damage = 0;
		}

		// Update is called once per frame
		void Update()
		{
			
		}

		public void Eject(Vector2 _vector, float damageInput)
        {
			damage += damageInput;
			playerController.playerRb.AddForce(_vector * damage, ForceMode2D.Impulse);
			
			SetHitStun();
        }

		void SetHitStun()
        {
			playerController.hitStun = true;

			if (currentHitStun != null) StopCoroutine(currentHitStun);
			currentHitStun = StartCoroutine(HitStunRecovery());
        }

		int hitStunFrameCount;
		IEnumerator HitStunRecovery()
        {
			hitStunFrameCount = 0;
			while(hitStunFrameCount < 15 + (int)(damage * 0.41f))
            {
				yield return new WaitForFixedUpdate();
				hitStunFrameCount++;
            }
			//yield return new WaitForSeconds(0.3f + damage * 0.0065f);
			playerController.hitStun = false;
        }
	}
}

