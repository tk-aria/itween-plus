using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AriaSDK.Runtime.iTweenPro
{
	/// <summary>
	///  iTween setting component.
	/// </summary>
	#if UNITY_EDITOR
	[HelpURL("http://pixelplacement.com/itween/documentation.php")]
	#endif // UNITY_EDITOR END.
	#if USE_IL2CPP
	[Il2CppSetOption(Option.NullChecks, false)]
	#endif // USE_IL2CPP END.
	public abstract class iTweenBehaviourBase : Coroutine.EventObject
	{

		[Serializable]
		public class AnimationTarget
		{
			public GameObject target;
			public SendMessageParameter onupdate;
		}

		#region Field

		[SerializeField] protected List<AnimationTarget> animationTargetList = new List<AnimationTarget>();
		[SerializeField] protected bool isAutoStart = true;

		[SerializeField] protected float time = 1f;
		[SerializeField] protected float delay = 0f;
		[SerializeField] protected bool ignoreTimeScale = false;

		[SerializeField] protected iTween.EaseType easeType;
		[SerializeField] protected iTween.LoopType loopType;

		[SerializeField] protected SendMessageParameter onupdate = new SendMessageParameter();
		[SerializeField] protected UnityEvent onstart;
		[SerializeField] protected UnityEvent oncomplete;

		protected Action<GameObject, Hashtable> tweenAction = (target, hash) => { };

		#endregion // Field End.

		#region Method

		/// <summary>
		/// 
		/// </summary>
		protected abstract void Initialize();

		/// <summary>
		///  
		/// </summary>
		protected virtual Hashtable CreateHash()
		{
			Action onStartAction = () =>
			{
				onstart.Invoke();
			};

			Action onCompleteAction = () =>
			{
				oncomplete.Invoke();
			};

			return iTween.Hash(
				"time", time,
				"delay", delay,
				"easetype", easeType,
				"looptype", loopType,
				"onstart", onStartAction,
				"oncomplete", onCompleteAction,
				"ignoretimescale", ignoreTimeScale
			);
		}

		/// <summary>
		///  メソッドの登録処理.
		/// </summary>
		public void Register(Action<GameObject, Hashtable> action)
		{
			tweenAction += action;
		}

		/// <summary>
		///  アニメーション開始処理.
		/// </summary>
		public void StartAnimation()
		{
			StartCoroutine(OnEventProcess());
		}

		/// <summary>
		///  
		/// </summary>
		public override IEnumerator OnEventProcess()
		{
			var hash = CreateHash();
			Func<Hashtable, AnimationTarget, Hashtable> addHashAnimationTarget = (Hashtable hashtable, AnimationTarget param) => 
			{
				if (param?.onupdate != null)
				{
					hashtable["onupdate"] = param.onupdate.invokeMethod;
					hashtable["onupdatetarget"] = param.onupdate.notifier;
				}
				return hashtable;
			};

			for (int index = 1, max = animationTargetList.Count; index < max; index++)
			{
				tweenAction(animationTargetList[index].target, addHashAnimationTarget(hash, animationTargetList[index]));
			}
			yield return new WaitForTweenAnimation(animationTargetList[0].target, addHashAnimationTarget(hash, animationTargetList[0]), tweenAction);
		}

		/// <summary>
		///  コンポーネントの初期化.
		/// </summary>
		protected virtual void Reset()
		{
			animationTargetList.Clear();
			animationTargetList.Add(new AnimationTarget()
			{
				target = this.gameObject,
				onupdate = new SendMessageParameter()
				{
					notifier = this.gameObject
				}
			});
		}

		/// <summary>
		///  Unityによって呼び出される.
		/// </summary>
		private void Start()
		{
			Initialize();

			if (isAutoStart)
			{
				StartAnimation();
			}
		}

		#endregion // Method End.

#if UNITY_EDITOR

		#region Editor

		[UnityEditor.MenuItem("CONTEXT/iTweenBehaviourBase/[ForTest]Invoke Start Animation")]
		private static void InvokeStartAnimation(UnityEditor.MenuCommand menuCommand)
		{
			var component = menuCommand.context as iTweenBehaviourBase;
			if (component == null)
			{
				return;
			}
			component.Initialize();
			component.StartAnimation();
		}

		#endregion // Editor End.

#endif // UNITY_EDITOR END.
	}
}