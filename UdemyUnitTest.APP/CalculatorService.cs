using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyUnitTest.APP
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return 0;
            }

            return a + b;
        }


        public int Multip(int a, int b)
        {
            throw new Exception("a=0 olamaz");
            return a * b;
        }
    }
}
