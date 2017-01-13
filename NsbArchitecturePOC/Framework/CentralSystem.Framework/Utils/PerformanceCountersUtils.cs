namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Performance counters utilities
    /// </summary>
    public static class PerformanceCountersUtils
    {

        #region Methods

        /// <summary>
        /// Found category object
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>Object</returns>
        public static PerformanceCounterCategory GetCategoryIfExists(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException("categoryName");
            }
            if (!PerformanceCounterCategory.Exists(categoryName))
            {
                return null;
            }
            return new PerformanceCounterCategory(categoryName);
        }

        /// <summary>
        /// Remove counter instance
        /// </summary>
        /// <param name="performanceCountersCategory">Performance counter category</param>
        /// <param name="instanceName">Instance name</param>
        public static void RemovePerformanceCounterInstance(PerformanceCounterCategory performanceCountersCategory, string instanceName)
        {
            if (performanceCountersCategory == null)
            {
                throw new ArgumentNullException("performanceCountersCategory");
            }
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName");
            }

            //According to MSDN and internal test
            //remove instance by one counter will remove instances for every category
            string oneCounterNameToRemove = performanceCountersCategory.GetCounters(instanceName).First().CounterName;

            using (PerformanceCounter performanceCounter = new PerformanceCounter(performanceCountersCategory.CategoryName,
                oneCounterNameToRemove,
                instanceName,
                false))
            {
                performanceCounter.RemoveInstance();
            }
        }

        #endregion

    }
}
