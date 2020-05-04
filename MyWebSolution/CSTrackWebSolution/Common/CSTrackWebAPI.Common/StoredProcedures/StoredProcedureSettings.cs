using System.Collections.Generic;

namespace CSTrackWebAPI.Common.StoredProcedures
{
    public class StoredProcedureSettings
    {
        public string Name { get; set; }
        public List<StoredProcedureParameter> Parameters { get; set; }
    }
}
