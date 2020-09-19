// Copyright (c) 2009-2012 David Koontz
// Please direct any bugs/comments/suggestions to david@koontzfamily.org
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ArrayIndexes
{
	public int[] indexes;
}

public sealed class iTweenEvent : MonoBehaviour
{
	public const string VERSION = "0.6.1";

	public enum TweenType
	{
		AudioFrom,
		AudioTo,
		AudioUpdate,
		CameraFadeFrom,
		CameraFadeTo,
		ColorFrom,
		ColorTo,
		ColorUpdate,
		FadeFrom,
		FadeTo,
		FadeUpdate,
		LookFrom,
		LookTo,
		LookUpdate,
		MoveAdd,
		MoveBy,
		MoveFrom,
		MoveTo,
		MoveUpdate,
		PunchPosition,
		PunchRotation,
		PunchScale,
		RotateAdd,
		RotateBy,
		RotateFrom,
		RotateTo,
		RotateUpdate,
		ScaleAdd,
		ScaleBy,
		ScaleFrom,
		ScaleTo,
		ScaleUpdate,
		ShakePosition,
		ShakeRotation,
		ShakeScale,
		Stab
		//ValueTo
	}

	public string tweenName = "";
	public bool playAutomatically = true;
	public float delay = 0;
	//public iTweenEvent.TweenType type = iTweenEvent.TweenType.MoveTo;
	public bool showIconInInspector = true;
	[SerializeField] public iTweenHashObject tweenHash = null;


	/// <summary>
	/// Finds an iTweenEvent on a GameObject
	/// </summary>
	/// <param name="obj">
	/// The <see cref="GameObject"/> to look under
	/// </param>
	/// <param name="name">
	/// The name of the <see cref="iTweenEvent"/> to look for
	/// </param>
	/// <returns>
	/// A <see cref="iTweenEvent"/>
	/// </returns>
	public static iTweenEvent GetEvent(GameObject obj, string name) {
		var tweens = obj.GetComponents<iTweenEvent>();
		if(tweens.Length > 0) {
			var result = tweens.FirstOrDefault(tween => { return tween.tweenName == name; });
			if(result != null) {
				return result;
			}
		}

		throw new System.ArgumentException("No tween with the name '" + name + "' could be found on the GameObject named '" + obj.name + "'");
	}

	/// <summary>
	///
	/// </summary>
	public sealed class WaitForAnimation : CustomYieldInstruction
	{
		iTweenEvent tweenEvent = null;

		/// <summary>
		///
		/// </summary>
		/// <param name="target"></param>
		/// <param name="hash"></param>
		public WaitForAnimation(GameObject target, iTweenHashObject hash)
		{
			tweenEvent = target.GetComponent<iTweenEvent>() ?? target.AddComponent<iTweenEvent>();
			tweenEvent.tweenHash = hash;
			tweenEvent.Play();
		}

		public override bool keepWaiting
		{
			get { return !tweenEvent.stopped; }
		}
	}

	/*
	public Dictionary<string, object> Values {
		get {
			if(null == values) {
				DeserializeValues();
			}
			return values;
		}
		set {
			values = value;
			SerializeValues();
		}
	}
	*/

	/*
	[SerializeField]
	string[] keys;

	[SerializeField]
 	int[] indexes;

	[SerializeField]
	string[] metadatas;

	[SerializeField]
	int[] ints;

	[SerializeField]
	float[] floats;

	[SerializeField]
	bool[] bools;

	[SerializeField]
	string[] strings;

	[SerializeField]
	Vector3[] vector3s;

	[SerializeField]
	Color[] colors;

	[SerializeField]
	Space[] spaces;

	[SerializeField]
	iTween.EaseType[] easeTypes;

	[SerializeField]
	iTween.LoopType[] loopTypes;

	[SerializeField]
	GameObject[] gameObjects;

	[SerializeField]
	Transform[] transforms;

	[SerializeField]
	AudioClip[] audioClips;

	[SerializeField]
	AudioSource[] audioSources;

	[SerializeField]
	ArrayIndexes[] vector3Arrays;

	[SerializeField]
	ArrayIndexes[] transformArrays;

	[SerializeField]
	iTweenPath[] paths;
	*/

