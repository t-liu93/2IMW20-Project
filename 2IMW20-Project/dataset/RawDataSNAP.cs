using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.dataset
{
    class RawDataSNAP : RawData
    {
        private parser.SNAPParser snap; //SNAP parser

        /// <summary>
        /// Constructor, calls constructor of base class and then parse txt file
        /// </summary>
        /// <param name="location">SNAP text database location</param>
        public RawDataSNAP(string location) : base(location)
        {
            this.snap = new parser.SNAPParser(location);
            snap.ReadFile();
        }

        /// <summary>
        /// Build nodes
        /// </summary>
        public override void BuildNodes()
        {
            foreach(int element in snap.GetNodes())
            {
                base.AddNode(element.ToString(), element);
            }
        }


        /// <summary>
        /// Build edges
        /// </summary>
        public void BuildEdges()
        {
            foreach(Edge element in snap.GetEdges())
            {
                //Console.WriteLine(element.u);
                base.AddEdge(element.u, element.v);
            }
        }

        /// <summary>
        /// Build dataset
        /// </summary>
        public override void BuildDataset()
        {
            this.BuildNodes();
            this.BuildEdges();
        }
    }
}
