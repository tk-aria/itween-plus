using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AriaSDK.Runtime.iTweenPro
{
	/// <summary>
	///  .
	/// </summary>
	public sealed class MoveTo : iTweenBehaviourBase
	{
		#region Field

		public Vector3 target = Vector3.zero;
		public bool islocal = true;
		public bool ignoreX = false;
		public bool ignoreY = false;
		public bool ignoreZ = false;

		#endregion // Field End.

		#region Method

		protected override void Reset()
		{
			base.Reset();
			target = transform.localPosition; 
		}

		protected override void Initialize()
		{
			Register(iTween.MoveTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				if (!ignoreX) hash["x"] = target.x;
				if (!ignoreY) hash["y"] = target.y;
				if (!ignoreZ) hash["z"] = target.z;
				hash["islocal"] = islocal;
			}
			return hash;
		}

		#endregion // Method End.
	}

	#if UNITY_EDITOR

	public static class MoveToMenu
	{
		[UnityEditor.MenuItem("CONTEXT/MoveTo/Record Position")]
		private static void RecordPosition(UnityEditor.MenuCommand menuCommand)
		{
			var component = menuCommand.context as MoveTo;
			if (component == null)
			{
				return;
			}
			component.target = component.transform.localPosition; 
		}
	}

	#endif // UNITY_EDITOR END.

}