	//Dictionary<string, object> values;

	[NonSerialized] public bool stopped;
	iTween instantiatedTween;
	string internalName;

	private void Start() {
		if(playAutomatically) Play();
	}

	public void Play() {
		if(!string.IsNullOrEmpty(internalName)) Stop();

		stopped = false;
		StartCoroutine(StartEvent());
	}

	/// <summary>
	/// Stops the currently running tween that was started with <seealso cref="Play"/>.  A tween
	/// stopped in this manner will not go to the "end" of the tween.
	/// </summary>
	public void Stop() {
		iTween.StopByName(gameObject, internalName);
		internalName = "";
		stopped = true;
	}

	public void OnDrawGizmos() {
		if(showIconInInspector) Gizmos.DrawIcon(transform.position, "iTweenIcon.tif");
	}

	IEnumerator StartEvent() {
		if(delay > 0) yield return new WaitForSeconds(delay);

		if(stopped) yield return null;

		var optionsHash = tweenHash.ToHashtable();
		/*
		var optionsHash = new Hashtable();
		//foreach(var pair in Values) {
		foreach(var pair in tweenHash.Values) {
			if("path" == pair.Key && pair.Value.GetType() == typeof(string)) optionsHash.Add(pair.Key, iTweenPath.GetPath((string)pair.Value));
			else optionsHash.Add(pair.Key, pair.Value);
		}
		*/

		// We use the internalName to have a unique identifier to stop the tween
		internalName = string.IsNullOrEmpty(tweenName) ? string.Empty : tweenName;
		internalName = string.Format("{0}-{1}", internalName, System.Guid.NewGuid().ToString());
		optionsHash.Add("name", internalName);

		GetMethod(tweenHash.type)(gameObject, optionsHash);
	}

