using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2IMW20_Project
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

        public Dictionary<int, string> GetNodesToMap(string name)
        {
            Dictionary<int, string> tempMap = new Dictionary<int, string>();
            XmlNodeList xmlNodes = base.GetElementsByTagName(name);
            this.nodeQuantity = xmlNodes.Count;
            int counter = 0;
            foreach(XmlNode element in xmlNodes)
            {
              
                if (! (element == null))
                {
                    tempMap.Add(counter ++, element.InnerText);
                }
                //nodes.Add(element.InnerText);
                //Console.WriteLine(element.InnerText);
            }

            return tempMap;
        }
    }
}
