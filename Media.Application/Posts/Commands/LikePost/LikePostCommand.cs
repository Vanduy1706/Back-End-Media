﻿using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Commands.LikePost
{
    public class LikePostCommand : IRequest<ErrorOr<PostResult>>
    {
        public string PostId { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