	public static Action<GameObject, Hashtable> GetMethod(TweenType type)
	//, GameObject target, Hashtable hashtable)
	{
		Action<GameObject, Hashtable> tweenAction = null;

		switch(type) {
		case TweenType.AudioFrom:
			tweenAction = iTween.AudioFrom;
			break;
		case TweenType.AudioTo:
			tweenAction = iTween.AudioTo;//(target, hashtable);
			break;
		case TweenType.AudioUpdate:
			tweenAction = iTween.AudioUpdate;//(target, hashtable);
			break;

		// obsolete?
		case TweenType.CameraFadeFrom:
			tweenAction = (GameObject target, Hashtable hash) => { iTween.CameraFadeFrom(hash); };//(hashtable);
			break;

		// obsolete?
		case TweenType.CameraFadeTo:
			tweenAction = (GameObject target, Hashtable hash) => { iTween.CameraFadeTo(hash); };//(hashtable);
			break;

		case TweenType.ColorFrom:
			tweenAction = iTween.ColorFrom;//(target, hashtable);
			break;
		case TweenType.ColorTo:
			tweenAction = iTween.ColorTo;//(target, hashtable);
			break;
		case TweenType.ColorUpdate:
			tweenAction = iTween.ColorUpdate;//(target, hashtable);
			break;
		case TweenType.FadeFrom:
			tweenAction = iTween.FadeFrom;//(target, hashtable);
			break;
		case TweenType.FadeTo:
			tweenAction = iTween.FadeTo;//(target, hashtable);
			break;
		case TweenType.FadeUpdate:
			tweenAction = iTween.FadeUpdate;//(target, hashtable);
			break;
		case TweenType.LookFrom:
			tweenAction = iTween.LookFrom;//(target, hashtable);
			break;
		case TweenType.LookTo:
			tweenAction = iTween.LookTo;//(target, hashtable);
			break;
		case TweenType.LookUpdate:
			tweenAction = iTween.LookUpdate;//(target, hashtable);
			break;
		case TweenType.MoveAdd:
			tweenAction = iTween.MoveAdd;//(target, hashtable);
			break;
		case TweenType.MoveBy:
			tweenAction = iTween.MoveBy;//(target, hashtable);
			break;
		case TweenType.MoveFrom:
			tweenAction = iTween.MoveFrom;//(target, hashtable);
			break;
		case TweenType.MoveTo:
			tweenAction = iTween.MoveTo;//(target, hashtable);
			break;
		case TweenType.MoveUpdate:
			tweenAction = iTween.MoveUpdate;//(target, hashtable);
			break;
		case TweenType.PunchPosition:
			tweenAction = iTween.PunchPosition;//(target, hashtable);
			break;
		case TweenType.PunchRotation:
			tweenAction = iTween.PunchRotation;//(target, hashtable);
			break;
		case TweenType.PunchScale:
			tweenAction = iTween.PunchScale;//(target, hashtable);
			break;
		case TweenType.RotateAdd:
			tweenAction = iTween.RotateAdd;//(target, hashtable);
			break;
		case TweenType.RotateBy:
			tweenAction = iTween.RotateBy;//(target, hashtable);
			break;
		case TweenType.RotateFrom:
			tweenAction = iTween.RotateFrom;//(target, hashtable);
			break;
		case TweenType.RotateTo:
			tweenAction = iTween.RotateTo;//(target, hashtable);
			break;
		case TweenType.RotateUpdate:
			tweenAction = iTween.RotateUpdate;//(target, hashtable);
			break;
		case TweenType.ScaleAdd:
			tweenAction = iTween.ScaleAdd;//(target, hashtable);
			break;
		case TweenType.ScaleBy:
			tweenAction = iTween.ScaleBy;//(target, hashtable);
			break;
		case TweenType.ScaleFrom:
			tweenAction = iTween.ScaleFrom;//(target, hashtable);
			break;
		case TweenType.ScaleTo:
			tweenAction = iTween.ScaleTo;//(target, hashtable);
			break;
		case TweenType.ScaleUpdate:
			tweenAction = iTween.ScaleUpdate;//(target, hashtable);
			break;
		case TweenType.ShakePosition:
			tweenAction = iTween.ShakePosition;//(target, hashtable);
			break;
		case TweenType.ShakeRotation:
			tweenAction = iTween.ShakeRotation;//(target, hashtable);
			break;
		case TweenType.ShakeScale:
			tweenAction = iTween.ShakeScale;//(target, hashtable);
			break;
		case TweenType.Stab:
			tweenAction = iTween.Stab;//(target, hashtable);
			break;
		default:
			throw new System.ArgumentException("Invalid tween type: " + type);
		}

		return tweenAction;
	}

