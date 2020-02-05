using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class MoveTo : iTweenBehaviourBase
	{
		#region Field

		public Vector3 target = Vector3.zero;
		public bool islocal = true;

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.MoveTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["x"] = target.x;
				hash["y"] = target.y;
				hash["z"] = target.z;
				hash["islocal"] = islocal;
			}
			return hash;
		}

		#endregion // Method End.
	}

}
