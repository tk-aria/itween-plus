
namespace AriaSDK.Runtime.iTweenPro
{
	public sealed class ValueTo_Float : ValueTo<float> 
	{
		protected override void Reset()
		{
			base.Reset();

			from = 0f;
			to = 1f;

			foreach (var param in animationTargetList)
			{
				param.onupdate.invokeMethod = "SetFloat";
			}
		}
	}
}
