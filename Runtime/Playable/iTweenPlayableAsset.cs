using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace AriaSDK.Runtime.iTweenPro
{

	[System.Serializable]
	public sealed class iTweenPlayableAsset : PlayableAsset
	{
		#region Field

		[SerializeField] ExposedReference<GameObject> target;
		[SerializeField] iTweenHashObject hash = null;
		[SerializeField] bool overwritableFromPlayableAsset = true;

		#endregion // Field End.

		#region Method

		public override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject)
		{
			var behaviour = new iTweenPlayableBehaviour();
			{
				behaviour.target = target.Resolve(graph.GetResolver());;
				behaviour.action = iTweenEvent.GetMethod(hash.type);
				behaviour.hash = hash.ToHashtable();
			}

			if (overwritableFromPlayableAsset)
			{
				float delay = behaviour.hash.ContainsKey("delay") ? (float)behaviour.hash["delay"] : 0f;
				behaviour.hash["time"] = this.duration - delay;
			}

			var playable = ScriptPlayable<iTweenPlayableBehaviour>.Create(graph, behaviour);
			return playable;
		}

		#endregion // Method End.
	}
}
