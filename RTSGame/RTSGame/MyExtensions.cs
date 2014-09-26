﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTSGame
{
    public static class MyExtensions
    {
        static readonly Random random = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

        }
    }
}