using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.Util
{
    public static class Debug
    {       
        static uint call;
        static uint callCounter
        {
            get
            {
                if (call == uint.MaxValue)
                    call = 0;
                return call++;
            }
        }

        public static void LogError(string v)
        {
            UnityEngine.Debug.LogError($"{callCounter} : {v}");
        }

        public static void Log(string format)
        {
            UnityEngine.Debug.Log($"{callCounter} : {format}");
        }
    }
}