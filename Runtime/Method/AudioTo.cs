using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class AudioTo : iTweenBehaviourBase
	{
		#region Field

		[SerializeField] AudioSource audiosource;
		[SerializeField] float volume = 0.5f;
		[SerializeField] float pitch = 1.0f;

		#endregion // Field End.

		#region Method

		protected override void Initialize()
		{
			Register(iTween.AudioTo);
		}

		protected override Hashtable CreateHash()
		{
			Hashtable hash = base.CreateHash();
			{
				hash["audiosource"] = audiosource;
				hash["volume"] = volume;
				hash["pitch"] = pitch;
			}
			return hash;
		}

		#endregion // Method End.
	}
}