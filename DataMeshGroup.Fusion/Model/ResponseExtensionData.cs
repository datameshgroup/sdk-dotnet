﻿using DataMeshGroup.Fusion.Model.Transit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Extra data outside the standard payments API
    /// </summary>
    public class ResponseExtensionData
    {
        public POIInformation POIInformation { get; set; }
    }
}
