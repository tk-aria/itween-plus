using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AriaSDK.Runtime.Common
{
	/// <summary>
	///  .
	/// </summary>
	public sealed class ShaderPropertyComponent : MonoBehaviour
	{
		#region Field

		[SerializeField] private bool isGlobal = false;
		[SerializeField] private Renderer renderer_ = null;
		[SerializeField] private string propertyName = "_Color";
		private ShaderProperty shaderProp = null;

		#endregion // Field End.

		#region Method

		private void Reset()
		{
			renderer_ = GetComponent<Renderer>() ?? null;
		}

		private void Start()
		{
			var shader = isGlobal ? renderer_.GetComponent<Material>() : renderer_.material;
			shaderProp = new ShaderProperty(shader, propertyName);
		}

		public void SetFloat(float value){ shaderProp.SetFloat(value); }
		public void SetAlpha(float alpha){ shaderProp.SetAlpha(alpha); }
		public void SetColor(Color color){ shaderProp.SetColor(color); }
		public void SetTexture(Texture texture){ shaderProp.SetTexture(texture); }

		#endregion // Method End.
	}

}
