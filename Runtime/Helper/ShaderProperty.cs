using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaPlugin.Runtime.Common
{
	/// <summary>
	///  
	/// </summary>
	[Serializable]
	public class ShaderProperty
	{
		public Material shader = null;
		public string propertyName = "_Color";

		public ShaderProperty(Material material, string propertyName)
		{
			this.shader = material;
			this.propertyName = propertyName;
		}

		public void SetFloat(float value)
		{
			shader.SetFloat(propertyName, value);
		}

		public void SetAlpha(float alpha)
		{
			var color = shader.GetColor(propertyName);
			shader.SetColor(propertyName, new Color(color.r, color.g, color.b, alpha));
		}

		public void SetColor(Color color)
		{
			shader.SetColor(propertyName, color);
		}

		public void SetTexture(Texture texture)
		{
			shader.SetTexture(propertyName, texture);
		}
	}

}
