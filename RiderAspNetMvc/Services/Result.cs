using System;
using System.Collections.Generic;

namespace RiderAspNetMvc.Services {
    public class Result {
        public bool Succeded { get; }
        public List<string> Errors { get; } = new List<string>();

        private Result() => Succeded = true;
        private Result(string error) => Errors.Add(error);

        private Result(Exception exception) {
            while (exception != null) {
                Errors.Add(exception.Message);
                exception = exception.InnerException;
            }
        }

        public static Result Success() => new Result();
        public static Result Fail(string message) => new Result(message);
        public static Result Fail(Exception exception) => new Result(exception);

        public static implicit operator bool(Result result) => result.Succeded;
    }
}