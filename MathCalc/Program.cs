using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MathCalc
{
    class Program
    {
        private static StringBuilder sb;
        static void Main(string[] args)
        {
            string input; 
            var pattern = @"^([-+/*]?|[(]?\d+(\d+)?[)]?)*$";

            do
            {
                Console.WriteLine("Please enter your Expression:\n");
                input = Console.ReadLine();

            } while (!Regex.IsMatch(input, pattern));
            
            //Read the input expression string from the stringbuilder
            sb = new StringBuilder(input);
            Console.WriteLine($"Answer is: {Expression()}");
            
            Console.Read();
            //^([-+/*]?|[(]?\d+(\d+)?[)]?)*
            //^([-+/*\d]?|[(]?[\d]+[-+/*\d]+[)]?)*
        }

        //Break expression input string up into tokens
        private static Token GetToken()
        {
            Token t = null;
            StringBuilder Elem = null;

            if (sb.Length > 0)
            {
                if (char.IsDigit(sb[0]))
                {
                    //Get the full number from the input
                    var numbers = new string(sb.ToString()
                        .TakeWhile(c => char.IsDigit(c))
                        .ToArray());

                    Elem = new StringBuilder(numbers);

                    //Remove this number from the input string
                    sb.Remove(0, Elem.Length); 
                    return new Token('8', double.Parse(Elem.ToString()));
                }

                t = new Token(sb[0]);
                sb.Remove(0, 1);
                return t;
            }

            return new Token(';');
        }

        private static void PutTokenBack(char Symbol)
        {
            sb.Insert(0, Symbol);
        }

        private static double Element()
        {
            Token t = GetToken();
            double d = 0;
            switch (t.Symbol)
            {
                case '(':
                    {
                        d = Expression();
                        t = GetToken();
                        if (!t.Symbol.Equals(')')) throw new InvalidOperationException();
                        break;
                    }
                case '8':            //'8' to represent a number
                    return (double)t.Value;// return the number's value
            }
            return d;
        }

        private static double Expression()
        {
            double left = Multiply();      // read and evaluate a Term
            Token t = GetToken();        // get the next token from token stream

            while (true)
            {
                switch (t.Symbol)
                {
                    case '+':
                        left += Multiply();    // evaluate Term and add; Perform all multiplications first
                        break;
                    case '-':
                        left -= Multiply();    // evaluate Term and subtract
                        break;
                    default:
                        PutTokenBack(t.Symbol);      // put t back into the expression input string
                        return left;       // finally: no more + or -: return the answer
                }
                return left;
            }
        }

        private static double Multiply()
        {
            double left = Element();
            Token t = GetToken();        // get the next token from token stream

            while (true)
            {
                switch (t.Symbol)
                {
                    case '*':
                        left *= Element();
                        break;
                    default:
                        PutTokenBack(t.Symbol);     // put t back into the input string
                        return left;
                }
                return left;
            }
        }

    }
}
