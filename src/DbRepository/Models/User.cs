using System;
using System.Collections.Generic;
using System.Text;

namespace DbRepository.Models
{
    public partial class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
