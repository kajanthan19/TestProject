using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVersion
{
   public class Program
    {
        public static void Main(string[] args)
        {
            GitVersion task = new GitVersion();
            task.BuildEngine = new MockBuild();
            task.ToolPath = @"C:\Program Files (x86)\Git\bin";

            string prjRootPath = TaskUtility.GetProjectRootDirectory(true);
            task.LocalPath = Path.Combine(prjRootPath, @"Source");

            bool result = task.Execute();

        }
    }
}
