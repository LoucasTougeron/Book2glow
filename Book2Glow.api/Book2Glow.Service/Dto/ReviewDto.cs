using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Dto
{
    public class ReviewDto
    {
        public Guid BookId { get; set; }
        public int Stars { get; set; }
        public string Comments { get; set; }
        public DateOnly DateTime { get; set; }
    }
}
