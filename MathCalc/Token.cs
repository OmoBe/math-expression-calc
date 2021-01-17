using System;
using System.Collections.Generic;
using System.Text;

namespace MathCalc
{
    public class Token
    {
        public Token(char SymbolToken, double Val)
        {
            Symbol = SymbolToken;
            Value = Val;
        }

        public Token(char SymbolToken)
        {
            Symbol = SymbolToken;
            Value = null;
        }

        public char Symbol { get; set; }
        public double? Value { get; set; }
    }
}
