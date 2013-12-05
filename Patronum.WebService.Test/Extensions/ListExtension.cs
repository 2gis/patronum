using System;
using System.Collections.Generic;

namespace Patronum.WebService.Test.Extensions
{
    public static class ListExtension
    {
        public static T Random<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("List cannot be empty");
            }

            T random;

            if (list.Count == 1)
            {
                random = list[0];
            }
            else
            {
                var randomValue = new Random();
                int r = randomValue.Next(list.Count);
                random = list[r];
            }

            return random;
        }
    }
}
