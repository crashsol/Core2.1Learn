using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task Run");
            {


                //Action<object> action = (object obj) =>
                //{

                //    Console.WriteLine($"Task= { Task.CurrentId},obj={obj},Thread={Thread.CurrentThread.ManagedThreadId}");

                //};
                //// Create a task but do not start it.
                //Task t1 = new Task(action, "alpha");

                //// Construct a started task
                //Task t2 = Task.Factory.StartNew(action, "beta");
                //t2.Wait();

                //// Launch t1 
                //t1.Start();
                //Console.WriteLine("t1 has been launched. (Main Thread={0})",
                //                  Thread.CurrentThread.ManagedThreadId);
                //// Wait for the task to finish.
                //t1.Wait();

                //// Construct a started task using Task.Run.
                //String taskData = "delta";
                //Task t3 = Task.Run(() => {
                //    Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                //                      Task.CurrentId, taskData,
                //                       Thread.CurrentThread.ManagedThreadId);
                //});
                //// Wait for the task to finish.
                //t3.Wait();

                //// Construct an unstarted task
                //Task t4 = new Task(action, "gamma");
                //// Run it synchronously
                //t4.RunSynchronously();
                //// Although the task was run synchronously, it is a good practice
                //// to wait for it in the event exceptions were thrown by the task.
                //t4.Wait();
            }


            {
                //var task = Task.Run(async () =>
                //{
                //    while (DateTime.Now.Minute < 36)
                //    {
                //        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                //        await Task.Delay(3000);
                //    }
                //});

                //Console.WriteLine("111111111");
                //task.Wait();
                //Console.WriteLine("222222222");
            }

            {


                //Task taskA = Task.Run(() => Thread.Sleep(5000));
                //Console.WriteLine("taskA Status: {0}", taskA.Status);

                //try
                //{
                //    taskA.Wait();
                //    Console.WriteLine("taskA Status: {0}", taskA.Status);
                //}
                //catch (AggregateException ex)
                //{
                //    Console.WriteLine($"Exception in taskA.{ex.ToString()}");

                //}
            }


            // Wait on a single task with a timeout specified.
            Task taskA = Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Task 2222");
                    throw new Exception("Error");
                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                   // throw ex;
                }
               
            } );
            try
            {
                taskA.Wait();       // Wait for 1 second.
                bool completed = taskA.IsCompleted;
                Console.WriteLine("Task A completed: {0}, Status: {1}",
                                 completed, taskA.Status);
                if (!completed)
                    Console.WriteLine("Timed out before task A completed.");
            }
            catch (AggregateException)
            {
                Console.WriteLine("Exception in taskA.");
            }



            Console.ReadLine();
        }
    }
}
