using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class ValueTo<X> : iTweenBehaviourBase
	{
		#region Field

		public X from;
		public X to;
		[SerializeField] protected GameObject updatetarget;
		[SerializeField] protected string onupdate;

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
				hash["onupdate"] = onupdate;
				hash["onupdatetarget"] = updatetarget;
			}
			return hash;
		}

		protected override void Reset()
		{
			base.Reset();
			updatetarget = updatetarget ?? this.gameObject;
		}

		#endregion // Method End.
	}
}