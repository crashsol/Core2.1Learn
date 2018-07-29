using System;
using System.Reflection;

namespace 反射
{
    class Program
    {
        static void Main(string[] args)
        {

            Person person = new Person();

            //常用的两种方法获取 Person Type对象 
            Type type = person.GetType();
            Type type2 = typeof(Person);


            //Activator.CreateInstance 动态创建一个实例化对象
            //类必须要有 public 无参数构成函数
            object obj = Activator.CreateInstance(type); //调用类的无参构造函数

            Person p = (Person)obj;
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        //每一个Type对应一个类
        //MethodInfo对应他的方法
        class Person
        {

        }
    }
}
