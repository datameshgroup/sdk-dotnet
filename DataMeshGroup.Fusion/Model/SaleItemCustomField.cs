using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    /// <summary>
    /// Wrapper for a custom SaleItem field. 
    /// </summary>
    public class SaleItemCustomField
    {
        
        public SaleItemCustomField(string key, string value)
        {
            Key = key;
            Type = SaleItemCustomFieldType.String;
            Value = value;
        }

        /// <summary>
        /// Key of the field. Required.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Defines the type of the content in 'Value'. In most instances 
        /// this should be string, but other types are supported
        /// </summary>
        public SaleItemCustomFieldType Type { get; set; }


        /// <summary>
        /// Value of the custom field. DataType is defined by the 'Type' field
        /// </summary>
        public string Value { get; set; }
    }
}
