using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.dataset
{
    /// <summary>
    /// This class stores the raw data parsed from the DBLP XML file.
    /// It create an instance of parser and get all nodes with tag author of articles
    /// It create edges when two authors co-authored the same article. 
    /// </summary>
    class RawDataDblp : RawData
    {
        private parser.Parser p;
        public RawDataDblp(string location) : base(location)
        {
            p = new parser.Parser(base.location);
            p.Load();
        }

        /// <summary>
        /// Get nodes from XML parser in dictionary form
        /// </summary>
        public override void getNodes()
        {
            base.nodes = p.GetNodesToDictionary("author");
        }

        /// <summary>
        /// Build edges
        /// In this dblp model, if more than one author co-authored an article
        /// then there is an edge between these authors.
        /// </summary>
        public void buildEdges()
        {
            System.Xml.XmlNodeList n = p.GetElementsByTagName("article");
            foreach(System.Xml.XmlElement element in n)
            {
                if (getQuantityOfTag(element, "author") > 1)
                {
                    System.Xml.XmlNodeList e = element.GetElementsByTagName("author");
                    for (int i = 0; i < e.Count; i++)
                    {
                        for (int j = i + 1; j < e.Count; j ++)
                        {
                            base.AddEdge(base.nodes[e.Item(i).InnerText], base.nodes[e.Item(j).InnerText]);
                        }
                    }
                }  
            }
        }

        /// <summary>
        /// Build dataset
        /// invoke the method to get nodes and build edges
        /// stores them in two class variables in the base class
        /// </summary>
        public override void buildDataset()
        {
            getNodes();
            buildEdges();
        }

        /// <summary>
        /// Calculate the number of child nodes in an XML element with a specified tag
        /// </summary>
        /// <param name="xmlElement">The XML element to be checked</param>
        /// <param name="tag">The specified tag</param>
        /// <returns></returns>
        private int getQuantityOfTag(System.Xml.XmlElement xmlElement, string tag)
        {
            //System.Xml.XmlNodeList n = xmlElement.GetElementsByTagName(tag);
            return xmlElement.GetElementsByTagName(tag).Count;
        }
    }


}
