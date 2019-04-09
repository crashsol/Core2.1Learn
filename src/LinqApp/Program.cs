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

            Console.WriteLine("\r\n\r\n");


            Console.WriteLine("--------------------Linq Join -----------------------------");
            var departMents = Helper.Departments();
            var employeies = Helper.Employees();
            var joinResult = employeies.Join(departMents, a => a.DepartmentId, b => b.Id, (a, b) => new { a, b }).ToList();
            Console.WriteLine("Join出来的数据是一对一，在双方的集合中都存在的");
            foreach (var item in joinResult)
            {
                Console.WriteLine($"{item.a.Id}-{item.a.UserName}\t{item.b.Id},{item.b.Name}");
            }

            var joinResult1 = departMents.Join(employeies, a => a.Id, b => b.DepartmentId, (a, b) => new { a, b }).ToList();
            foreach (var item in joinResult1)
            {
                Console.WriteLine($"{item.a.Id}-{item.a.Name}\t{item.b.Id},{item.b.UserName}");
            }

            Console.WriteLine("--------------------- GroupJoin ---------------------------");
            var groupJoin = departMents.GroupJoin(employeies, a => a.Id, b => b.DepartmentId, (a, b) => new { a, b });
            foreach (var item in groupJoin)
            {
                Console.WriteLine($"{item.a.Name}:");
                foreach (var employee in item.b)
                {
                    Console.WriteLine($"{employee.UserName}\t");
                }
            }
            Console.WriteLine("--------- GroupJoin outer ,及时depart中" +
                "没有用户也会显示出depart名称！--------------------");
            var grouyJoin2 = departMents.GroupJoin(employeies, a => a.Id, b => b.DepartmentId, (depart, employee) =>
            new { depart, employeies = employee ?? null }).ToList();
            foreach (var item in grouyJoin2)
            {
                Console.WriteLine($"{item.depart.Name}:");
                foreach (var employee in item.employeies)
                {
                    Console.WriteLine($"{employee.UserName}\t");
                }
            }

            Console.WriteLine("------------------------");

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<TempProjectionItem> temp = from n in names
                                                   select new TempProjectionItem
                                                   {
                                                       Original = n,
                                                       Vowellness = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                                                   };
            var temp2 = names.Select(b => new TempProjectionItem
            {
                Original = b,
                Vowellness = b.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
            });

            foreach (var item in temp)
            {
                Console.WriteLine($"{item.Original} -{item.Vowellness}");
            }
            Console.WriteLine("----------------------");
            foreach (var item in temp2)
            {
                Console.WriteLine($"{item.Original} -{item.Vowellness}");
            }

            Console.WriteLine("--------------------------");
            var query = from n in names
                select new
                {
                    Original = n,
                    Vowellness = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                }
                into mid
                where mid.Vowellness.Length > 2
                select mid.Original;
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

               

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
            if (obj == null)
            { return false; }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }
            Person person = obj as Person;
            if (this.Id == person.Id && this.Name == person.Name && this.Age == person.Age)
            {
                return true;
            }
            else { return false; }
        }


        public override int GetHashCode()
        {
            int hashCode = Id.GetHashCode();
            return hashCode ^= Name.GetHashCode();
        }

        public static bool operator ==(Person left, Person right)
        {
            if (left is null)
            {
                return right is null;
            }
            return (left.Equals(right));
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }


    }

    public class Helper
    {
        public static List<Department> Departments()
        {
            return new List<Department>
            {
                new Department{ Id =1,Name="Department-1"},
                new Department{ Id =2,Name="Department-2"},
                new Department{ Id =3,Name="Department-3"},
                new Department{ Id =4,Name="Department-4"},
                new Department{ Id =5,Name="Department-5"},
                new Department{ Id =6,Name="Department-6"}

            };
        }

        public static List<Employee> Employees()
        {
            var result = new List<Employee>();

            for (int i = 1; i < 8; i++)
            {
                result.Add(new Employee { Id = i, UserName = $"UserName-{i}", DepartmentId = i % 5  });
            }
            return result;
        }
    }



    class TempProjectionItem
    {

        public string Original { get; set; }
        public string Vowellness { get; set; }
    }


}
