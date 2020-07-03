using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  .
	/// </summary>
	public sealed class AudioTo : iTweenBehaviourBase
	{
		#region Field

		[SerializeField] AudioSource audiosource = null;
		[SerializeField] float volume = 0.5f;
		[SerializeField] float pitch = 1.0f;

		#endregion // Field End.

		#region Method

		protected override void Reset()
		{
			base.Reset();
			audiosource = gameObject.GetComponent<AudioSource>();
			if (audiosource == null)
			{
				audiosource = gameObject.AddComponent<AudioSource>();
			}
		}

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