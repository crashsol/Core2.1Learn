using System;
using System.Linq.Expressions;


namespace ExpressionApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Expression<Func<int, int, bool>> expression;
            expression = (x, y) => x > y;
            Console.WriteLine($"---------------{expression}---------------------{System.Environment.NewLine}");

            
            Console.WriteLine(); 
            Console.WriteLine();

            Console.Read();
        }

       
    }
}
