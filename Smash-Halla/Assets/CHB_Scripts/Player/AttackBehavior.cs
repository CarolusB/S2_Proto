using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AttackBehavior : MonoBehaviour
	{
		#region Variables
		[SerializeField] List<GameObject> attackParts;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			foreach(GameObject attackPart in attackParts)
            {
				attackPart.SetActive(false);
            }
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}
}

