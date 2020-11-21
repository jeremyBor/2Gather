namespace Utility
{
	using System;

	public class EventVariable<T> : IEventVariable<T>
	{
		public EventVariable(T startValue = default, Action<T> onChanged = default)
		{
			_value = startValue;
			this.onChanged = onChanged;
		}

		public event Action<T> onChanged;

		private T _value;

		public T value
		{
			get => _value;
			set
			{
				if ((_value == null && value == null) || (_value != null && _value.Equals(value)))
				{
					return;
				}

				_value = value;
				onChanged?.Invoke(value);
			}
		}
	}
}