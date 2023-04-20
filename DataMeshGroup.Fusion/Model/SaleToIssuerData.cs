namespace DataMeshGroup.Fusion.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Sale information intended for the Issuer.
    /// </summary>
    public class SaleToIssuerData
    {
        /// <summary>
        /// Label to print on the bank statement. Reference a transaction on the bank statement
        /// </summary>
        public string StatementReference { get; set; }
    }
}
