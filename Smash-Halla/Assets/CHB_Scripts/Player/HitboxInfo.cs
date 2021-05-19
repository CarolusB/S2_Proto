using System.Collections;
using System;
using UnityEngine;

namespace Player
{
	[Serializable]
	public struct HitboxValue
	{
		public Vector2 eject; /*can give direction AND force*/
		public int damageInput;
		public int localPriority;

		//public HitboxValue(Vector2 _eject, bool _toRight, int _damageInput, int _localPriority)
		//{
		//	if (!_toRight)
		//		eject = new Vector2(-_eject.x, _eject.y);
		//	else
		//		eject = _eject;
			
		//	damageInput = _damageInput;
		//	localPriority = _localPriority;
		//}
	}

	public class HitboxInfo : MonoBehaviour, IComparable<HitboxInfo>
	{
		#region Variables
		public HitboxValue values;
		#endregion

		public int CompareTo(HitboxInfo other)
        {
			if (values.localPriority < other.values.localPriority) return 1;

			if (values.localPriority > other.values.localPriority) return -1;

			return 0;
        }
	}
}

