using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  .
	/// </summary>
	public class LookTo : iTweenBehaviourBase
	{
		#region Field

        [SerializeField] Transform looktarget = null;

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.LookTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["looktarget"] = looktarget;
			}
			return hash;
		}

		#endregion // Method End.
	}

} 
