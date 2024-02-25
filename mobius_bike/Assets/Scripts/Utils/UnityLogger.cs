using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSisters.Logger
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
    }
}