	/*
	void SerializeValues() {
		var keyList = new List<string>();
		var indexList = new List<int>();
		var metadataList = new List<string>();

		var intList = new List<int>();
		var floatList = new List<float>();
		var boolList = new List<bool>();
		var stringList = new List<string>();
		var vector3List = new List<Vector3>();
		var colorList = new List<Color>();
		var spaceList = new List<Space>();
		var easeTypeList = new List<iTween.EaseType>();
		var loopTypeList = new List<iTween.LoopType>();
		var gameObjectList = new List<GameObject>();
		var transformList = new List<Transform>();
		var audioClipList = new List<AudioClip>();
		var audioSourceList = new List<AudioSource>();
		var vector3ArrayList = new List<ArrayIndexes>();
		var transformArrayList = new List<ArrayIndexes>();

		foreach(var pair in values) {
			var mappings = EventParamMappings.mappings[type];
			var valueType = mappings[pair.Key];
			if(typeof(int) == valueType) {
				AddToList<int>(keyList, indexList, intList, metadataList, pair);
			}
			if(typeof(float) == valueType) {
				AddToList<float>(keyList, indexList, floatList, metadataList, pair);
			}
			else if(typeof(bool) == valueType) {
				AddToList<bool>(keyList, indexList, boolList, metadataList, pair);
			}
			else if(typeof(string) == valueType) {
				AddToList<string>(keyList, indexList, stringList, metadataList, pair);
			}
			else if(typeof(Vector3) == valueType) {
				AddToList<Vector3>(keyList, indexList, vector3List, metadataList, pair);
			}
			else if(typeof(Color) == valueType) {
				AddToList<Color>(keyList, indexList, colorList, metadataList, pair);
			}
			else if(typeof(Space) == valueType) {
				AddToList<Space>(keyList, indexList, spaceList, metadataList, pair);
			}
			else if(typeof(iTween.EaseType) == valueType) {
				AddToList<iTween.EaseType>(keyList, indexList, easeTypeList, metadataList, pair);
			}
			else if(typeof(iTween.LoopType) == valueType) {
				AddToList<iTween.LoopType>(keyList, indexList, loopTypeList, metadataList, pair);
			}
			else if(typeof(GameObject) == valueType) {
				AddToList<GameObject>(keyList, indexList, gameObjectList, metadataList, pair);
			}
			else if(typeof(Transform) == valueType) {
				AddToList<Transform>(keyList, indexList, transformList, metadataList, pair);
			}
			else if(typeof(AudioClip) == valueType) {
				AddToList<AudioClip>(keyList, indexList, audioClipList, metadataList, pair);
			}
			else if(typeof(AudioSource) == valueType) {
				AddToList<AudioSource>(keyList, indexList, audioSourceList, metadataList, pair);
			}
			else if(typeof(Vector3OrTransform) == valueType) {
				if(pair.Value == null || typeof(Transform) == pair.Value.GetType()) {
					AddToList<Transform>(keyList, indexList, transformList, metadataList, pair.Key, pair.Value, "t");
				}
				else {
					AddToList<Vector3>(keyList, indexList, vector3List, metadataList, pair.Key, pair.Value, "v");
				}
			}
			else if(typeof(Vector3OrTransformArray) == valueType) {
				if(typeof(Vector3[]) == pair.Value.GetType()) {
					var value = (Vector3[])pair.Value;
					var vectorIndexes = new ArrayIndexes();
					var indexArray = new int[value.Length];
					for(var i = 0; i < value.Length; ++i) {
						vector3List.Add((Vector3)value[i]);
						indexArray[i] = vector3List.Count - 1;
					}

					vectorIndexes.indexes = indexArray;
					AddToList<ArrayIndexes>(keyList, indexList, vector3ArrayList, metadataList, pair.Key, vectorIndexes, "v");
				}
				else if(typeof(Transform[]) == pair.Value.GetType()) {
					var value = (Transform[])pair.Value;
					var transformIndexes = new ArrayIndexes();
					var indexArray = new int[value.Length];
					for(var i = 0; i < value.Length; ++i) {
						transformList.Add((Transform)value[i]);
						indexArray[i] = transformList.Count - 1;
					}

					transformIndexes.indexes = indexArray;
					AddToList<ArrayIndexes>(keyList, indexList, transformArrayList, metadataList, pair.Key, transformIndexes, "t");
				}
				else if(typeof(string) == pair.Value.GetType())
				{
					AddToList<string>(keyList, indexList, stringList, metadataList, pair.Key, pair.Value, "p");
				}
			}
		}

		keys = keyList.ToArray();
		indexes = indexList.ToArray();
		metadatas = metadataList.ToArray();
		ints = intList.ToArray();
		floats = floatList.ToArray();
		bools = boolList.ToArray();
		strings = stringList.ToArray();
		vector3s = vector3List.ToArray();
		colors = colorList.ToArray();
		spaces = spaceList.ToArray();
		easeTypes = easeTypeList.ToArray();
		loopTypes = loopTypeList.ToArray();
		gameObjects = gameObjectList.ToArray();
		transforms = transformList.ToArray();
		audioClips = audioClipList.ToArray();
		audioSources = audioSourceList.ToArray();
		vector3Arrays = vector3ArrayList.ToArray();
		transformArrays = transformArrayList.ToArray();
	}

	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair) {
		AddToList<T>(keyList, indexList, valueList, metadataList, pair.Key, pair.Value);
	}

	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair, string metadata) {
		AddToList<T>(keyList, indexList, valueList, metadataList, pair.Key, pair.Value, metadata);
	}

	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value) {
		AddToList<T>(keyList, indexList, valueList, metadataList, key, value, null);
	}

	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value, string metadata) {
		keyList.Add(key);
		valueList.Add((T)value);
		indexList.Add(valueList.Count - 1);
		metadataList.Add(metadata);
	}

	void DeserializeValues() {
		values = new Dictionary<string, object>();

		if(null == keys) {
			return;
		}

		for(var i = 0; i < keys.Length; ++i) {
			var mappings = EventParamMappings.mappings[type];
			var valueType = mappings[keys[i]];

			if(typeof(int) == valueType) {
				values.Add(keys[i], ints[indexes[i]]);
			}
			else if(typeof(float) == valueType) {
				values.Add(keys[i], floats[indexes[i]]);
			}
			else if(typeof(bool) == valueType) {
				values.Add(keys[i], bools[indexes[i]]);
			}
			else if(typeof(string) == valueType) {
				values.Add(keys[i], strings[indexes[i]]);
			}
			else if(typeof(Vector3) == valueType) {
				values.Add(keys[i], vector3s[indexes[i]]);
			}
			else if(typeof(Color) == valueType) {
				values.Add(keys[i], colors[indexes[i]]);
			}
			else if(typeof(Space) == valueType) {
				values.Add(keys[i], spaces[indexes[i]]);
			}
			else if(typeof(iTween.EaseType) == valueType) {
				values.Add(keys[i], easeTypes[indexes[i]]);
			}
			else if(typeof(iTween.LoopType) == valueType) {
				values.Add(keys[i], loopTypes[indexes[i]]);
			}
			else if(typeof(GameObject) == valueType) {
				values.Add(keys[i], gameObjects[indexes[i]]);
			}
			else if(typeof(Transform) == valueType) {
				values.Add(keys[i], transforms[indexes[i]]);
			}
			else if(typeof(AudioClip) == valueType) {
				values.Add(keys[i], audioClips[indexes[i]]);
			}
			else if(typeof(AudioSource) == valueType) {
				values.Add(keys[i], audioSources[indexes[i]]);
			}
			else if(typeof(Vector3OrTransform) == valueType) {
				if("v" == metadatas[i]) {
					values.Add(keys[i], vector3s[indexes[i]]);
				}
				else if("t" == metadatas[i]) {
					values.Add(keys[i], transforms[indexes[i]]);
				}
			}
			else if(typeof(Vector3OrTransformArray) == valueType) {
				if("v" == metadatas[i]) {
					var arrayIndexes = vector3Arrays[indexes[i]];
					var vectorArray = new Vector3[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						vectorArray[idx] = vector3s[arrayIndexes.indexes[idx]];
					}

					values.Add(keys[i], vectorArray);
				}
				else if("t" == metadatas[i]) {
					var arrayIndexes = transformArrays[indexes[i]];
					var transformArray = new Transform[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						transformArray[idx] = transforms[arrayIndexes.indexes[idx]];
					}

					values.Add(keys[i], transformArray);
				}
				else if("p" == metadatas[i]) {
					values.Add(keys[i], strings[indexes[i]]);
				}
			}
		}
	}*/
}