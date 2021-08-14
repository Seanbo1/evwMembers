using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;

namespace evwMembers
{

    public class Graph<T>
    {
        
        public Graph() { }

        /// <summary>
        /// Creates a graph based on nodes and edges (which connect the nodes)
        /// </summary>
        /// <param name="nodes">The nodes</param>
        /// <param name="edges">The edges</param>
        public Graph(IEnumerable<T> nodes, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var node in nodes)
                AddNode(node);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        /// <summary>
        /// Adds a node
        /// </summary>
        /// <param name="node">The node to add</param>
        public void AddNode(T node)
        {
            AdjacencyList[node] = new HashSet<T>();
        }

        /// <summary>
        /// Adds an edge
        /// </summary>
        /// <param name="edge">The edge to add</param>
        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }

    public class Traversal : AppLogicGraph
    {

        /// <summary>
        /// Gets the keyword hits
        /// </summary>
        /// <param name="id">The member id</param>
        /// <param name="keywords">The keyword(s) to search for...</param>
        /// <returns>An int array containing the nodes that match the keyword(s).</returns>
        private int[] GetKeywordHits(string id, string keywords)
        {
            DataTable dtHits = GetNodeHits(id, keywords);
            int[] hits = new int[dtHits.Rows.Count];

            for (int i=0; i < dtHits.Rows.Count; i++)
            {
                hits[i] = dtHits.Rows[i].Field<int>("objectId");
            }

            return hits;
        }

        /// <summary>
        /// Creates a dictionary of nodeId, nodeName for cross referencing between id's and the actual name.
        /// </summary>
        /// <returns>A dictionary of id / name</returns>
        private Dictionary<int,string> GetNodeNames()
        {
            Dictionary<int, string> dictNodes = new Dictionary<int, string>();
            DataTable dtNodes = GetGraphNodes();
            
            //int[] nodes = new int[dtNodes.Rows.Count];
            //NodeItem[] nodes = new NodeItem[dtNodes.Rows.Count];
            for (int i=0; i< dtNodes.Rows.Count; i++)
                {
                dictNodes.Add(dtNodes.Rows[i].Field<int>("objectId"), dtNodes.Rows[i].Field<string>("name"));
                }

            return dictNodes;
        }


        /// <summary>
        /// Gets the nodes from the database.
        /// </summary>
        /// <returns>An int array of Nodes.</returns>
        private int[] GetNodes()
        {
            DataTable dtNodes = GetGraphNodes();

            int[] nodes = new int[dtNodes.Rows.Count];
            for (int i = 0; i < dtNodes.Rows.Count; i++)
            {
                nodes[i] = dtNodes.Rows[i].Field<int>("objectId"); 
            }

            return nodes;
        }


        /// <summary>
        /// Gets the edges for the graph
        /// </summary>
        /// <returns>An array of int Tuples containing the Origin-->Destination edges.</returns>
        private System.Tuple<int, int>[] GetEdges()
        {
            DataTable dtEdges = GetGraphEdges();
            System.Tuple<int,int>[] edges = new Tuple<int,int>[dtEdges.Rows.Count];

            for (int i = 0; i < dtEdges.Rows.Count; i++)
            {
                edges[i] = Tuple.Create(dtEdges.Rows[i].Field<int>("relParent"), dtEdges.Rows[i].Field<int>("relChild"));
            }

            return edges;
        }


        /// <summary>
        /// Does the work to identify the paths to potential friends that have websites containing the requested keywords.
        /// </summary>
        /// <param name="startNode">The Member to search from</param>
        /// <param name="keywords">The keywords to search for</param>
        /// <returns>A list of paths from the Member to potential friends matching the requested keywords.</returns>
        public List<string> BuildGraph(string startNode, string keywords)
        {
            List<string> paths = new List<string>();              //The paths returned from the search.

            Dictionary<int, string> dictNodes = GetNodeNames();   //Get the node id/names dictionary.
            int[] nodes = GetNodes();                             //Get the node id's.
            Tuple<int, int>[] edges = GetEdges();                 //Get the edges.
            int[] hits = GetKeywordHits(startNode, keywords);


            // Don't bother building a graph and traversing if there are no keyword hits found.
            if (hits == null) 
            {
                Debug.Print("No experts found with skillset: " + keywords);
                paths.Add("No experts found with skillset: " + keywords);
                return paths;
            }

            // Don't bother building a graph and traversing if there are no keyword hits found.
            if (hits.Length == 0)
            {
                Debug.Print("No keywords found matching: " + keywords);
                paths.Add("No keywords found matching: " + keywords);
                return paths;
            }

            var memberGraph = new Graph<int>(nodes, edges);  //Build the graph containg the nodes and edges.

            int beginNode = Convert.ToInt32(startNode);
            var shortestPath = GetShortestPath(memberGraph, beginNode);

            //Only traverse the graph from the nodes that contain keyword hits back to the target member id...
            //For each member with matching keywords, we will always "attempt" to get the shortest path (of friends of friends) back to the target member..
            //The current "target" member is always excluded, as are any members that are not connected to the target member via a valid set of 1..n friend relationships.
            foreach (int node in hits)  
            {
             
                Debug.Print("Node = " + node);
                string shortPath = string.Join(", ", shortestPath(node));
                if (shortPath.Contains(",") == false)
                {
                    Debug.Print("shortest path to {0,2}: {1}", node, shortPath + " ==> No valid path");
                }
                else
                {
                    Debug.Print("shortest path to " + node + ": " + shortPath + " for keyword(s): " + keywords);
                    string[] ids = shortPath.Split(',');

                    string p = "";
                    foreach(string id in ids)
                    {
                        string name = dictNodes[int.Parse(id.Trim())];
                        p = p + "<a href='MemberDetail.aspx?id=" + id.Trim() + "'>" + name + "</a> &#8594; ";
                    }

                    p += " Information about: " + keywords;
                    paths.Add(p);
                }

                
            }

            return paths;
        }


        public Func<T, IEnumerable<T>> GetShortestPath<T>(Graph<T> graph, T start)
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[node])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = node;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, IEnumerable<T>> shortestPath = n => {
                var path = new List<T> { };

                var current = n;
                bool noPath = false;
                while (!current.Equals(start) & noPath == false)
                {
                    path.Add(current);

                    try
                    {
                        current = previous[current];
                    }
                    catch 
                    {
                        noPath = true;  // Exit when there is no way to reach the target.
                    }

                };

                if (noPath == false )
                {
                    path.Add(start);
                }
                
                path.Reverse();  // Puts the path in Source Member --> Friend n --> Target Member format.

                return path;
            };

            return shortestPath;
        }

    }
}