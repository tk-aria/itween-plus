using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.Coroutine
{

	public class WaitForAnimation : CustomYieldInstruction
	{
		UnityEngine.Animation animation;
		Func<bool> onCondition;

		public WaitForAnimation(UnityEngine.Animation animation, Func<bool> onCondition = null)
		{
			this.animation = animation;
			this.onCondition = onCondition ?? (() => { return true; });
		}

		public override bool keepWaiting 
		{
			get { return animation.isPlaying & onCondition(); }
		}
	}

	public class WaitForAnimator : CustomYieldInstruction
	{
		Animator m_animator;
		int m_lastStateHash = 0;
		int m_layerNo = 0;
		Func<bool> onCondition;

		public WaitForAnimator(Animator animator, int layerNo, Func<bool> onCondition = null)
		{
			Initialize(animator, layerNo, animator.GetCurrentAnimatorStateInfo(layerNo).fullPathHash);
			this.onCondition = onCondition ?? (() => { return false; });
		}

		void Initialize(Animator animator, int layerNo, int hash)
		{
			m_layerNo = layerNo;
			m_animator = animator;
			m_lastStateHash = hash;
		}

		public override bool keepWaiting 
		{
			get {
				var currentAnimatorState = m_animator.GetCurrentAnimatorStateInfo (m_layerNo);
				return ((currentAnimatorState.fullPathHash == m_lastStateHash) &&
					(currentAnimatorState.normalizedTime < 1 )) && onCondition();
			}
		}
	}

	public class WaitFor_iTween : CustomYieldInstruction
	{
		private bool m_isAnimation = false;

		public WaitFor_iTween(GameObject target, System.Collections.Hashtable hashTable, System.Action<GameObject,System.Collections.Hashtable> tweenAction)
		{
			System.Action completedAction = () => { m_isAnimation = false;};
			if (hashTable.ContainsKey("oncomplete"))
			{
			 	completedAction = (System.Action)hashTable["oncomplete"];
			}

			m_isAnimation = true;
			hashTable["oncomplete"] = completedAction;
			//hashTable["oncompletetarget"] = target;
			hashTable["oncompabsolutecompletelete"] = true;

			tweenAction(target, hashTable);
		}

		public override bool keepWaiting 
		{
			get { return m_isAnimation; }
		}
	}

	public class WaitForNetworkRequest : CustomYieldInstruction
	{
		private System.Action m_onComplete;
		private bool isDonwloading = false;

		public WaitForNetworkRequest(System.Action<System.Action> request)
		{
			isDonwloading = true;
			m_onComplete = () => { isDonwloading = false; };
			request(m_onComplete);
		}

		public override bool keepWaiting 
		{
			get { return isDonwloading; }
		}
	}

}