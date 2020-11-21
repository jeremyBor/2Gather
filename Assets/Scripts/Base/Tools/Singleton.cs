﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Tools
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance = null;
    
        protected Singleton() { }
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}