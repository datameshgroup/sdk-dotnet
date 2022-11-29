using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace DataMeshGroup.Fusion.Model
{
    public class OutputContent
    {

        /// <summary>
        /// Format of the content. For XHTML the content will be in <see cref="OutputXHTML"/>, for Text the content is in <see cref="OutputText"/>
        /// </summary>
        public OutputFormat OutputFormat { get; set; }

        public PredefinedContent PredefinedContent { get; set; }
        //public OutputText OutputText { get; set; }

        /// <summary>
        /// The content in XHTML format BASE64 encoded. Only set if <see cref="OutputFormat"/> is XHTML, and null otherwise
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Base64JsonConverter))]
        public string OutputXHTML { get; set; }

        /// <summary>
        /// The content in Text format. Only set if <see cref="OutputFormat"/> is Text, and null otherwise
        /// </summary>
        public OutputText OutputText { get; set; }

        // We don't support barcode yet
        //public OutputBarcode OutputBarcode { get; set; }


        /// <summary>
        /// Returns the receipt or display content as plain text. Returns "DATAMESH INVALID FORMAT" if content format is invalid. 
        /// </summary>
        public string ContentAsPlainText
        {
            get
            {
                try
                {
                    return GetContentAsPlainText();
                }
                catch
                {
                    // Not great to supress this exception, but required as POS systems 
                    // could access this field without implementing error handling
                    return "DATAMESH INVALID FORMAT";
                }
            }
        }

        /// <summary>
        /// Returns a plain text version of the content
        /// </summary>
        /// <exception cref="XmlException">Thrown if XHTML OutputFormat and XML format is invalid</exception>
        public string GetContentAsPlainText()
        {

            switch (OutputFormat)
            {
                case OutputFormat.XHTML:
                    if (string.IsNullOrWhiteSpace(OutputXHTML))
                    {
                        return "";
                    }

                    // Parse - this will throw XmlException if the format is invalid
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml($"<html>{OutputXHTML}</html>");
                    StringBuilder receipt = new StringBuilder();
                    ParseNode(doc.DocumentElement, receipt);
                    return receipt.ToString();

                case OutputFormat.Text:
                    if (string.IsNullOrWhiteSpace(OutputText?.Text))
                    {
                        return "";
                    }

                    return OutputText?.Text ?? "";

                // We can't handle any other cases
                case OutputFormat.Barcode:
                case OutputFormat.MessageRef:
                case OutputFormat.Unknown:
                default:
                    return "";
            }
        }

        private readonly string[] newLineTags = new string[] { "p", "br", "br/", "pre" };
        private void ParseNode(XmlNode rootNode, StringBuilder receipt)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                string tag = node.Name.ToLowerInvariant();

                if (newLineTags.Contains(tag) && receipt.Length > 0)
                {
                    receipt.Append(Environment.NewLine);
                }
                else if (tag == "#text")
                {
                    string line = (rootNode.Name == "pre") ? node.InnerText : Regex.Replace(node.InnerText.Trim(), @"[\t\n\r\s]+", " ");
                    receipt.Append(line);
                }
                ParseNode(node, receipt);

                if (tag == "p" && receipt.Length > 0)
                {
                    receipt.Append(Environment.NewLine);
                }
            }
        }

    }
}
