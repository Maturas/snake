using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Snake
{
    public static class Utils
    {
        /// <summary>
        ///     Picks n random elements from the source collection
        /// </summary>
        /// <param name="source"></param>
        /// <param name="n"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int n)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sourceList = source.ToList();
            if (n > sourceList.Count)
            {
                throw new ArgumentException("n is greater than the number of elements in the source collection");
            }

            return sourceList.OrderBy(x => Random.Range(0, sourceList.Count)).Take(n);
        }

        /// <summary>
        ///     Picks n random elements from the source collection with duplicates, taking into account the weight of each element
        /// </summary>
        /// <param name="source"></param>
        /// <param name="weightSelector"></param>
        /// <param name="n"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> PickRandomWeightedWithDuplicates<T>(this IEnumerable<T> source,
            Func<T, float> weightSelector, int n)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (weightSelector == null)
            {
                throw new ArgumentNullException(nameof(weightSelector));
            }

            var sourceList = source.ToList();
            var weightList = sourceList.Select(weightSelector).ToList();

            if (sourceList.Count == 0)
            {
                throw new ArgumentException("The source collection is empty");
            }

            var selectedItems = new List<T>();
            var totalWeight = weightList.Sum();

            for (var i = 0; i < n; i++)
            {
                var randomValue = Random.Range(0, totalWeight);
                var cumulativeWeight = 0.0f;

                for (var j = 0; j < sourceList.Count; j++)
                {
                    cumulativeWeight += weightList[j];
                    if (randomValue < cumulativeWeight)
                    {
                        selectedItems.Add(sourceList[j]);
                        break;
                    }
                }
            }

            return selectedItems;
        }
    }
}