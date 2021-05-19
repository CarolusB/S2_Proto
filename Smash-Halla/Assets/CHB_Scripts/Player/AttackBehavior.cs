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
		}

		// Update is called once per frame
		void Update()
		{
			
		}
		
		public AttackBehavior StartAttack(bool _facingRight)
        {
            for (int i = 0; i < hitboxes.Length; i++)
            {
				hitboxes[i].values = setValues[i];
            }

			if (_facingRight)
            {
				//facing right version
            }
            else
            {
				foreach(HitboxInfo hitbox in hitboxes)
                {
					hitbox.values.eject = new Vector2(-hitbox.values.eject.x, hitbox.values.eject.y);
                }
				//facing left version
			}

			Ongoing = true;
			StartCoroutine(ProceedAttack());
			return this;
		}

        protected virtual IEnumerator ProceedAttack()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
			//Cancel when hit by opponent attack
			attackPlayer.Stop();
			Ongoing = false;
        }
	}
}

