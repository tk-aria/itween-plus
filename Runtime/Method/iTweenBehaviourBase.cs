using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  iTweenコンポーネント.
	/// </summary>
	public abstract class iTweenBehaviourBase : AriaPlugin.Coroutine.EventObject
	{

		#region Field

		[SerializeField] protected List<GameObject> animationTargetList;
		[SerializeField] protected bool isAutoStart = true;

		[SerializeField] protected float time = 1f;
		[SerializeField] protected float delay = 0f;
		[SerializeField] protected bool ignoreTimeScale = false;

		[SerializeField] protected iTween.EaseType easeType;
		[SerializeField] protected iTween.LoopType loopType;

		[SerializeField] protected UnityEvent onStart;
		[SerializeField] protected UnityEvent onUpdate;
		[SerializeField] protected UnityEvent onComplete;

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
				onStart.Invoke();
			};

			Action onUpdateAction = () =>
			{
				onUpdate.Invoke();
			};

			Action onCompleteAction = () =>
			{
				onComplete.Invoke();
			};

			return iTween.Hash(
				"time", time,
				"delay", delay,
				"easetype", easeType,
				"looptype", loopType,
				"onstart", onStartAction,
				"onupdate", onUpdateAction,
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
			for (int index = 1, max = animationTargetList.Count; index < max; index++)
			{
				tweenAction(animationTargetList[index], hash);
			}
			yield return new WaitForTweenAnimation(animationTargetList[0], hash, tweenAction);
		}

		/// <summary>
		///  コンポーネントの初期化.
		/// </summary>
		protected virtual void Reset()
		{
			animationTargetList.Clear();
			animationTargetList.Add(this.gameObject);
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
			component.StartAnimation();
		}

		#endregion // Editor End.

#endif // UNITY_EDITOR END.
	}
}