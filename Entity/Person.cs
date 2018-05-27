using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Person
    {
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual string SocialCode { get; set; }
    }
}
