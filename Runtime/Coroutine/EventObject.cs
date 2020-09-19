using System;
using System.Collections;
using UnityEngine;

namespace AriaSDK.Runtime.Coroutine
{
	/// <summary>
	///  Coroutineの実装をmustにするだけのMonoBehaviour.
	///  ※ Unity2019.3から必要なくなる.
	/// </summary>
	public abstract class EventObject : MonoBehaviour, ICoroutineComponent
	{
		public abstract IEnumerator OnEventProcess();
	}

	//[Serializable]
	public interface ICoroutineComponent
	{
		IEnumerator OnEventProcess();
	}
}
