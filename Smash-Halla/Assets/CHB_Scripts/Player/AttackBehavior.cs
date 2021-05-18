using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AttackBehavior : MonoBehaviour
	{
		#region Variables
		[SerializeField] List<GameObject> attackParts;
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
			foreach(GameObject attackPart in attackParts)
            {
				attackPart.SetActive(false);
            }

			Ongoing = false;
		}

		// Update is called once per frame
		void Update()
		{
			
		}
		
		public AttackBehavior StartAttack(bool _facingRight)
        {
            if (_facingRight)
            {
				//facing right version
            }
            else
            {
				//facing left version
			}

			StartCoroutine(ProceedAttack());
			return this;
		}

        private IEnumerator ProceedAttack()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
			//Cancel when hit by opponent attack
        }
	}
}

