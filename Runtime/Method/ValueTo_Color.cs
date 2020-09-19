using UnityEngine;

namespace AriaSDK.Runtime.iTweenPro
{
	public sealed class ValueTo_Color : ValueTo<Color> 
	{
		protected override void Reset()
		{
			base.Reset();

			from = new Color(0f, 0f, 0f, 0f);
			to = new Color(1f, 1f, 1f, 1f);

			foreach (var param in animationTargetList)
			{
				param.onupdate.invokeMethod = "SetColor";
			}
		}
	}
}
