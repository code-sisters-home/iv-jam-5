using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSisters.Utils
{
    public class UnityLogger : MonoBehaviour
    {
        public static void Log(string message)
        {
            if (Application.isEditor)
                Debug.Log(message);
            else
                Console.WriteLine(message);
        }

        public static void LogError(string message)
        {
            if (Application.isEditor)
                Debug.LogError($"<color=red>{message}</color>");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
