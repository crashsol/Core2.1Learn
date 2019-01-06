using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObjectEqualApp
{
    class Program
    {
        static void Main(string[] args)
        {

            int a = 5;
            int b = 5;
            Console.WriteLine("Struct Type------------ == -------");
            Console.WriteLine(a == b);

            object c = 33;
            object d = 33;
            Console.WriteLine($"Reference Type------- == -----");
            Console.WriteLine($"c == d: {c == d }");
            Console.WriteLine($"c.Equals(d): { c.Equals(d) }");


            Console.WriteLine($"c hashCode :{c.GetHashCode()}");
            Console.WriteLine($"d hashCode :{d.GetHashCode()}");


            Person p1 = new Person();
            Person p2 = new Person();

            Console.WriteLine($"p1 == p2,{p1 == p2} hashCode :{p1.GetHashCode()}");
            Console.WriteLine($"p1.Equals(p2),{p1.Equals(p2)},hashCode:{p2.GetHashCode()} ");

            Console.WriteLine("-------------------Area Compare");
            Area a1 = new Area(5,10);
            Area a2 = new Area(10,5);
            Console.WriteLine($"a1 == a2,{a1 ==a2} ");
            Console.WriteLine($"a1.Equals(a2),{a1.Equals(a2)} ");
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        internal class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreateTime { get; set; }
        }


        /// <summary>
        /// struct 重载相等和equal 
        /// </summary>
        struct Area : IEquatable<Area>
        {

            public readonly int Measure1;
            public readonly int Measure2;

            public Area(int m1, int m2)
            {
                Measure1 = Math.Min(m1, m2);
                Measure2 = Math.Max(m1, m2);

            }

            public bool Equals(Area other) => Measure1 == other.Measure1 && Measure2 == other.Measure2;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Area other && Equals(other);
            }

            public static bool operator ==(Area a1, Area a2) => a1.Equals(a2);

            public static bool operator !=(Area a1, Area a2) => !a1.Equals(a2);

            public override int GetHashCode()
            {
                return Measure2 * 31 + Measure1;
            }
        }





        interface IDateTime
        {
           DateTime CurreDateTime { get; }
        }

        public class  SystemTime:IDateTime{
         
            public DateTime CurreDateTime => DateTime.UtcNow;
        }
    }
}
