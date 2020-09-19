using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AriaSDK.Runtime.iTweenPro;

namespace AriaSDK.Runtime.Samples
{
	public sealed class AnimationEventSample : MonoBehaviour
	{
		//iTweenHashObject hashObject;

		IEnumerator StartAnimation(iTweenHashObject hashObject)
		{
			var tweenHash = hashObject.ToHashtable();
			
			yield return new iTweenEvent.WaitForAnimation(this.gameObject, hashObject);
			//yield return new iTween.WaitForAnimation(
			//	iTween.MoveTo, this.gameObject, tweenHash
			//);
			//this.gameObject.AddComponent<>
		}

		void OnNotifyValue(AnimationEvent value)
		{
			//var animEventParam = new AnimationEventParameter()
			//floatValue  = value.floatParameter;
			//intValue = value.intParameter;
			//stringValue = value.stringParameter;
			// = value.objectReferenceParameter;
			
			StartCoroutine(StartAnimation(value.objectReferenceParameter as iTweenHashObject));
		}

		/*
		void OnNotifyValue_Int(int value)
		{
			var animEventParam = new AnimationEventParameter()
			{
				intValue = value,
			};
			OnNotifyAnimationEvent(animEventParam);
		}

		void OnNotifyValue_Float(float value)
		{
			var animEventParam = new AnimationEventParameter()
			{
				floatValue = value,
			};
			OnNotifyAnimationEvent(animEventParam);
		}

		void OnNotifyValue_String(string value)
		{
			var animEventParam = new AnimationEventParameter()
			{
				stringValue = value,
			};
			OnNotifyAnimationEvent(animEventParam);
		}

		void OnNotifyValue_Object(UnityEngine.Object value)
		{
			var animEventParam = new AnimationEventParameter()
			{
				objectReferenceValue = value,
			};
			OnNotifyAnimationEvent(animEventParam);
		}
		*/
	}
}
