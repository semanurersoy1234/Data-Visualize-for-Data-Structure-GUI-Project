using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ce205_hw2_algorithms_cs;
using static ce205_hw2_algorithms_cs.Class1;
using System.Collections.Generic;
using System.Text;

namespace ce205_hw2_test_cs
{
    [TestClass]
    public class UnitTest1
    {
        Class1 class1 = new Class1();

        [TestMethod]
        public void MST_Test1()
        {
            /* Let us create following weighted graph
                    10
                0--------1
                | \ |
            6| 5\ |15
                | \ |
                2--------3
                    4 */
            int V = 4; // Number of vertices in graph
            int E = 5; // Number of edges in graph

            Graph2 graph = new Graph2(V, E);

            // add edge 0-1
            graph.edge[0].src = 0;
            graph.edge[0].dest = 1;
            graph.edge[0].weight = 10;

            // add edge 0-2
            graph.edge[1].src = 0;
            graph.edge[1].dest = 2;
            graph.edge[1].weight = 6;

            // add edge 0-3
            graph.edge[2].src = 0;
            graph.edge[2].dest = 3;
            graph.edge[2].weight = 5;

            // add edge 1-3
            graph.edge[3].src = 1;
            graph.edge[3].dest = 3;
            graph.edge[3].weight = 15;

            // add edge 2-3
            graph.edge[4].src = 2;
            graph.edge[4].dest = 3;
            graph.edge[4].weight = 4;

            string result = graph.MinimumSpanningTree_Kruskal();

            string expected = "(source --> destination : weight)\r\n" +
                              "(2 --> 3 : 4)\r\n" +
                              "(0 --> 3 : 5)\r\n" +
                              "(0 --> 1 : 10)\r\n";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void MST_Test2()
        {

            int V = 6; // Number of vertices in graph
            int E = 8; // Number of edges in graph

            Graph2 graph = new Graph2(V, E);

            // add edge 0-1
            graph.edge[0].src = 0;
            graph.edge[0].dest = 1;
            graph.edge[0].weight = 4;

            // add edge 0-2
            graph.edge[1].src = 0;
            graph.edge[1].dest = 2;
            graph.edge[1].weight = 4;

            // add edge 1-2
            graph.edge[2].src = 1;
            graph.edge[2].dest = 2;
            graph.edge[2].weight = 2;

            // add edge 2-3
            graph.edge[3].src = 2;
            graph.edge[3].dest = 3;
            graph.edge[3].weight = 3;

            // add edge 2-5
            graph.edge[4].src = 2;
            graph.edge[4].dest = 5;
            graph.edge[4].weight = 2;

            // add edge 2-4
            graph.edge[5].src = 2;
            graph.edge[5].dest = 4;
            graph.edge[5].weight = 4;

            // add edge 3-4
            graph.edge[6].src = 3;
            graph.edge[6].dest = 4;
            graph.edge[6].weight = 3;

            // add edge 5-4
            graph.edge[7].src = 5;
            graph.edge[7].dest = 4;
            graph.edge[7].weight = 3;

            string result = graph.MinimumSpanningTree_Kruskal();

            string expected = "(source --> destination : weight)\r\n" +
                              "(1 --> 2 : 2)\r\n" +
                              "(2 --> 5 : 2)\r\n" +
                              "(2 --> 3 : 3)\r\n" +
                              "(3 --> 4 : 3)\r\n" +
                              "(0 --> 1 : 4)\r\n";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void MST_Test3()
        {
            /* Let us create following weighted graph
                    8
                0--------1
                | \ |
            7| 3\ |12
                | \ |
                2--------3
                    5 */
            int V = 4; // Number of vertices in graph
            int E = 5; // Number of edges in graph

            Graph2 graph = new Graph2(V, E);

            // add edge 0-1
            graph.edge[0].src = 0;
            graph.edge[0].dest = 1;
            graph.edge[0].weight = 8;

            // add edge 0-2
            graph.edge[1].src = 0;
            graph.edge[1].dest = 2;
            graph.edge[1].weight = 7;

            // add edge 0-3
            graph.edge[2].src = 0;
            graph.edge[2].dest = 3;
            graph.edge[2].weight = 3;

            // add edge 1-3
            graph.edge[3].src = 1;
            graph.edge[3].dest = 3;
            graph.edge[3].weight = 12;

            // add edge 2-3   
            graph.edge[4].src = 2;
            graph.edge[4].dest = 3;
            graph.edge[4].weight = 5;

            string result = graph.MinimumSpanningTree_Kruskal();

            string expected = "(source --> destination : weight)\r\n" +
                              "(0 --> 3 : 3)\r\n" +
                              "(2 --> 3 : 5)\r\n" +
                              "(0 --> 1 : 8)\r\n";
            Assert.AreEqual(result, expected);
        }


        [TestMethod]
        public void BFS_Test1()
        {
            class1.GraphBFS(4);

            class1.AddEdgeBFS(0, 1);
            class1.AddEdgeBFS(0, 2);
            class1.AddEdgeBFS(1, 2);
            class1.AddEdgeBFS(2, 0);
            class1.AddEdgeBFS(2, 3);
            class1.AddEdgeBFS(3, 3);

            string result = class1.BFS(2); //starting from vertex 2
            string expected = "2, 0, 3, 1";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void BFS_Test2()
        {
            class1.GraphBFS(10);

            class1.AddEdgeBFS(0, 1);
            class1.AddEdgeBFS(0, 2);
            class1.AddEdgeBFS(0, 3);
            class1.AddEdgeBFS(1, 3);
            class1.AddEdgeBFS(2, 4);
            class1.AddEdgeBFS(3, 5);
            class1.AddEdgeBFS(3, 6);
            class1.AddEdgeBFS(4, 7);
            class1.AddEdgeBFS(4, 5);
            class1.AddEdgeBFS(5, 2);
            class1.AddEdgeBFS(6, 5);
            class1.AddEdgeBFS(7, 5);
            class1.AddEdgeBFS(7, 8);

            string result = class1.BFS(2); //starting from vertex 2
            string expected = "2, 4, 7, 5, 8";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void BFS_Test3()
        {
            class1.GraphBFS(6);

            class1.AddEdgeBFS(0, 1);
            class1.AddEdgeBFS(0, 3);
            class1.AddEdgeBFS(0, 4);
            class1.AddEdgeBFS(4, 5);
            class1.AddEdgeBFS(3, 5);
            class1.AddEdgeBFS(1, 2);
            class1.AddEdgeBFS(1, 0);
            class1.AddEdgeBFS(2, 1);
            class1.AddEdgeBFS(4, 1);
            class1.AddEdgeBFS(3, 1);
            class1.AddEdgeBFS(5, 4);
            class1.AddEdgeBFS(5, 3);

            string result = class1.BFS(0); //starting from vertex 0
            string expected = "0, 1, 3, 4, 2, 5";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void DFS_Test1()
        {
            class1.GraphDFS(4);

            class1.AddEdgeDFS(0, 1);
            class1.AddEdgeDFS(0, 2);
            class1.AddEdgeDFS(1, 2);
            class1.AddEdgeDFS(2, 0);
            class1.AddEdgeDFS(2, 3);
            class1.AddEdgeDFS(3, 3);
            class1.dfs = "";
            string result = class1.DFS(2); //starting from vertex 2
            string expected = "2, 0, 1, 3";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void DFS_Test2()
        {
            class1.GraphDFS(4);

            class1.AddEdgeDFS(0, 1);
            class1.AddEdgeDFS(0, 2);
            class1.AddEdgeDFS(1, 2);
            class1.AddEdgeDFS(2, 0);
            class1.AddEdgeDFS(2, 3);
            class1.AddEdgeDFS(3, 3);
            class1.dfs = "";
            string result = class1.DFS(0); //starting from vertex 0
            string expected = "0, 1, 2, 3";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void DFS_Test3()
        {
            class1.GraphDFS(4);

            class1.AddEdgeDFS(0, 1);
            class1.AddEdgeDFS(0, 2);
            class1.AddEdgeDFS(1, 2);
            class1.AddEdgeDFS(2, 3);
            class1.dfs = "";
            string result = class1.DFS(2); //starting from vertex 2
            string expected = "2, 3";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SCC_Test1()
        {
            Graph g = new Graph(5);

            g.AddEdge(1, 0);
            g.AddEdge(0, 2);
            g.AddEdge(2, 1);
            g.AddEdge(0, 3);
            g.AddEdge(3, 4);

            Graph.SCC = "";
            string result = g.Strongly_Connected_Components();
            string expected = "0, 1, 2, " + //first group
                              "\r\n3, " + //second group
                              "\r\n4"; //third group
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SCC_Test2()
        {
            Graph g = new Graph(8);

            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 0);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);
            g.AddEdge(6, 4);
            g.AddEdge(6, 7);

            Graph.SCC = "";
            string result = g.Strongly_Connected_Components();
            string expected = "0, 3, 2, 1, " + //first group
                              "\r\n4, 6, 5, " + //second group
                              "\r\n7"; //third group
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SCC_Test3()
        {
            Graph g = new Graph(5);

            g.AddEdge(2, 1);
            g.AddEdge(3, 2);
            g.AddEdge(1, 0);
            g.AddEdge(0, 3);
            g.AddEdge(3, 1);

            Graph.SCC = "";
            string result = g.Strongly_Connected_Components();
            string expected = "4, " + //first group
                              "\r\n0, 1, 2, 3"; //second group
            Assert.AreEqual(result, expected);
        }
    }
}