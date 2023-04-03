namespace Domain.Exceptions;

public class OperationNotAllowedException : Exception
{
	public OperationNotAllowedException()
		: base("operation not allowed")
	{
	}

    public OperationNotAllowedException(string message)
        : base(message)
    {
    }
}
