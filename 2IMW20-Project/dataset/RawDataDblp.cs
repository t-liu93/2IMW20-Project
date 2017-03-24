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
        private parser.Parser p; //New instance of an XML parser
        private int articleNumber;

        /// <summary>
        /// Constructor
        /// Invoke constructor of base cass
        /// </summary>
        /// <param name="location">xml file location in URL form</param>
        public RawDataDblp(string location) : base(location)
        {
            p = new parser.Parser(base.location);
            p.Load();
        }

        /// <summary>
        /// Get nodes from XML parser in dictionary form
        /// </summary>
        public override void BuildNodes()
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
            this.articleNumber = n.Count;
            foreach(System.Xml.XmlElement element in n)
            {
                if (GetQuantityOfTag(element, "author") > 1)
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
        public override void BuildDataset()
        {
            BuildNodes();
            buildEdges();
        }

        /// <summary>
        /// Calculate the number of child nodes in an XML element with a specified tag
        /// </summary>
        /// <param name="xmlElement">The XML element to be checked</param>
        /// <param name="tag">The specified tag</param>
        /// <returns></returns>
        private int GetQuantityOfTag(System.Xml.XmlElement xmlElement, string tag)
        {
            return xmlElement.GetElementsByTagName(tag).Count;
        }

        /// <summary>
        /// Return the article number in the dataset
        /// </summary>
        /// <returns>An integer contains the article number</returns>
        public int GetArticleNumber()
        {
            return this.articleNumber;
        }
    }


}
