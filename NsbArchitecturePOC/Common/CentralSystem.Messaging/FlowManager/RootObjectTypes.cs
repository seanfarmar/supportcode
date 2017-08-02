namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Root object types.
    /// </summary>
    /// <remarks>
    /// Current settings duplicated in messaging layer. The reason - message contract is independent component.
    /// The codes should be equals to settings in the class CentralSystem.FlowManager.DataObjects.DOs.RootObjectTypes.
    /// </remarks>
    public static class RootObjectTypes
    {

        /// <summary>
        /// Supported root object types
        /// </summary>
        public static IEnumerable<string> SupportedTypes
        {
            get
            {
                return new string[]
                {
                    Draw,
                    CDC,
                };
            }
        }

        /// <summary>
        /// Draw
        /// </summary>
        public const string Draw = "D";

        /// <summary>
        /// EOD
        /// </summary>
        public const string CDC = "E";
    }
}
