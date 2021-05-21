using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Player
{
	public class AttackBehavior : MonoBehaviour
	{
		#region Variables
		[SerializeField] PlayableDirector attackPlayer;

		[SerializeField] PlayableAsset rightVersion;
		[SerializeField] PlayableAsset leftVersion;

		[SerializeField] int endLag = 20;
		[SerializeField] int autoCancelWindow = 0;
		[SerializeField] int autoCancelLag = 9;

		public HitboxInfo[] hitboxes;
		[SerializeField] HitboxValue[] setValues;
		bool isOngoing;
		public bool Ongoing
        {
			private set { isOngoing = value; }
            get { return isOngoing; }
        }
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			Ongoing = false;
			Debug.Log(attackPlayer.gameObject.name + " " + attackPlayer.state);
		}

		public AttackBehavior StartAttack(bool _facingRight)
        {
			Ongoing = true;

			for (int i = 0; i < hitboxes.Length; i++)
            {
				hitboxes[i].values = setValues[i];
            }

			if (_facingRight)
            {
				attackPlayer.playableAsset = rightVersion;
            }
            else
            {
				foreach(HitboxInfo hitbox in hitboxes)
                {
					hitbox.values.eject = new Vector2(-hitbox.values.eject.x, hitbox.values.eject.y);
                }
				
				attackPlayer.playableAsset = leftVersion;
			}

			attackPlayer.Play();
			StartCoroutine(ProceedAttack());
			return this;
		}

		int endLagFrameCount;
		int targetLag;
        protected virtual IEnumerator ProceedAttack()
        {
			endLagFrameCount = 0;
			targetLag = endLag;
			yield return new WaitUntil(() => attackPlayer.state == PlayState.Paused);

			while(endLagFrameCount < targetLag)
            {
				yield return new WaitForFixedUpdate();
				endLagFrameCount++;
            }

			Ongoing = false;
        }

		//int autoCancelDelay;
        public void Stop(bool canAutoCancel)
        {
			//Cancel when hit by opponent attack
			attackPlayer.Stop();

            if (canAutoCancel)
            {
				if (endLagFrameCount < autoCancelWindow)
                {
					//autoCancelDelay = 0;
					targetLag = autoCancelLag;
				}
            }
			else Ongoing = false;
        }
	}
}

