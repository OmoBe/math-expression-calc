using System;

/// <summary>
/// Summary description for Solver
/// </summary>
class Solver
{
    ArrayList get_solution(int n)
    {
        ArrayList result = new ArrayList();
        String item;

        //Loop from 1 to the parameter n
        for (int i = 1; i <= n; i++)
        {
            //Perform modulo division and assign value to item variable, based on result of division
            item = (i % 3 == 0) ? "Fizz" : String.valueOf(i);
            item = (i % 5 == 0) ? "Buzz" : item;
            item = (i % 15 == 0) ? "Fizz Buzz" : item;
            //add assigned value to array
            result.add(item);
        }

        return result;

    }
}