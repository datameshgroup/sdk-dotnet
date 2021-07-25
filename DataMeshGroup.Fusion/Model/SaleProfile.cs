using System.Collections.Generic;

namespace DataMeshGroup.Fusion.Model
{
    public class SaleProfile
    {
        public GenericProfile GenericProfile { get; set; } = GenericProfile.Basic;
        public List<ServiceProfile> ServiceProfiles { get; private set; } = new List<ServiceProfile>();
    }
}
