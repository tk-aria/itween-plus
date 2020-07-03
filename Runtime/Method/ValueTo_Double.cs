
namespace AriaPlugin.Runtime.iTweenHelper
{
	public sealed class ValueTo_Double : ValueTo<double> 
	{
		protected override void Reset()
		{
			base.Reset();

			from = 0;
			to = 1;
			
			foreach (var param in animationTargetList)
			{
				param.onupdate.invokeMethod = "SetDouble";
			}
		}
	}
}
