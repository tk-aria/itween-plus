using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AriaSDK.Runtime.iTweenPro
{
	/// <summary>
	///  
	/// </summary>
	public class WaitForTweenAnimation : CustomYieldInstruction
	{
		private bool m_isAnimation = false;

		public WaitForTweenAnimation(GameObject target, Hashtable hashTable, System.Action<GameObject, Hashtable> tweenAction)
		{
			System.Action completedAction = () => { m_isAnimation = false; Debug.Log("TweenAnimationEnd"); };
			if (hashTable.ContainsKey("oncomplete"))
			{
				completedAction += (System.Action)hashTable["oncomplete"];
			}

			m_isAnimation = true;
			hashTable["oncomplete"] = completedAction;

			tweenAction(target, hashTable);
		}

		public override bool keepWaiting
		{
			get { return m_isAnimation; }
		}
	}

}
