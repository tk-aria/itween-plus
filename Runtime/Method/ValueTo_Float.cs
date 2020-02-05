
namespace AriaPlugin.Runtime.iTweenHelper
{
	public class ValueTo_Float : ValueTo<float> 
	{
		protected override void Reset()
		{
			base.Reset();

			from = 0f;
			to = 1f;
			onupdate = "SetFloat";
		}
	}
}
