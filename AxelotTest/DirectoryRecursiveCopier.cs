using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxelotTest
{
    internal class DirectoryRecursiveCopier : ICopier
    {
        private static DirectoryRecursiveCopier _instance;

        private DirectoryRecursiveCopier()
        {
        
        }

        public static DirectoryRecursiveCopier GetCopier()
        {
            if (_instance == null)
            {
                return new DirectoryRecursiveCopier();
            }

            return _instance;
        }

        public void Copy(string src, string dest)
        {
              Console.WriteLine("Copiing: " + Path.GetFileName(src));
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(dest), Path.GetFileName(src)));
                // First create all of the directories
                foreach (string directory in Directory.GetDirectories(src, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Directory.CreateDirectory(directory.Replace(src, dest));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                // Copy all the files
                foreach (string file in Directory.GetFiles(src, "*.*", SearchOption.AllDirectories))
                {
                    Console.WriteLine("Copiing: " + file);
                    File.Copy(file, file.Replace(src, dest), true);
                    Manager.TotalAmount += new FileInfo(file).Length;
                }
        }
    }
}
