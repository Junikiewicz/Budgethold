using Budgethold.Domain.Common.Errors;
using Budgethold.Domain.Common.Exceptions;

namespace Budgethold.Domain.Common
{
    public class Result
    {
        public Result() { }

        public Result(Error error)
        {
            Error = error;
        }

        public Error? Error { get; init; }

        public bool Succeeded => Error == default;
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
                if (Error != default)
                {
                    throw new UnhandledErrorResultException($"Unhandled error result: {Error.Message}");
                }

                return _value!;
            }
        }
    }

    public class CreatedResult<T> : Result
    {
        private readonly T? _value;

        public CreatedResult(T value)
        {
            _value = value;
        }

        public CreatedResult(Error error) : base(error)
        {
            _value = default;
        }

        public T Value
        {
            get
            {
                if (_error != default)
                {
                    throw new UnhandledErrorResultException($"Unhandled error result: {_error.Message}");
                }

                return _value!;
            }
        }
    }
}
