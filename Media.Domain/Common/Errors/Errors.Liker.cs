using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Liker
        {
            public static Error NotFoundLiker => Error.NotFound(
                code: "Can't like",
                description: "This user can't like.");
            public static Error DuplicatedLike => Error.NotFound(
                code: "Can't dislike",
                description: "This user can't dislike.");
        }
    }
}
