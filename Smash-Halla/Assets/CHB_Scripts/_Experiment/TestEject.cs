using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Experiment
{
	public class TestEject : MonoBehaviour
	{
		#region Variables
		public Rigidbody2D dummyRb;
		public Transform projectPoint;
		Vector2 eject;
		public float ejectMultiply = 1f;

		public PlayableDirector attack;
        #endregion

        private void Start()
        {
			//dummyRb = GetComponent<Rigidbody2D>();
			ejectMultiply = 33f;
        }
        // Update is called once per frame
        void Update()
		{
   //         if (Input.GetKeyDown(KeyCode.T))
   //         {
			//	eject = projectPoint.position - transform.position;
			//	dummyRb.AddForce(eject * ejectMultiply, ForceMode2D.Impulse);
			//	ejectMultiply += 6.35f;
   //         }

			//if (Input.GetKeyDown(KeyCode.M))
   //         {
			//	attack.Play();
   //         }
		}
	}
}

