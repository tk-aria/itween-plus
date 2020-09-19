using System;
using System.Collections;
using UnityEngine;


public class CoroutineSample : MonoBehaviour
{

	void Start()
	{
		StartCoroutine(TitleAnimation());
	}

	IEnumerator TitleAnimation()
	{
		yield return new iTween.WaitForAnimation(
			iTween.MoveTo, this.gameObject, iTween.Hash(
				"x", 0f,
				"islocal", true));

		yield return new iTween.WaitForAnimation(
			iTween.ShakePosition, this.gameObject, iTween.Hash(
				"amount", new Vector3(3f,3f,0f),
				"time", 0.5f));
	}
}
