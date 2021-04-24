using System;
using System.Linq;
using SJDI;
using SJDI.Impl;

namespace cs_sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // var runCount = args.Any() ? int.Parse(args[0]) : 1;
            
            // var returner = new ReturnOneOf();

            // var i = 0;
            // while(i++ < runCount){
            //     var result = returner.Return();
            //     Console.WriteLine(result.Match(_ => "Ok", _ => "BadRequest", _ => "Forbid"));
            // }

            var initializer = new Initializer();

            initializer.Resolve<ClassB>();
        }
    }
}
