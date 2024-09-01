using System.Collections.Generic;

namespace QueryDB.Resources
{
    /// <summary>
    /// Data dictionary to hold table data.
    /// </summary>
    public class DataDictionary
    {
        /// <summary>
        /// Holds table data values.
        /// </summary>
        public IDictionary<string, string> ReferenceData { get; private set; }

        /// <summary>
        /// Creates data dictionary to hold table data.
        /// </summary>
        internal DataDictionary()
        {
            ReferenceData = new Dictionary<string, string>();
        }
    }
}
