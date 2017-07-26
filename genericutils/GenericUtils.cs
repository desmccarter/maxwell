using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uk.org.hs2.genericutils
{
    public class GenericUtils
    {
        private static Random random = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomDigits(int length)
        {
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void CreateFolder(string folder)
        {
            if( !Directory.Exists(folder) )
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static void CreateTextContentFile(string file, string content)
        {
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            using (System.IO.StreamWriter writer = System.IO.File.CreateText(file))
            {
                writer.Write(content);
            }
        }

        public static void CallProtectedMethod(object objectContainingMethod, 
            string methodName, object[] parameters)
        {
            // *** get test function name ...
            MethodInfo methodInfo =
                objectContainingMethod.GetType().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);

            // *** throw error if test function not defined ...

            if (methodInfo == null)
            {
                throw new Exception("[ERR] Private method '" + methodName + "' does not exist in object '"+
                    objectContainingMethod.GetType().ToString());
            }

            methodInfo.Invoke(objectContainingMethod, parameters);
        }

        public static Stream GetResourceStream(string resourceFile)
        {
            StackFrame[] s1 =
                new StackTrace().GetFrames();

            Stream[] s =
                new StackTrace().GetFrames().Where(
                frame => frame.GetMethod().DeclaringType.Assembly.
                GetManifestResourceStream(resourceFile) != null).
                Select(fr => fr.GetMethod().DeclaringType.Assembly.
                GetManifestResourceStream(resourceFile)).ToArray();

            if( (s==null) || (s.Length==0) )
            {
                throw new Exception("[ERR] Resource not found! :" + resourceFile);
            }

            return s[0];
        }
    }
}
