using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Con
{
    public class Parent
    {

        public virtual string GetStr()
        {
            return "parnt";
        }

        public string CombineStr()
        {
            return GetStr() + "_" + Guid .NewGuid() .ToString("N");
        }
    }
}
