using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        //public static async Task test1()
        //{
        //    Task.Delay(2000).GetAwaiter().GetResult();
        //    Console.WriteLine("1");

        //    // tra lai luong khi gap thang AWAIT DAU` TIEN ma TASK CHUA COMPLETED

        //    // Yield, 1 ham async chua chac da chay async (tuc la van dang chay sync)
        //    // Dung task.yield de dam bao ham` async chay 1 cach async
        //}

        //public static Task GetTaskCompleted()
        //{
        //    return Task.CompletedTask;
        //}

        //public static async Task test2()
        //{
        //    Task.Delay(1000).GetAwaiter().GetResult();
        //    Console.WriteLine("2");
        //}
        //public static async Task Main(string[] args)
        //{
        //    var sw = Stopwatch.StartNew();

        //    await Task.WhenAll(test1(), test2());

        //    sw.Stop();
        //    Console.WriteLine(sw.ElapsedMilliseconds);

        //    Console.ReadKey();
        //}
    }
}
