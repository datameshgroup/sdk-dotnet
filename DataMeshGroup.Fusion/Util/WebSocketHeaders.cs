using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Wrapper for headers used in WebSocket communication
    /// </summary>
    public class WebSocketHeaders
    {
        public string UserAgent => GetUserAgent();
        public string SaleID { get; set; }
        public string POIID { get; set; }
        public string CertificationCode { get; set; }
        public string InstanceID { get; internal set; }

        private static string GetUserAgent()
        {
            string baseName = "DataMesh.Fusion";
            try
            {
                object[] customAttributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(true);
                string assemblyVersion = customAttributes.OfType<System.Reflection.AssemblyFileVersionAttribute>().FirstOrDefault()?.Version ?? "UNKNOWN";
                var framework = customAttributes.OfType<System.Runtime.Versioning.TargetFrameworkAttribute>().FirstOrDefault()?.FrameworkDisplayName ?? "UNKNOWN";
                string osVersionString = System.Environment.OSVersion.ToString();
                string osArchitecture = Environment.Is64BitOperatingSystem ? "x64" : "x86";
                string appArchitecture = Environment.Is64BitProcess ? "Win64" : "Win32";
                return $"{baseName}/{assemblyVersion}({framework}; {osVersionString}; {appArchitecture}; {osArchitecture})";
            }
            catch
            {
                return baseName;  // Suppress errors here to prevent introduced crashes
            }
        }

    }

}
