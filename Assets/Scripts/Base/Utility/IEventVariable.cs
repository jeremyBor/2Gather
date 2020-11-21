namespace Utility
{
	using System;

	public interface IEventVariable<out T>
	{
		T value { get; }

		event Action<T> onChanged;
	}
}