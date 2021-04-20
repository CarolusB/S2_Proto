using System.Collections;
using System;
using UnityEngine;

namespace Player
{
	public class HitboxValue : MonoBehaviour, IComparable<HitboxValue>
	{
		#region Variables
		public Vector2 eject; /*can give direction AND force*/
		public int damageInput;
		public int localPriority;
		#endregion

		public int CompareTo(HitboxValue other)
        {
			if (localPriority < other.localPriority) return 1;

			if (localPriority > other.localPriority) return -1;

			return 0;
        }
	}
}

