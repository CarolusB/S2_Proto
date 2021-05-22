using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class HurtboxBehavior : MonoBehaviour
	{
		#region Variables
		//[SerializeField] private LayerMask hitboxLayer;
        [SerializeField] private CharacterManager charManager;
        List<HitboxInfo> hitboxesCaught;
        HitboxInfo hitboxChosen;
        bool decisionOngoing = false;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("HitBox")/*.layer == hitboxLayer*/)
            {
                //ready for an attack with multiple hitboxes available at the same time
                hitboxesCaught.Add(collision.gameObject.GetComponent<HitboxInfo>());

                if (!decisionOngoing)
                    StartCoroutine(DamageDecision());
            }
        }

        IEnumerator DamageDecision()
        {
            decisionOngoing = true;
            yield return new WaitForFixedUpdate();

            hitboxesCaught.Sort();
            MakeEject(hitboxesCaught[0]);
            hitboxesCaught = new List<HitboxInfo>();
            decisionOngoing = false;
        }

        private void MakeEject(HitboxInfo hitbox)
        {
            HitboxValue hitboxValue = hitbox.values;
            charManager.Eject(hitboxValue.eject * (1f + hitboxValue.ejectAddCoef), hitboxValue.damageInput, hitboxValue.giveIntangibility, hitboxValue.giveIntangibilityFrames);
        }
    }
}

