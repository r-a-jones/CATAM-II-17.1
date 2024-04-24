using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphColouringProject
{

    
    /// <summary>
    /// A graph whose vertices are given an ordering 1, ..., n.
    /// </summary>
    public class VertexOrderedGraph
    {
        private static Random random = new Random();
        int numberOfVertices;

        (int, int)[] edges;

        public VertexOrderedGraph(int numberOfVertices, (int, int)[] edges)
        {
            this.numberOfVertices = numberOfVertices;
            this.edges = edges;
            FormatEdges();
        }

        public void FormatEdges()
        {
            edges = edges.RemoveAllTuplesWithEqualEntries();


            edges = edges.Select(x => x.Ordered()).ToArray();

            foreach ((int, int) edge in edges)
            {
                if (edge.Item2 > numberOfVertices || edge.Item2 <= 0 || edge.Item1 <= 0)
                {
                    throw new Exception("Invalid edge: " + edge.ToString());
                }
            }
        }

        public int NumberOfVerticesConnectedTo(int vertex)
        {
            int count = 0;
            foreach ((int, int) edge in edges)
            {
                if (edge.Item1 == vertex || edge.Item2 == vertex)
                {
                    count++;
                }
            }
            return count;
        }

        public bool AreConnected(int vertexA, int vertexB)
        {
            if (vertexA > vertexB)
            {
                return AreConnected(vertexB, vertexA);
            }
            return edges.Contains((vertexA, vertexB));
        }
        public int NumberOfVerticesConnectedTo(int vertex, int[] ignoring)
        {
            int count = 0;
            foreach ((int, int) edge in edges)
            {
                if (!(ignoring.Contains(edge.Item1) || ignoring.Contains(edge.Item2)))
                {
                    if (edge.Item1 == vertex || edge.Item2 == vertex)
                    {
                        count++;
                    }
                }
                
            }
            return count;
        }

        /// <summary>
        /// Give the list of all vertices we are connected to with a smaller number. e.g. if 3 is connected to 1,2, 4, returns [1,2]
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public int[] VerticesConnectedToWithSmallerID(int vertex)
        {
            List<int> result = new List<int>();
            for (int i = 1; i < vertex; i++)
            {
                if (edges.Contains((i, vertex)))
                {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Returns a new graph isomorphic to this one, where the vertices are ordered by increasing or descending degree. True if ascending, false if descending
        /// </summary>
        /// <returns></returns>
        private VertexOrderedGraph ReorderByAscendingDescendingDegree(bool ascending)
        {
            //a list containing each vertices ID followed by the number of vertices it is connected to
            List<(int, int)> numberOfConnectedStats = new List<(int, int)>();

            for (int i = 1; i <= numberOfVertices; i++)
            {
                numberOfConnectedStats.Add((i, NumberOfVerticesConnectedTo(i)));
            }

            if (ascending)
            {
                numberOfConnectedStats = numberOfConnectedStats.OrderBy(x => x.Item2).ToList();
            }
            else
            {
                numberOfConnectedStats = numberOfConnectedStats.OrderBy(x => -x.Item2).ToList();
            }


            int[] permutationSpecifier = numberOfConnectedStats.Select(x => x.Item1).ToArray();

            //permutation specifier tells us how we relabel the vertices. e.g. the slot at index 0 is where 1 is relabelled to and so on, due to array indexing.

            //we can now make the new graph

            List<(int, int)> newEdges = new List<(int, int)>();

            foreach ((int, int) edge in edges)
            {
                newEdges.Add((permutationSpecifier[edge.Item1 - 1], permutationSpecifier[edge.Item2 - 1]));


            }

            return new VertexOrderedGraph(numberOfVertices, newEdges.ToArray());
        }

        public VertexOrderedGraph ReorderByAscendingDegree()
        {
            return ReorderByAscendingDescendingDegree(true);
        }
        public VertexOrderedGraph ReorderByDescendingDegree()
        {
            return ReorderByAscendingDescendingDegree(false);
        }

        /// <summary>
        /// Reorder as in part iii of question 1
        /// </summary>
        /// <returns></returns>
        public VertexOrderedGraph ReorderByMinimalAmongSubgraphs()
        {
            //specifies that 1 -> permutationSpecifier[0], 2 -> permutationSpecifier[1], etc.
            List<int> permutationSpecifier = new List<int>();


            while (permutationSpecifier.Count < numberOfVertices)
            {
                int minimalDegreeAmongSubgraphFound = -1;
                int vertexWithMinimalDegree = 0;

                for (int i = 1; i <= numberOfVertices; i++)
                {
                    if (permutationSpecifier.Contains(i) == false) //not done this one yet
                    {
                        int degree = NumberOfVerticesConnectedTo(i, permutationSpecifier.ToArray());

                        if (degree < minimalDegreeAmongSubgraphFound || minimalDegreeAmongSubgraphFound == -1)
                        {
                            minimalDegreeAmongSubgraphFound = degree;
                            vertexWithMinimalDegree = i;
                        }

                    }
                }
                if (minimalDegreeAmongSubgraphFound == -1)
                {
                    throw new Exception("Was not able to find vertex with minimal degree");
                }

                permutationSpecifier.Add(vertexWithMinimalDegree);
            }

           // Console.WriteLine(permutationSpecifier.ToArray().ToFormattedString());

            List<(int, int)> newEdges = new List<(int, int)>();

            foreach ((int, int) edge in edges)
            {
                newEdges.Add((permutationSpecifier[edge.Item1 - 1], permutationSpecifier[edge.Item2 - 1]));


            }

            return new VertexOrderedGraph(numberOfVertices, newEdges.ToArray());

        }

        /// <summary>
        /// Reorders the graph randomly.
        /// </summary>
        /// <returns></returns>
        public VertexOrderedGraph ReorderRandomly()
        {
            //essentially, we shall generate a random permutation, and apply that to the vertices' labelling.
            List<int> verticesNotReordered = new List<int> ();

            for (int i = 1; i <= numberOfVertices; i++)
            {
                verticesNotReordered.Add(i);
            }

            List<int> permutationSpecifier = new List<int>();

            while (verticesNotReordered.Count > 0)
            {
                int index = random.Next(0, verticesNotReordered.Count);
                permutationSpecifier.Add(verticesNotReordered[index]);
                verticesNotReordered.RemoveAt(index);
            }

            List<(int, int)> newEdges = new List<(int, int)>();

            foreach ((int, int) edge in edges)
            {
                newEdges.Add((permutationSpecifier[edge.Item1 - 1], permutationSpecifier[edge.Item2 - 1]));


            }

            return new VertexOrderedGraph(numberOfVertices, newEdges.ToArray());
        }
        
        //choose from G(n, p)
        public static VertexOrderedGraph RandomGraph(int numberOfVertices, double edgeProbabiliy)
        {
            List<(int, int)> edges = new List<(int, int)>();
            for (int i = 1; i < numberOfVertices; i++)
            {
                for (int j = i+1; j <= numberOfVertices; j++)
                {
                    double randomInZeroOne = random.NextDouble();

                    if (randomInZeroOne <= edgeProbabiliy)
                    {
                        edges.Add((i, j));
                    }
                }
            }

            return new VertexOrderedGraph(numberOfVertices, edges.ToArray());
        }

        //choose from Gk(n, p)
        public static VertexOrderedGraph RandomGraph(int numberOfVertices, double edgeProbabiliy, int differenceModNotAllowed)
        {
            List<(int, int)> edges = new List<(int, int)>();
            for (int i = 1; i < numberOfVertices; i++)
            {
                for (int j = i + 1; j <= numberOfVertices; j++)
                {
                    if ((i-j).Mod(differenceModNotAllowed) != 0) //not i-j = 0 mod k
                    {
                        double randomInZeroOne = random.NextDouble();

                        if (randomInZeroOne <= edgeProbabiliy)
                        {
                            edges.Add((i, j));
                        }
                    }
                    
                }
            }

            return new VertexOrderedGraph(numberOfVertices, edges.ToArray());
        }

        /// <summary>
        /// Color this vertex ordered graph via greedy. Colours are given numbers 1, 2, ...
        /// </summary>
        /// <returns></returns>
        public int[] GreedyColouring()
        {
            int[] colors = new int[numberOfVertices];


            for (int i = 1; i <= numberOfVertices; i++)
            {
                //find the color of i

                

                int[] previousNeighbors = VerticesConnectedToWithSmallerID(i);

                List<int> neighbourColors = new List<int>();

                foreach (int neighbour in previousNeighbors)
                {
                    neighbourColors.Add(colors[neighbour - 1]); //-1 due to indexing
                }

                int currentColor = 1;
                while (neighbourColors.Contains(currentColor))
                {
                    currentColor++;
                }
                colors[i-1] = currentColor; //-1 due to indexing

            }
            return colors;
        }

        public int GreedyColouringNumberOfColours()
        {
            return GreedyColouring().Max();
        }

        public override string ToString()
        {
            string edgesString = "";

            foreach (var edge in edges)
            {
                edgesString += edge.ToString();
                edgesString += " ";
            }
            edgesString = edgesString.Trim();

            return "VertexOrderedGraph(" + numberOfVertices + ", " + edgesString + ")";
        }

        /// <summary>
        /// Find the cliques of this graph. The clique number of the graph is just the length of any element of the returned int[][]
        /// </summary>
        public int[][] FindCliques()
        {
            int i = 1;

            //The complete subgraphs of this graph of size i.
            List<int[]> completeSubgraphsOfSizei = new List<int[]>();

            //Construct it manually for i = 1. just all the singletons.

            for (int j = 1; j <= numberOfVertices; j++)
            {
                completeSubgraphsOfSizei.Add(new int[] { j });
            }

            //we use these recursively to construct all the complete subgraphs of size i+1.

            List<int[]> completeSubgraphsOfSizeiPlusOne = new List<int[]>();
            do
            {
                //Console.WriteLine(i);
                completeSubgraphsOfSizeiPlusOne = new List<int[]>();

                //try construct all the next level subgraphs by recursion

                foreach (int[] completeSubgraph in completeSubgraphsOfSizei)
                {
                    //we do this max thing so that we dont find things twice. e.g. we will find the complete subgraph 1, 2 starting from the singleton 1, but not from 2.
                    int maxOrderedNodeID = completeSubgraph.Max();


                    for (int j = maxOrderedNodeID+1; j <= numberOfVertices; j++)
                    {
                        //if j is connected to all of the stuff, thats a complete subgraph of size i+1
                        bool allAreConnected = true;
                        foreach (int originalNode in completeSubgraph)
                        {
                            if (AreConnected(originalNode, j) == false)
                            {
                                allAreConnected = false;
                                break;
                            }
                        }
                        if (allAreConnected) //new complete subgraph: this + j!
                        {
                            int[] newCompleteSubgraph = new int[completeSubgraph.Length + 1];

                            for (int k = 0; k < completeSubgraph.Length; k++)
                            {
                                newCompleteSubgraph[k] = completeSubgraph[k];
                            }
                            newCompleteSubgraph[completeSubgraph.Length] = j;

                            completeSubgraphsOfSizeiPlusOne.Add(newCompleteSubgraph);
                        }
                    }
                }



                i++;
                if (completeSubgraphsOfSizeiPlusOne.Count > 0) //more stuff to do at the next stage
                {
                    completeSubgraphsOfSizei = completeSubgraphsOfSizeiPlusOne.Copy();
                }
                

            } while (completeSubgraphsOfSizeiPlusOne.Count > 0); //we have found stuff of size i+1.


            i -= 1; //i is now the clique number of the graph

            return completeSubgraphsOfSizei.ToArray();

        }

        /// <summary>
        /// Returns the complement of this graph, i.e xy an edge iff xy not an edge in the complement
        /// </summary>
        /// <returns></returns>
        public VertexOrderedGraph Complement()
        {
            List<(int, int)> complementEdges = new List<(int, int)> ();
            for (int i = 1; i < numberOfVertices; i++)
            {
                for (int j = i+1; j <= numberOfVertices; j++)
                {
                    if (AreConnected(i,j) == false)
                    {
                        complementEdges.Add((i, j));
                    }
                }
            }
            return new VertexOrderedGraph(numberOfVertices, complementEdges.ToArray());
        }

        //Make a new graph, isomorphic to this - (the subgraph verticesToSubtract)
        public VertexOrderedGraph Subtract(int[] verticesToSubtract)
        {
            //verify input

            foreach (int vertex in verticesToSubtract)
            {
                if (vertex <= 0 || vertex > numberOfVertices)
                {
                    throw new Exception("Invalid vertex.");
                }
            }


            //first, get the new edges naively.

            List<(int, int)> newEdges = new List<(int, int)>();

            foreach ((int, int) edge in this.edges)
            {
                if (!(verticesToSubtract.Contains(edge.Item1) || verticesToSubtract.Contains(edge.Item2)))
                {
                    //still in the graph

                    //Console.WriteLine((edge.Item1, edge.Item2) + " still in the graph");
                    newEdges.Add((edge.Item1, edge.Item2));
                }
            }


            //now, relabel stuff where it is not needed, e.g. if we remove just 3, 1->1, 2->2, 4->3, 5->4

            int newNumberOfVertices = numberOfVertices - verticesToSubtract.Length;

            int newVertex = 1;
            for (int oldVertex = 1; oldVertex <= numberOfVertices; oldVertex++)
            {


                if (!verticesToSubtract.Contains(oldVertex))
                {
                    //oldVertex -> newVertex

                    //so replace all instances of oldVertex with newVertex

                    for (int i = 0; i < newEdges.Count; i++)
                    {
                        //this can be optimised a lot as we know how the edges are ordered in the array
                        //but it will have minimal improvement so i dont care
                        //it should be done if this program were extended to graphs with larger numbers of vertices
                        (int, int) oldNewEdge = newEdges[i];

                        if (oldNewEdge.Item1 == oldVertex)
                        {
                            oldNewEdge.Item1 = newVertex;
                        }
                        if (oldNewEdge.Item2 == oldVertex)
                        {
                            oldNewEdge.Item2 = newVertex;
                        }
                        newEdges[i] = oldNewEdge;
                    }


                    newVertex++;
                }
            }

            return new VertexOrderedGraph(newNumberOfVertices, newEdges.ToArray());
            

        }

        /// <summary>
        /// Colours the graph by the method as in Q5
        /// returns sets, each of which are a different colour
        /// 
        /// so our colouring uses result.Length colours.
        /// </summary>
        /// <returns></returns>
        public int[][] ColourByQuestion5Method()
        {
            int i = 1;
            VertexOrderedGraph currentGraph = new VertexOrderedGraph(this.numberOfVertices, this.edges); //will be G - I1 - ... - Ii-1 at stage i

            List<int[]> Is = new List<int[]>(); //[I1, I2, ...]

            List<int> currentRemovedVertices = new List<int>();
            while (currentGraph.numberOfVertices > 0)
            {
                //Console.WriteLine("current graph = " + currentGraph.ToString());
                VertexOrderedGraph currentComplement = currentGraph.Complement();

                int[] Ii = currentComplement.FindCliques()[0]; // just take the first clique we find to be Ii i suppose, will be the same size as all other cliques. guaranteed at least 1

                //note Ii is labelled as in the subgraph. need to convert it back to our original labelling

                //Console.WriteLine(Ii.ToFormattedString());
                //Console.WriteLine(Ii.Max());

                int oldMax = Ii.Max();
                int oldVertex = 1;

                int[] newIi = Ii.Copy();
                for (int newVertex = 1; newVertex <= Math.Max(numberOfVertices, Ii.Max()); newVertex++)
                {
                    while (currentRemovedVertices.Contains(oldVertex))
                    {
                        //Console.WriteLine(oldVertex);
                        oldVertex++;
                    }

                    //newVertex -> oldVertex

                    for (int j = 0; j < Ii.Length; j++)
                    {
                        if (Ii[j] == newVertex)
                        {
                            newIi[j] = oldVertex;
                        }
                    }

                    oldVertex++;
                }

                currentGraph = currentGraph.Subtract(Ii);


                Ii = newIi.Copy();


                Is.Add(Ii);

                foreach (int vertex in Ii)
                {
                    currentRemovedVertices.Add(vertex);
                }

                
                i++;


            }

            return Is.ToArray();

        }

    }

}
