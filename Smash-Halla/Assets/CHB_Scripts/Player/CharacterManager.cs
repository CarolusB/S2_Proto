using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class CharacterManager : MonoBehaviour
	{
		#region Variables
		public float damage;
		[SerializeField] protected Rigidbody2D characterRb;
        #endregion

        protected virtual void Start()
        {
			damage = 0;
        }

        public virtual void Eject(Vector2 _vector, float damageInput)
        {
			damage += damageInput;
			characterRb.AddForce(_vector * damage, ForceMode2D.Impulse);
		}
	}
}

