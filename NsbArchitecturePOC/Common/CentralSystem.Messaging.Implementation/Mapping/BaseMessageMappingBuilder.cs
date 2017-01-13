namespace CentralSystem.Messaging.Implementation.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonMessageContracts = CentralSystem.Messaging;
    using CommonValueObjects = CentralSystem.DataObjects.ValueObjects;
    using CentralSystem.Messaging.Utils;

    /// <summary>
    /// Base message mapper
    /// </summary>
    public sealed class BaseMessageMappingBuilder
    {

        #region Methods

        /// <summary>
        /// Mapping from Message to Value object
        /// </summary>
        /// <param name="source">Message object</param>
        /// <param name="destination">Value object</param>
        public void ExecuteMappingHeaders(IBaseMessage source, CommonValueObjects.BaseRequest destination)
        {
            destination.BrandID = source.BrandID;
            destination.APIVersion = source.APIVersion;
            destination.OriginatorSystemID = source.OriginatorSystemID;
            destination.BTMID = source.BTMID;
            destination.InitiatingAccountID = source.InitiatingAccountID;
            destination.InitiatingApplication = source.InitiatingApplication;
            destination.RequesterID = source.RequesterID;
            destination.RequesterType = source.RequesterType;
            destination.RequestID = source.RequestID;
            destination.RequestSystemSubType = source.RequestSystemSubType;
            destination.RequestSystemType = source.RequestSystemType;
            destination.SubSystemID = source.SubSystemID;
        }

        /// <summary>
        /// Mapping from Value to Message object
        /// </summary>
        /// <param name="source">Value object</param>
        /// <param name="destination">Message object</param>
        public void ExecuteMappingHeaders(CommonValueObjects.BaseRequest source, IBaseMessage destination)
        {
            ApplyDefaultHeadersInOutcomingMessage(destination);

            destination.BrandID = source.BrandID;
            destination.OriginatorSystemID = source.OriginatorSystemID;
            destination.BTMID = source.BTMID;
            destination.RequesterID = source.RequesterID;
            destination.RequesterType = source.RequesterType;
            destination.RequestID = source.RequestID;
            destination.RequestSystemSubType = source.RequestSystemSubType;
            destination.RequestSystemType = source.RequestSystemType;
            destination.SubSystemID = source.SubSystemID;
        }

        /// <summary>
        /// Mapping from Message to Message object
        /// </summary>
        /// <param name="source">source Message object</param>
        /// <param name="destination">destination Message object</param>
        public void ExecuteMappingHeaders(IBaseMessage source, IBaseMessage destination)
        {
            ApplyDefaultHeadersInOutcomingMessage(destination);

            destination.BrandID = source.BrandID;
            destination.OriginatorSystemID = source.OriginatorSystemID;
            destination.BTMID = source.BTMID;
            destination.RequesterID = source.RequesterID;
            destination.RequesterType = source.RequesterType;
            destination.RequestID = source.RequestID;
            destination.RequestSystemSubType = source.RequestSystemSubType;
            destination.RequestSystemType = source.RequestSystemType;
            destination.SubSystemID = source.SubSystemID;
        }

        /// <summary>
        /// Apply default headers in out coming message
        /// </summary>
        /// <param name="destination">Destination</param>
        public void ApplyDefaultHeadersInOutcomingMessage(Messaging.IBaseMessage destination)
        {
            new BusApiVersionDetector().Apply(destination);
            destination.InitiatingApplication = "TestApp";
            destination.InitiatingAccountID = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        #endregion

    }
}
