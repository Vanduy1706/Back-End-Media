using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Common
{
    public class LikerResult
    {
        public string PostId { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
