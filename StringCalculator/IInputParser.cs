using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    interface IInputParser
    {
        List<int> Parse(string input);
    }
}
