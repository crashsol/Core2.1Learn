using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<object> stufff = new object[]
            {
                new object(),
                1,3,4,5,6,"1234",Guid.NewGuid()
            };

            Print("Stuff is ", stufff);
            IEnumerable<int> odd = new int[]
            {
                0,2,4,6,8
            };

            Print("odd", odd);


            IEnumerable<int> even = new int[]
         {
                2,1,3,9,5,7
         };

            Print("even", even);

            Console.WriteLine("-------------------------");

            Print("Union", odd.Union(even));
            Print("Concat", odd.Concat(even));
            Print("Interset", odd.Intersect(even));

            Console.WriteLine("-----------------------------------");

            IEnumerable<Person> c1 = new Person[]
            {
                new Person{ Id =1,Name="1"},
                  new Person{ Id =2,Name="2"},
                    new Person{ Id =3,Name="3"},
                      new Person{ Id =4,Name="4"},
            };

            IEnumerable<Person> c2 = new Person[]
            {
                 new Person{ Id =3,Name="3",Age=11},
                 new Person{ Id =5,Name="5"},
                    new Person{ Id =6,Name="6"},
                new Person{ Id =7,Name="7"},          
                  
            };

            Console.WriteLine("------------------------------");

            Print("Person Union", c1.Union(c2));
            Print("Person Concat", c1.Concat(c2));
            Print("Person Intersect", c1.Intersect(c2));


            Console.ReadKey();
        }
        private static void Print<T>(string format, IEnumerable<T> items)
       => Console.WriteLine($"{format}:{string.Join(",", items.Select(b => b.ToString()))}");

        private static void Print<T>(string format, T t)
        => Console.WriteLine($"{format}:{t.ToString()}");
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; } = 10;        

        public override string ToString()
        {
            return $"{Id}-{Name}-{Age}";
        }

        public override bool Equals(object obj)
        {
            if(obj ==null)
            { return false; }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            if(this.GetHashCode()!= obj.GetHashCode())
            {
                return false;
            }
            Person person = obj as Person;
            if(this.Id == person.Id && this.Name == person.Name && this.Age == person.Age)
            {
                return true;
            }
            else {return  false; }           
        }

      
        public override int GetHashCode()
        {
            int hashCode = Id.GetHashCode();
            return hashCode ^= Name.GetHashCode();
        }

        public static  bool operator == (Person left,Person right)
        {
            if (left is null)
            {
                return right is null;
            }
            return (left.Equals(right));
        }

        public static bool operator !=(Person left,Person right)
        {
            return !(left == right);
        }


    }


}
