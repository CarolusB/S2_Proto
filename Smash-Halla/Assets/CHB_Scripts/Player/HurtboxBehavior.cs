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
                MakeEject(collision.gameObject.GetComponent<HitboxInfo>());
            }
        }

        private void MakeEject(HitboxInfo hitbox)
        {
            charManager.Eject(hitbox.values.eject, hitbox.values.damageInput);
        }
    }
}

