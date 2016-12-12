using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Con
{
    class Program
    {
        static void Main(string[] args)
        {

            Parent  s = new Son();
            var r = s.CombineStr();
            Console.Write(r);
            Console.ReadKey();
        }
    }
}
