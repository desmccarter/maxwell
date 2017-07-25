using System;
using System.Diagnostics;
using uk.org.hs2.genericutils;

namespace logging
{
    public enum LogOutputTypeEnum
    {
        Info = 0,
        Warn,
        Debug,
        Error,
        NpsStep
    }

    public class Log
    {
        private static string LogFile = AppSettings.Get("log.file");

        public static void LogNpsStepStart()
        {
            string testName = new StackFrame(1).GetMethod().DeclaringType.ToString();

            testName = testName.Split(new char[] { '.' })[testName.Split(new char[] { '.' }).Length-1];

            NpsStep("TEST STEP STARTED: " + testName);
        }

        public static void LogNpsStepStop()
        {
            string testName = new StackFrame(1).GetMethod().DeclaringType.ToString();

            testName = testName.Split(new char[] { '.' })[testName.Split(new char[] { '.' }).Length - 1];

            NpsStep("TEST STEP ENDED  : " + testName + " (successfully)");
        }

        public static void NpsStep(string text)
        {
            var objtype = new StackFrame(1).GetMethod().DeclaringType;

            var name = new StackFrame(1).GetMethod().Name;

            Write(LogOutputTypeEnum.NpsStep, "[" + objtype.ToString() + "] '" + text + "'");
        }

        public static void Info(string text)
        {
            Write(LogOutputTypeEnum.Info, text);
        }

        public static void Err(string text)
        {
            Write(LogOutputTypeEnum.Error, text);
        }

        public static void ErrAndFail(string text)
        {
            Err(text);

            throw new Exception(text);
        }

        public static void Debug(string text)
        {
            Write(LogOutputTypeEnum.Debug, text);
        }

        public static void Warn(string text)
        {
            Write(LogOutputTypeEnum.Warn, text);
        }

        public static void Write(LogOutputTypeEnum outputType, string text)
        {
            using (System.IO.StreamWriter fs = System.IO.File.AppendText(LogFile))
            {
                DateTime now = DateTime.Now;

                string day = now.Day.ToString().PadLeft(2, '0');
                string month = now.Month.ToString().PadLeft(2, '0');
                string year = now.Year.ToString();

                string hour = now.Hour.ToString().PadLeft(2, '0');
                string minute = now.Minute.ToString().PadLeft(2, '0');
                string second = now.Second.ToString().PadLeft(2, '0');

                string dateTimeNow =
                    day + "/" + month + "/" + year + " " + hour + ":" + minute + ":" + second;

                fs.WriteLine(outputType.ToString().ToUpper().PadRight(10,' ') + "(" + dateTimeNow + ") " + text);
                fs.Flush();
            }
        }
    }
}
