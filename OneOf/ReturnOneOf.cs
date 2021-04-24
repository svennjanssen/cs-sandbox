using System;

namespace cs_sandbox {
    public interface IReturnOneOf {
        OneOf<Ok, BadRequest, Forbid> Return();
    }
    public class ReturnOneOf : IReturnOneOf
    {
        private readonly Random random = new Random();

        public ReturnOneOf()
        {
        }

        public OneOf<Ok, BadRequest, Forbid> Return()
        {
            var randomNumber = random.Next(1, 10);

            return random.Next(1, 10) switch {
                >= 1 and <= 3 => new Ok(),
                >= 4 and <= 6 => new BadRequest(),
                >= 7 and <= 9 => new Forbid(),
                _ => throw new Exception("This cannot be hit")
            };
        }
    }
}