//IZoomExtension interface
public interface IZoomExtension
{
	double ZoomFactor
	{
		get;
		set;
	}
}

public class myIZoomExtension: IZoomExtension
{
	public double ZoomFactor 
	{
		get 
		{
			return 0;
		}

		set 
		{
		}
	}
}