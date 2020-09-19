using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace AriaSDK.Runtime.iTweenPro
{

	/// <summary>
	///  timeline logic for iTween.
	/// </summary>
	public sealed class iTweenPlayableBehaviour : PlayableBehaviour
	{

		#region Property

		public System.Action<GameObject, Hashtable> action { set; private get; } = null;
		public GameObject target { set; private get; } = null;
		public Hashtable hash { set; get; } = null;

		#endregion // Property End.

		#region Method

		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			var coroutine = new iTween.WaitForAnimation(action, target, hash);
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			Object.DestroyImmediate(target.GetComponent<iTweenEvent>());
			Object.DestroyImmediate(target.GetComponent<iTween>());
		}

		//public override void PrepareFrame(Playable playable, FrameData info){}
		//public override void OnGraphStart(Playable playable){}
		//public override void OnGraphStop(Playable playable){}

		#endregion // Method End.
	}
}
