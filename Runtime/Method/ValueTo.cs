using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaSDK.Runtime.iTweenPro
{
	/// <summary>
	///  .
	/// </summary>
	public class ValueTo<X> : iTweenBehaviourBase
	{
		#region Field

		public X from;
		public X to;

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.ValueTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["from"] = from;
				hash["to"] = to;
			}
			return hash;
		}

		#endregion // Method End.
	}
}