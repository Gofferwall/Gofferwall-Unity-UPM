using System;

namespace Gofferwall.Model
{
    /// <summary>
    /// failure information for showing ad
    /// </summary>
    public class ShowFailure : EventArgs
    {
        /// <summary>
        /// unit id of ad which is failed to be shown
        /// </summary>
        public string UnitId { get; private set; }

        /// <summary>
        /// Gofferwall error for show failure
        /// </summary>
        public GofferwallError Error { get; private set; }

        /// <summary>
        /// constructor for load failure
        /// </summary>
        /// <param name="unitId">unit id of ad which is failed to be shown</param>
        /// <param name="error">Gofferwall error</param>
        public ShowFailure(string unitId, GofferwallError error)
        {
            this.UnitId = unitId;
            this.Error = error;
        }

        public override string ToString()
        {
            return
                "ShowFailure{" +
                "UnitId=\"" + this.UnitId + "\"" +
                ", Error=\"" + this.Error + "\"" +
                "}";
        }
    }
}
