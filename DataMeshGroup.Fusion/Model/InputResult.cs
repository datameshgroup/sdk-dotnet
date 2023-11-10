using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class InputResult
    {
        /// <summary>
        /// Mirrored from request
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// Mirrored from request
        /// </summary>
        public InfoQualify InfoQualify { get; set; }

        public Response Response { get; set; }

        public Input Input { get; set; }
    }
}
