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
        bool decisionOngoing = false;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("HitBox")/*.layer == hitboxLayer*/)
            {
                MakeEject(collision.gameObject.GetComponent<HitboxInfo>());
                //ready for an attack with multiple hitboxes available at the same time
                //HitboxInfo hitboxCatch = null;
                //while(hitboxCatch == null)
                //    hitboxCatch = collision.gameObject.GetComponent<HitboxInfo>();

                //hitboxesCaught.Add(hitboxCatch);

                //if (!decisionOngoing)
                //    StartCoroutine(DamageDecision());
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

