using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TestScene scene = new TestScene())
                scene.Run();
        }
    }
}
