using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class ChargedBehavior : AttackBehavior
	{
        #region Variables
        [SerializeField] PlayerController playerController;
        #endregion

        protected override IEnumerator ProceedAttack()
        {
            yield return new WaitUntil(() => !playerController.IsCharging);
            StartCoroutine(base.ProceedAttack());
        }

        public void ChargeDamages()
        {

        }
    }
}

