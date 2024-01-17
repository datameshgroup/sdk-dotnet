using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public class StoredValueAccountID
    {
        public StoredValueAccountID() {
            StoredValueAccountType = StoredValueAccountType.GiftCard;
        }

        /// <summary>
        /// Type of stored value account
        /// </summary>
        public StoredValueAccountType StoredValueAccountType { get; set; }

        /// <summary>
        /// Identification of the provider of the stored value account load/reload
        /// </summary>
        public string StoredValueProvider {  get; set; }

        /// <summary>
        /// Owner name of an account
        /// </summary>
        public string OwnerName {  get; set; }

        /// <summary>
        /// Date after which the card cannot be used.
        /// </summary>
        public string ExpiryDate {  get; set; }

        /// <summary>
        /// Entry mode of the payment instrument information
        /// </summary>
        public EntryMode EntryMode { get; set; }

        /// <summary>
        /// Type of account identification        
        /// </summary>
        public IdentificationType IdentificationType { get; set; }

        /// <summary>
        /// Stored value account identification
        /// </summary>
        public string StoredValueID { get; set; }

    }
}
