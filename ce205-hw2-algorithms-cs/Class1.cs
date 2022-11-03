/**
* @file ce205_hw2_cs
* @author Semanur Ersoy
* @date 3 November 2022
*
* @brief <b> HW-2 Functions </b>
*
* HW-2 Sample Lib Functions
*
* @see http://bilgisayar.mmf.erdogan.edu.tr/en/
*
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce205_hw2_algorithms_cs
{
    public class Graph2
    {
        // A class to represent a graph edge
        public class Edge : IComparable<Edge>
        {
            public int src, dest, weight;

            // Comparator function used for sorting edges
            // based on their weight
            public int CompareTo(Edge compareEdge)
            {
                return this.weight - compareEdge.weight;
            }
        }

        // A class to represent
        // a subset for union-find
        public class subset
        {
            public int parent, rank;
        };

        public int V, E; // V-> no. of vertices & E->no.of edges
        public Edge[] edge; // collection of all edges

        /**
        * @name Graph2
        * @param [in] fiv [\b int]
        * @param [in] fie [\b int]
        * Creates a graph with V vertices and E edges
        **/
        public Graph2(int fiv, int fie)
        {
            V = fiv;
            E = fie;
            edge = new Edge[E];
            for (int i = 0; i < fie; ++i)
                edge[i] = new Edge();
        }

        /**
        * @name find
        * @param [in] fiSubsets [\b subset[]]
        * @param [in] fii [\b int]
        * @retval [\b int]
        * A utility function to find set of an element i  
        * (uses path compression technique)
        **/
        public int find(subset[] fiSubsets, int fii)
        {
            // find root and make root as
            // parent of i (path compression)
            if (fiSubsets[fii].parent != fii)
                fiSubsets[fii].parent = find(fiSubsets, fiSubsets[fii].parent);
            return fiSubsets[fii].parent;
        }

        /**
        * @name Union
        * @param [in] fiSubsets [\b subset[]]
        * @param [in] fix [\b int]
        * @param [in] fiy [\b int]
        * A function that does union of two sets of x and y (uses union by rank)
        **/
        public void Union(subset[] fiSubsets, int fix, int fiy)
        {
            int xroot = find(fiSubsets, fix);
            int yroot = find(fiSubsets, fiy);

            // Attach smaller rank tree under root of
            // high rank tree (Union by Rank)
            if (fiSubsets[xroot].rank < fiSubsets[yroot].rank)
                fiSubsets[xroot].parent = yroot;
            else if (fiSubsets[xroot].rank > fiSubsets[yroot].rank)
                fiSubsets[yroot].parent = xroot;

            // If ranks are same, then make one as root
            // and increment its rank by one
            else
            {
                fiSubsets[yroot].parent = xroot;
                fiSubsets[xroot].rank++;
            }
        }

        /**
        * @name MinimumSpanningTree_Kruskal
        * @retval [\b string]
        * The main function to construct MST using Kruskal's algorithm
        * A minimum spanning tree (MST) or minimum weight spanning tree for a weighted, connected, undirected graph is a spanning tree with a weight less than or equal to the weight of every other spanning tree.
        * The weight of a spanning tree is the sum of weights given to each edge of the spanning tree.
        * A minimum spanning tree has (V – 1) edges where V is the number of vertices in the given graph. 
        * Below are the steps for finding MST using Kruskal’s algorithm
        * 1-Sort all the edges in non-decreasing order of their weight. 
        * 2-Pick the smallest edge. Check if it forms a cycle with the spanning tree formed so far. If cycle is not formed, include this edge. Else, discard it. 
        * 3-Repeat step#2 until there are (V-1) edges in the spanning tree.
        **/
        public string MinimumSpanningTree_Kruskal()
        {
            string mst = "";
            // This will store the
            // resultant MST
            Edge[] result = new Edge[V];
            int e = 0; // An index variable, used for result[]
            int i
                = 0; // An index variable, used for sorted edges
            for (i = 0; i < V; ++i)
                result[i] = new Edge();

            // Step 1: Sort all the edges in non-decreasing
            // order of their weight. If we are not allowed
            // to change the given graph, we can create
            // a copy of array of edges
            Array.Sort(edge);

            // Allocate memory for creating V subsets
            subset[] subsets = new subset[V];
            for (i = 0; i < V; ++i)
                subsets[i] = new subset();

            // Create V subsets with single elements
            for (int v = 0; v < V; ++v)
            {
                subsets[v].parent = v;
                subsets[v].rank = 0;
            }

            i = 0; // Index used to pick next edge

            // Number of edges to be taken is equal to V-1
            while (e < V - 1)
            {
                // Step 2: Pick the smallest edge. And increment
                // the index for next iteration
                Edge next_edge = new Edge();
                next_edge = edge[i++];

                int x = find(subsets, next_edge.src);
                int y = find(subsets, next_edge.dest);

                // If including this edge doesn't cause cycle,
                // include it in result and increment the index
                // of result for next edge
                if (x != y)
                {
                    result[e++] = next_edge;
                    Union(subsets, x, y);
                }
                // Else discard the next_edge
            }

            mst += "(source --> destination : weight)\r\n";

            for (i = 0; i < e; ++i)
            {
                mst += "(" + result[i].src + " --> " + result[i].dest + " : " + result[i].weight + ")\r\n";
            }
            return mst;
        }
    }
    public class Class1
    {
        public int _V;
        //Adjacency Lists
        public LinkedList<int>[] _adj;

        /**
        * @name GraphBFS 
        * @param [in] v [\b int]
        * Graph using adjacency list
        **/
        public void GraphBFS(int V)
        {
            _adj = new LinkedList<int>[V];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<int>();
            }
            _V = V;
        }

        /**
        * @name AddEdgeBFS 
        * @param [in] v [\b int]
        * @param [in] w [\b int]
        * Function to add an edge into the graph
        **/
        public void AddEdgeBFS(int v, int w)
        {
            _adj[v].AddLast(w);

        }

        /**
        * @name BFS 
        * @param [in] s [\b int]
        * @retval [\b string]
        * Prints BFS traversal from a given sources
        * Breadth-First Traversal (or Search) for a graph is similar to Breadth-First Traversal of a tree.
        * The only catch here is, unlike trees, graphs may contain cycles, so we may come to the same node again.
        * To avoid processing a node more than once, we use a boolean visited array.
        * For simplicity, it is assumed that all vertices are reachable from the starting vertex. 
        **/
        public string BFS(int s)
        {
            string bfs = "";
            // Mark all the vertices as not
            // visited(By default set as false)
            bool[] visited = new bool[_V];
            for (int i = 0; i < _V; i++)
                visited[i] = false;

            // Create a queue for BFS
            LinkedList<int> queue = new LinkedList<int>();

            // Mark the current node as
            // visited and enqueue it
            visited[s] = true;
            queue.AddLast(s);

            while (queue.Any())
            {

                // Dequeue a vertex from queue
                // and print it
                s = queue.First();
                bfs += s + ", ";
                queue.RemoveFirst();

                // Get all adjacent vertices of the
                // dequeued vertex s. If a adjacent
                // has not been visited, then mark it
                // visited and enqueue it
                LinkedList<int> list = _adj[s];

                foreach (var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
            return bfs.Remove(bfs.Length - 2);
        }
        /************************************/
        /********** End of the BFS **********/
        /************************************/

        public int V;
        public List<int>[] Adj;

        /**
        * @name GraphDFS 
        * @param [in] v [\b int]
        * Graph using adjacency list
        **/
        public void GraphDFS(int v)
        {
            V = v;
            Adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                Adj[i] = new List<int>();
        }

        /**
        * @name AddEdgeDFS 
        * @param [in] v [\b int]
        * @param [in] w [\b int]
        * Function to Add an edge into the graph
        **/
        public void AddEdgeDFS(int v, int w)
        {
            Adj[v].Add(w); // Add w to v's list.
        }

        public string dfs = "";

        /**
        * @name DFSUtil 
        * @param [in] v [\b int]
        * @param [in] visited [\b bool[]]
        * A function used by DFS
        **/
        public void DFSUtil(int v, bool[] visited)
        {
            // Mark the current node as visited
            // and print it
            visited[v] = true;
            dfs += v + ", ";

            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = Adj[v];
            foreach (var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        /**
        * @name DFS 
        * @param [in] v [\b int]
        * @retval [\b string]
        * The function to do DFS traversal.
        * It uses recursive DFSUtil()
        * Depth First Traversal (or Search) for a graph is similar to Depth First Traversal of a tree.
        * The only catch here is, unlike trees, graphs may contain cycles.
        * To avoid processing a node more than once, use a boolean visited array. 
        **/
        public string DFS(int v)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            bool[] visited = new bool[V];

            // Call the recursive helper function
            // to print DFS traversal
            DFSUtil(v, visited);
            return dfs.Remove(dfs.Length - 2);
        }
        /************************************/
        /********** End of the DFS **********/
        /************************************/

        public class Graph
        {
            private int V; // No. of vertices
                           // Adjacency List Representation
            private List<int>[] adj;

            /**
            * @name Graph 
            * @param [in] v [\b int]
            * Graph using adjacency list
            **/
            public Graph(int v)
            {
                V = v;
                adj = new List<int>[v];
                for (int i = 0; i < v; ++i)
                    adj[i] = new List<int>();
            }

            /**
            * @name AddEdge 
            * @param [in] v [\b int]
            * @param [in] w [\b int]
            * Function to add an edge into the graph
            **/
            public void AddEdge(int v, int w)
            {
                adj[v].Add(w);
            }

            public static string SCC = "";

            /**
            * @name DFSUtil 
            * @param [in] v [\b int]
            * @param [in] visited [\b bool[]]
            * A function used by DFS
            **/
            public void DFSUtil(int v, bool[] visited)
            {
                // Mark the current node as visited
                // and print it
                visited[v] = true;

                SCC += v + ", ";

                // Recur for all the vertices
                // adjacent to this vertex
                List<int> vList = adj[v];
                foreach (var n in vList)
                {
                    if (!visited[n])
                        DFSUtil(n, visited);
                }
            }

            /**
            * @name Graph getTranspose
            * The transpose serves to take the reverse cross of the elements.
            **/
            public Graph getTranspose()
            {
                Graph g = new Graph(V);
                for (int v = 0; v < V; v++)
                {
                    // Recur for all the vertices adjacent to this vertex
                    List<int> vList = adj[v];
                    foreach (var i in vList)
                        g.adj[i].Add(v);
                }
                return g;
            }

            /**
            * @name fillOrder 
            * @param [in] v [\b int]
            * @param [in] visited [\b bool[]]
            * @param [in] stack [\b Stack]
            * It serves the order of filling just like the function name.
            **/
            public void fillOrder(int v, bool[] visited, Stack stack)
            {
                // Mark the current node as visited and print it
                visited[v] = true;

                // Recur for all the vertices adjacent to this vertex
                List<int> vList = adj[v];
                foreach (var n in vList)
                {
                    if (!visited[n])
                        fillOrder(n, visited, stack);
                }

                // All vertices reachable from v are processed by now,
                // push v to Stack
                stack.Push(new int?(v));
            }

            /**
            * @name Strongly_Connected_Components
            * @retval [\b string]
            * A directed graph is strongly connected if there is a path between all pairs of vertices.
            * A strongly connected component (SCC) of a directed graph is a maximal strongly connected subgraph.
            * Create an empty stack ‘S’ and do DFS traversal of a graph. In DFS traversal, after calling recursive DFS for adjacent vertices of a vertex, push the vertex to stack.
            * In the above graph, if we start DFS from vertex 0, we get vertices in stack as 1, 2, 4, 3, 0.
            * Reverse directions of all arcs to obtain the transpose graph.
            * One by one pop a vertex from S while S is not empty. Let the popped vertex be ‘v’. Take v as source and do DFS (call DFSUtil(v)).
            * The DFS starting from v prints strongly connected component of v.
            * In the above example, we process vertices in order 0, 3, 4, 2, 1 (One by one popped from stack).
            **/
            public string Strongly_Connected_Components()
            {
                Stack stack = new Stack();

                // Mark all the vertices as not visited (For first DFS)
                bool[] visited = new bool[V];
                for (int i = 0; i < V; i++)
                    visited[i] = false;

                // Fill vertices in stack according to their finishing
                // times
                for (int i = 0; i < V; i++)
                    if (visited[i] == false)
                        fillOrder(i, visited, stack);

                // Create a reversed graph
                Graph gr = getTranspose();

                // Mark all the vertices as not visited (For second DFS)
                for (int i = 0; i < V; i++)
                    visited[i] = false;

                // Now process all vertices in order defined by Stack
                while (stack.Count > 0)
                {
                    // Pop a vertex from stack
                    int v = (int)stack.Pop();

                    // Print Strongly connected component of the popped vertex
                    if (visited[v] == false)
                    {
                        gr.DFSUtil(v, visited);
                        SCC += "\r\n";
                    }
                }
                return SCC.Remove(SCC.Length - 4);
            }
        }
    }
}
