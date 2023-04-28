using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    internal static class LevenshteinDistance
    {
        public static int Between(string synonym1, string synonym2)
        {
            int[,] distance = new int[synonym1.Length + 1, synonym2.Length + 1];

            for (int i = 0; i <= synonym1.Length; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= synonym2.Length; j++)
            {
                distance[0, j] = j;
            }

            for (int j = 1; j <= synonym2.Length; j++)
            {
                for (int i = 1; i <= synonym1.Length; i++)
                {
                    int cost = (synonym1[i - 1] == synonym2[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(Math.Min(
                                distance[i - 1, j] + 1,
                                distance[i, j - 1] + 1),
                                distance[i - 1, j - 1] + cost);
                }
            }

            return distance[synonym1.Length, synonym2.Length];
        }
    }
}
