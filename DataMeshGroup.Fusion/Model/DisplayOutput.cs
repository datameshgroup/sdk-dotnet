using System;

namespace DataMeshGroup.Fusion.Model
{
    public class DisplayOutput
    {
        /// <summary>
        /// When a message response is optional, for instance for display message, it allows to request to send not to sent a message response to the display.
        /// </summary>
        public bool? ResponseRequiredFlag { get; set; }
        
        /// <summary>
        /// If present, and >0 the display must be displayed for at least this number of seconds
        /// </summary>
        public int? MinimumDisplayTime { get; set; }

        public Device Device { get; set; }

        public InfoQualify InfoQualify { get; set; }

        public OutputContent OutputContent { get; set; }
        
        //public MenuEntry MenuEntry { get; set; }
        //public string OutputSignature { get; set; }
    }
}
