using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2IMW20_Project.parser
{
    class Parser : XmlDocument
    {
        private string location;
        private int nodeQuantity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">the file location in URL</param>
        public Parser(string location) : base()
        {
            this.location = location;
        }

        /// <summary>
        /// Load the xml file using URL stored in this instance
        /// </summary>
        public void Load()
        {
            base.Load(this.location);
        }

        /// <summary>
        /// Get nodes by tags and stored in a dictionary
        /// The dictionary uses the nodes.innertext as key and an increasing integer as value
        /// </summary>
        /// <param name="name">The tag in string that needed to be stored</param>
        /// <returns>A dictionary contains nodes with key value string and integer value ID</returns>
        public Dictionary<string, int> GetNodesToDictionary(string name)
        {
            Dictionary<string, int> tempMap = new Dictionary<string, int>();
            XmlNodeList xmlNodes = base.GetElementsByTagName(name);
            this.nodeQuantity = xmlNodes.Count;
            int vertix = 0;
            foreach(XmlNode element in xmlNodes)
            {
              
                if (! (element == null))
                {
                    if (!(tempMap.ContainsKey(element.InnerText)))
                    {
                        tempMap.Add(element.InnerText, vertix++);
                    }
                }
            }
            return tempMap;
        }

    }
}
