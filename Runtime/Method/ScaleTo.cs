using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  .
	/// </summary>
	public sealed class ScaleTo : iTweenBehaviourBase
	{
		#region Field

		public Vector3 scaleTo = new Vector3(1f,1f,1f);

		#endregion // Field End.

		#region Method

		protected override void Reset()
		{
			base.Reset();
			scaleTo = transform.localScale; 
		}

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

	#if UNITY_EDITOR

	public static class ScaleToMenu
	{
		[UnityEditor.MenuItem("CONTEXT/ScaleTo/Record Scale")]
		private static void RecordScale(UnityEditor.MenuCommand menuCommand)
		{
			var component = menuCommand.context as ScaleTo;
			if (component == null)
			{
				return;
			}
			component.scaleTo = component.transform.localScale; 
		}
	}

	#endif // UNITY_EDITOR END.
} 
