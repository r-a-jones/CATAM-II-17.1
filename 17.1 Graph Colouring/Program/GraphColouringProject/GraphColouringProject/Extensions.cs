using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColouringProject
{
    public static class Extensions
    {
        /// <summary>
        /// Removes all tuples with equal entries from the list. E.g. [(1,1),(1,2),(2,3)] -> [(1,2),(2,3)]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static (T, T)[] RemoveAllTuplesWithEqualEntries<T>(this (T, T)[] tuples)
        {
            List<(T, T)> newTuples = new List<(T, T)>();
            foreach (var tuple in tuples) {
                if (tuple.Item1.Equals(tuple.Item2) == false)
                {
                    newTuples.Add(tuple);
                }
            }
            return newTuples.ToArray();
        }
        /// <summary>
        /// Ordered in ascending order.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static (int, int) Ordered(this (int, int) tuple)
        {
            if (tuple.Item1 >= tuple.Item2)
            {
                return (tuple.Item2, tuple.Item1);
            }
            else
            {
                return tuple;
            }
        }

        public static int Mod(this int n, int modulus)
        {
            int newN = n;

            while (newN < 0)
            {
                newN += modulus;
            }

            return newN % modulus;
        }

        public static string ToFormattedString<T>(this T[] arr)
        {

            if (arr.Length == 0)
            {
                return "[]";
            }

            string result = "[";

            foreach (T item in arr)
            {
                result += item;
                result += ", ";
            }

            result = result.Substring(0, result.Length - 2);
            result += "]";

            return result;
        }
    }
}
