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

        public void buildEdges()
        {
            System.Xml.XmlNodeList n = p.GetElementsByTagName("article");
            //Console.WriteLine("test");
            //Console.ReadKey();
            //Console.WriteLine(base.location);
            //Console.WriteLine(base.nodes);
            //Console.WriteLine(n.Item(1).InnerXml);
            //foreach(XmlNode element in n)
            //{
            //    Console.WriteLine("123");
            //    Console.ReadKey();
            //}
            //foreach (KeyValuePair<string, int> kvp in base.nodes)
            //{
            //    Console.Write(kvp.Key);
            //    Console.WriteLine(kvp.Value);
            //}
            //Console.ReadKey();
            foreach(System.Xml.XmlElement element in n)
            {
                //System.Xml.XmlNodeList temp = element;
                //Console.WriteLine(element);
                if (getQuantityOfTag(element, "author") > 1)
                {
                    System.Xml.XmlNodeList e = element.GetElementsByTagName("author");
                    for (int i = 0; i < e.Count; i++)
                    {
                        for (int j = i + 1; j < e.Count; j ++)
                        {
                            Edge edge = new Edge(base.getEdgeId(base.edges) + 1,
                                base.nodes[e.Item(i).InnerText],
                                base.nodes[e.Item(j).InnerText]);
                            //Console.WriteLine(base.nodes[e.Item(i).InnerText]);
                            base.AddEdge(edge);
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Calculate the number of child nodes in an XML element with a specified tag
        /// </summary>
        /// <param name="xmlElement">The XML element to be checked</param>
        /// <param name="tag">The specified tag</param>
        /// <returns></returns>
        private int getQuantityOfTag(System.Xml.XmlElement xmlElement, string tag)
        {
            System.Xml.XmlNodeList n = xmlElement.GetElementsByTagName(tag);
            return n.Count;
        }
    }


}
