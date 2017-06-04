﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.Extensions
{
    public static class ListExtensions
    {
        public static T[,] To2DArray<T>(this List<List<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int max = source.Select(l => l).Max(l => l.Count());

            var result = new T[source.Count, max];

            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source[i].Count(); j++)
                {
                    result[i, j] = source[i][j];
                }
            }

            return result;
        }
    }
}