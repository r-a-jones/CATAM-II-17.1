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
    }

}
