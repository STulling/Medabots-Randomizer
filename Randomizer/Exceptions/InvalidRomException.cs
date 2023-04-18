using System;

namespace MedabotsRandomizer.Exceptions
{
	public class InvalidRomException : Exception
	{
		public InvalidRomException() { }

		public InvalidRomException(string message)
			: base(message)
		{
		}

		public InvalidRomException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
