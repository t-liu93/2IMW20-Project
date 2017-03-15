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
        //TODO:
        //1. Raw data from xml
        //1.1. Authors
        //1.2. Title optional
        //1.3. Edges
        //1.3.1. Counter of edges

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

        public Dictionary<string, int> GetNodesToMap(string name)
        {
            Dictionary<string, int> tempMap = new Dictionary<string, int>();
            XmlNodeList xmlNodes = base.GetElementsByTagName(name);
            this.nodeQuantity = xmlNodes.Count;
            int vertix = 0;
            foreach(XmlNode element in xmlNodes)
            {
              
                if (! (element == null))
                {
                    //tempMap.Add(element.InnerText, vertix++);
                    //Console.WriteLine("Key: " + element.InnerText + "Value: " + vertix++);
                    if (!(tempMap.ContainsKey(element.InnerText)))
                    {
                        tempMap.Add(element.InnerText, vertix++);
                    }
                }
                //nodes.Add(element.InnerText);
                //Console.WriteLine(element.InnerText);
            }

            return tempMap;
        }


    }
}
