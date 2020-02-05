using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class ScaleTo : iTweenBehaviourBase
	{
		#region Field

		public Vector3 scaleTo = new Vector3(1f,1f,1f);

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.ScaleTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["x"] = scaleTo.x;
				hash["y"] = scaleTo.y;
				hash["z"] = scaleTo.z;
			}
			return hash;
		}

		#endregion // Method End.
	}
}