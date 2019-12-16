using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class CalculatorOutput
    {
        public List<int> Operands { get; }
        public string Operator { get; }
        public double Result { get; }

        public CalculatorOutput(List<int> operands, double result, string oper)
        {
            Operands = operands;
            Operator = oper;
            Result = result;
        }

    }
}
