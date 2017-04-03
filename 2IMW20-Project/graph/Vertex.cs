﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project
{
    class Vertex
    {
        public int id;
        public Dictionary<int, float> neighbours;

        public int vertexDegree;
        public float triangleDegree;

        public float expectedVertexDegree;
        public float expectedTriangleDegree;

        public Vertex(int id)
        {
            this.id = id;

            this.vertexDegree = 0;
            this.triangleDegree = 0;

            this.expectedVertexDegree = 0f;
            this.expectedTriangleDegree = 0f;

            this.neighbours = new Dictionary<int, float>();
        }

        public float GetVertexDiscrepancy()
        {
            return this.vertexDegree - this.expectedVertexDegree;
        }

        public float GetTriangleDiscrepancy()
        {
            return this.triangleDegree - this.expectedTriangleDegree;
        }

        public void AddEdgeToVertex(int vertexId, float probability)
        {
            neighbours.Add(vertexId, probability);
        }

        public void RemoveEdge(int vertexId)
        {
            neighbours.Remove(vertexId);
        }
    }
}
