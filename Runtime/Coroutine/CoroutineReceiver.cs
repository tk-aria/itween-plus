using System.Collections;
using UnityEngine;

namespace AriaPlugin.Runtime.Coroutine
{

	/// <summary>
	///  This class allows us to start Coroutines from non-Monobehaviour scripts
	///  Create a GameObject it will use to launch the coroutine on
	/// </summary>
	[DisallowMultipleComponent]
	public class CoroutineReciever : MonoBehaviour 
	{

		static private CoroutineReciever m_Instance;

		/// <summary>
		///  
		/// </summary>
		static public CoroutineReciever Instance 
		{
			get 
			{
				if (m_Instance == null) 
				{
					GameObject o = new GameObject ("CoroutineReceiver");
					DontDestroyOnLoad (o);
					m_Instance = o.AddComponent<CoroutineReciever> ();
				}

				return m_Instance;
			}
		}

		/// <summary>
		///  
		/// </summary>
		public void OnDisable () 
		{
			if (m_Instance)
			{
				Destroy(m_Instance.gameObject);
			}
		}

		/// <summary>
		///  
		/// </summary>
		static public UnityEngine.Coroutine Notify(IEnumerator coroutine) 
		{
			return Instance.StartCoroutine(coroutine);
		}

	}

}