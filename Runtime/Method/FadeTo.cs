using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  .
	/// </summary>
	public class FadeTo : iTweenBehaviourBase
	{
		#region Field

		public float alpha;
		public float amount;
		public bool includechildren = false;
		public string NamedValueColor = "_Color";

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.FadeTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["alpha"] = alpha;
				hash["amount"] = amount;
				hash["includechildren"] = includechildren;
				hash["NamedValueColor"] = NamedValueColor;
			}
			return hash;
		}

		#endregion // Method End.
	}
}