using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class HurtboxBehavior : MonoBehaviour
	{
		#region Variables
		[SerializeField] private LayerMask hitboxLayer;
        [SerializeField] private CharacterManager charManager;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("HitBox")/*.layer == hitboxLayer*/)
            {
                MakeEject(collision.gameObject.GetComponent<HitboxValue>());
            }
        }

        private void MakeEject(HitboxValue hitbox)
        {
            charManager.Eject(hitbox.eject, hitbox.damageInput);
        }
    }
}

