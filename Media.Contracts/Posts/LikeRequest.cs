using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Contracts.Posts
{
    public class LikeRequest
    {
        [Required]
        public string PostId { get; set; } = default!;
        [Required]
        public Guid UserId { get; set; }
    }
}
