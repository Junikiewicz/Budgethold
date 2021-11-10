using Budgethold.Domain.Common.Errors;

namespace Budgethold.Domain.Common
{
    public class Result
    {
        protected readonly Error? _error;

        public Result() { }

        public Result(Error error)
        {
            _error = error;
        }

        public bool Succeeded => _error == default;

        public Error? Error => _error;
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public Result(T value)
        {
            _value = value;
        }

        public Result(Error error) : base(error)
        {
            _value = default;
        }

        public T Value
        {
            get
            {
                if (_error != default)
                {
                    throw new Exception(string.Empty); // TODO: Update exception
                }

                return _value!;
            }
        }
    }
}
