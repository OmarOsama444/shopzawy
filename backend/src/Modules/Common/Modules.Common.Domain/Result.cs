using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Domain
{
    public class Result
    {
        public bool isSuccess { get; init; }
        public Exception? exception { get; init; }
        protected Result(Exception exception)
        {
            this.isSuccess = false;
            this.exception = exception;
        }
        protected Result()
        {
            this.isSuccess = true;
            this.exception = null;
        }
        public static Result Failure(Exception exception)
        {
            return new Result(exception);
        }
        public static Result Success()
        {
            return new Result();
        }
        public TT Match<TT>(Func<TT> Succ, Func<Exception, TT> Fail)
        {
            if (isSuccess)
                return Succ.Invoke();
            return Fail.Invoke(exception!);
        }
        public static implicit operator Result(Exception exception) => new Result(exception);
    }

    public class Result<T> : Result
    {
        public T? _value;
        public T Value
        {
            get
            {
                if (isSuccess)
                    return _value!;
                throw new InvalidOperationException("You can get value from failure result");
            }
            private set
            {
                _value = value;
            }
        }
        private Result(T Value) : base() { this.Value = Value; }
        private Result(Exception exception) : base(exception) { }
        public TT Match<TT>(Func<T, TT> Succ, Func<Exception, TT> Fail)
        {
            if (isSuccess)
                return Succ.Invoke(Value);
            return Fail.Invoke(exception!);
        }
        public static Result<TValue> Success<TValue>(TValue Value) => new Result<TValue>(Value);
        public static Result<TValue> Failure<TValue>(Exception exception) => new Result<TValue>(exception);
        public static implicit operator Result<T>(T Value) => new Result<T>(Value);
        public static implicit operator Result<T>(Exception exception) => new Result<T>(exception);
    }
}
