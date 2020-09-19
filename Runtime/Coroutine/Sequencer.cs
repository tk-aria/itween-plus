using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaSDK.Runtime.Coroutine
{
	/// <summary>
	///  Corouitneの制御.
	/// </summary>
	public class Sequencer
	{
		#region Const
		#endregion // Const End.

		#region Type
		#endregion // Type End.

		#region Field

		private Queue<IEnumerator> eventList = new Queue<IEnumerator>();
		private bool isWorking = false;

		#endregion // Field End.

		#region Property

		/// <summary>
		///  実行中かどうか？
		/// </summary>
		public bool IsWorking
		{
			get { return isWorking; }
		}

		#endregion // Property End.

		#region Event
		#endregion // Event End.

		#region Method

		/// <summary>
		///  実行するCoroutineの登録.
		/// </summary>
		public void Register(IEnumerator coroutine)
		{
			eventList.Enqueue(coroutine);
		}

		/// <summary>
		/// 　
		/// </summary>
		public IEnumerator Execute()
		{
			isWorking = true;
			foreach(var sequence in eventList)
			{
				yield return sequence;
			}
			yield return null;
			isWorking = false;
		}

		#endregion // Method End.
	}

	/// <summary>
	///  Sequencerの終了を待つCoroutine.
	/// </summary>
	public class WaitForSequencer : CustomYieldInstruction
	{
		private Sequencer m_sequencer;

		public WaitForSequencer(GameObject target, Sequencer sequencer)
		{
			m_sequencer = sequencer;
		}

		public override bool keepWaiting 
		{
			get { return m_sequencer.IsWorking; }
		}
	}

}	
