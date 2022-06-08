using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Randoms;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class ArrayUtils
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;  
            while (count > 1) 
            {  
                count--;
                var k = EERandomUtils.RandomPositiveInt(count);
                var value = list[k];  
                list[k] = list[count];
                list[count] = value;  
            }  
        }
        
        public static T GetRandom<T>(this IList<T> list)
        {
            return list[EERandomUtils.RandomPositiveInt(list.Count)];
        }

        public static T GetLooped<T>(this IList<T> list, int i)
        {
            i %= list.Count;
            if (i < 0) i = list.Count + i;
            return list[i];
        }
    }
}
