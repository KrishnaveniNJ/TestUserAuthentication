using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestUserAuthentication
{
    public class ErrorLog
    {
        public void WriteToLog(string message)
        {

            //---------------------------------------
            var path = Path.Combine("Content", "ErrorLog.txt");
            using (var mutex = new Mutex(false, "LARGEFILE"))
            {
                mutex.WaitOne();
                File.AppendAllText(path, " " + message + Environment.NewLine);
                mutex.ReleaseMutex();
            }
          



        }

    }
}



