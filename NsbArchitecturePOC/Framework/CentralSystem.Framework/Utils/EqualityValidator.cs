namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Validates equality of objects   
    /// </summary>
    public sealed class EqualityValidator
    {
        /// <summary>
        ///  Validates that two values are the same using comparer
        /// </summary>
        /// <typeparam name="T">type to compare</typeparam>
        /// <param name="firstValue">first value to compare</param>
        /// <param name="secondValue">second value to compare</param>
        /// <param name="comparer">comparer to compare object; if not specified the default T comparer is used</param>
        /// <returns>comparison result</returns>
        public bool AreEqual<T>(T firstValue, T secondValue, IEqualityComparer<T> comparer)
        {
            return comparer.Equals(firstValue, secondValue);
        }

        /// <summary>
        ///  Validates that two values are the same using comparer
        /// </summary>
        /// <typeparam name="T">type to compare</typeparam>
        /// <param name="firstValue">first value to compare</param>
        /// <param name="secondValue">second value to compare</param>
        /// <returns>comparison result</returns>
        public bool AreEqual<T>(T firstValue, T secondValue)
        {
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
            return comparer.Equals(firstValue, secondValue);
        }

        /// <summary>
        /// Validates that two collections are the same using comparer
        /// </summary>
        /// <typeparam name="T">type to compare</typeparam>
        /// <param name="firstValue">first collection to compare</param>        
        /// <param name="secondValue">second collection to compare</param>
        /// <param name="comparer">comparer to compare object; if not specified the default T comparer is used</param>
        /// <returns>comparison result</returns>
        public bool CollectionsAreEqual<T>(IEnumerable<T> firstValue, IEnumerable<T> secondValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }
            bool areEqual = firstValue.SequenceEqual(secondValue, comparer);
            return areEqual;
        }

        /// <summary>
        /// Validates that two collections are the same using comparer
        /// </summary>
        /// <typeparam name="T">type to compare</typeparam>
        /// <param name="firstValue">first collection to compare</param>        
        /// <param name="secondValue">second collection to compare</param>
        /// <returns>comparison result</returns>
        public bool CollectionsAreEqual<T>(IEnumerable<T> firstValue, IEnumerable<T> secondValue)
        {
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
            bool areEqual = firstValue.SequenceEqual(secondValue, comparer);
            return areEqual;
        }

    }
}
