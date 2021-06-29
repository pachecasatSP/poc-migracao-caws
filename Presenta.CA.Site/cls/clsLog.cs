using System;
using System.Diagnostics;
using System.Reflection;

namespace Controle_Acesso
{
    public static class clsLog
    {

        // function to display its name
        private static string WhatsMyName()
        {
            StackFrame stackFrame = new StackFrame();
            MethodBase methodBase = stackFrame.GetMethod();
            //Console.WriteLine(methodBase.Name); // Displays “WhatsmyName”
            //WhoCalledMe();
            return methodBase.Name;
        }
        // Function to display parent function
        public static string WhoCalledMe()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            // Displays “WhatsmyName”
            //Console.WriteLine(" Parent Method Name {0} ", methodBase.Name);
            return methodBase.Name;
        }


    }
}