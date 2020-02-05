using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	/// <summary>
	///  [ここに何をするクラスなのかを記入する].
	/// </summary>
	public class ShaderProperty : MonoBehaviour
	{
		#region Field

		[SerializeField] protected Renderer renderer;
		public string propertyName = "_Color";

		#endregion // Field End.

		#region Method

		protected void Reset()
		{
			renderer = GetComponent<Renderer>() ?? null;
		} 

		public void SetFloat(float value)
		{
			if (renderer == null)
			{
				return;
			}
			var mat = renderer.material;
			mat.SetFloat(propertyName, value);
		}

		public void SetAlpha(float alpha)
		{
			if (renderer == null)
			{
				return;
			}
			var mat = renderer.material;
			var color = mat.GetColor(propertyName);
			mat.SetColor(propertyName, new Color(color.r, color.g, color.b, alpha));
		}

		public void SetColor(Color color)
		{
			if (renderer == null)
			{
				return;
			}
			var mat = renderer.material;
			mat.SetColor(propertyName, color);
		}

		public void SetTexture(Texture texture)
		{
			if (renderer == null)
			{
				return;
			}
			var mat = renderer.material;
			mat.SetTexture(propertyName, texture);
		}

		#endregion // Method End.
	}
}