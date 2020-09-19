using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaSDK.Runtime.Coroutine
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class SequencerControl : MonoBehaviour
	{
		#region Const
		#endregion // Const End.

		#region Type
		#endregion // Type End.

		#region Field

		private Sequencer sequencer = new Sequencer();
		[SerializeField] private List<EventObject> objectList = new List<EventObject>();

		#endregion // Field End.

		#region Property
		#endregion // Property End.

		#region Event
		#endregion // Event End.

		#region Method

		#region UnityCallback

		/// <summary>
		///  Unityによって呼び出される.
		/// </summary>
		private void Start()
		{
		}

		/// <summary>
		///  Unityから毎フレーム呼び出される.
		/// </summary>
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				Debug.Log("Invoke");
				foreach (var eventObj in objectList)
				{
					sequencer.Register(eventObj.OnEventProcess());
				}
				objectList.Clear();
				StartCoroutine(sequencer.Execute());
			}
		}

		#endregion // UnityCallback End.

		#endregion // Method End.
	}

}
