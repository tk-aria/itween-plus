#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AriaSDK.Editor.Coroutine
{
    /*
    /// <summary>
    ///  [ここに何をするクラスなのかを記入する].
    /// </summary>
    public class CoroutineForEditor
	{
		public static IEnumerator StartCoroutine(IEnumerator _routine)
		{
			CoroutineForEditor coroutine = new CoroutineForEditor(_routine);
			coroutine.start();
			return coroutine;
		}

		readonly IEnumerator routine;
		EditorCoroutine( IEnumerator _routine )
		{
			routine = _routine;
		}

		void start()
		{
			//Debug.Log("start");
			EditorApplication.update += update;
		}
		public void stop()
		{
			//Debug.Log("stop");
			EditorApplication.update -= update;
		}

		void update()
		{
			if (!routine.MoveNext())
			{
				stop();
			}
		}
	}
    */
}


#endif // UNITY_EDITOR END.