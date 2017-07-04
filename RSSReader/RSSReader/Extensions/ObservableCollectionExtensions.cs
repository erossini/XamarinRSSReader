using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Extensions
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Removes the specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll">The coll.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>System.Int32.</returns>
        public static int Remove<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }

        /// <summary>
        /// Extends ObservableCollection adding a RemoveAll method to remove elements based on a boolean condition function
        /// </summary>
        /// <typeparam name="T">The type contained by the collection</typeparam>
        /// <param name="observableCollection">The ObservableCollection</param>
        /// <param name="condition">A function that evaluates to true for elements that should be removed</param>
        /// <returns>The number of elements removed</returns>
        public static int RemoveAll<T>(this ObservableCollection<T> observableCollection, Func<T, bool> condition)
        {
            // Find all elements satisfying the condition, i.e. that will be removed
            var toRemove = observableCollection
                .Where(condition)
                .ToList();

            // Remove the elements from the original collection, using the Count method to iterate through the list, 
            // incrementing the count whenever there's a successful removal
            return toRemove.Count(observableCollection.Remove);
        }

        public static int RemoveAll<T>(this ObservableCollection<T> observableCollection)
        {
            if (observableCollection != null)
            {
                // Find all elements satisfying the condition, i.e. that will be removed
                var toRemove = observableCollection
                    .ToList();

                // Remove the elements from the original collection, using the Count method to iterate through the list, 
                // incrementing the count whenever there's a successful removal
                return toRemove.Count(observableCollection.Remove);
            }
            else
                return -1;
        }
    }
}
