using UnityEngine;

namespace AriaPlugin.Runtime.iTweenHelper
{
	public class ValueTo_Color : ValueTo<Color> 
	{
		protected override void Reset()
		{
			base.Reset();

			from = new Color(0f, 0f, 0f, 0f);
			to = new Color(1f, 1f, 1f, 1f);
			onupdate = "SetColor";
		}
	}
}
