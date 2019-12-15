using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class CalculatorOutput
    {
        public CalculatorOutput(List<int> operands, int result)
        {
            Operands = operands;
            Result = result;
        }
        public List<int> Operands { get; set; }
        public int Result { get; set; }
    }
}
