using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public void CreatedDate()
        {
            CreateAt = DateTime.Now;
        }

        public void ChangedDate()
        {
            UpdateAt = DateTime.Now;
        }
    }
}
