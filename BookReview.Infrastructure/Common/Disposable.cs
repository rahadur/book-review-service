namespace BookReview.Infrastructure.Common;

public class Disposable : IDisposable
{
	private bool disposed = false;

	~Disposable()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!disposed && disposing)
		{
			DisposeContext();
		}
		disposed = true;
	}


	protected virtual void DisposeContext() { }
}

