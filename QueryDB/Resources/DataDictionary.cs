using System.Collections.Generic;

namespace QueryDB.Resources
{
    /// <summary>
    /// Data dictionary to hold data.
    /// </summary>
    public class DataDictionary
    {
        /// <summary>
        /// Holds data values.
        /// </summary>
        public IDictionary<string, string> ReferenceData { get; private set; }

        /// <summary>
        /// Creates data dictionary to hold data.
        /// </summary>
        internal DataDictionary()
        {
            ReferenceData = new Dictionary<string, string>();
        }
    }
}
