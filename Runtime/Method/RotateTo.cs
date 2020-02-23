using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  .
	/// </summary>
	public class RotateTo : iTweenBehaviourBase
	{
		#region Field

		public Vector3 eulerAngle = new Vector3(0f,0f,0f);

		#endregion // Field End.

		#region Method

        protected override void Reset()
		{
			base.Reset();
			eulerAngle = transform.transform.localRotation.eulerAngles; 
		}

		protected override void Initialize()
		{
			Register(iTween.RotateTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["rotation"] = eulerAngle;
			}
			return hash;
		}

		#endregion // Method End.
	}

	#if UNITY_EDITOR

	public static class RotateToMenu
	{
		[UnityEditor.MenuItem("CONTEXT/RotateTo/Record EulerAngle")]
		private static void RecordRotate(UnityEditor.MenuCommand menuCommand)
		{
			var component = menuCommand.context as RotateTo;
			if (component == null)
			{
				return;
			}
			component.eulerAngle = component.transform.localRotation.eulerAngles; 
		}
	}

	#endif // UNITY_EDITOR END.
} 
