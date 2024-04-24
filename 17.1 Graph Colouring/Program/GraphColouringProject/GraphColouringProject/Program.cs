// See https://aka.ms/new-console-template for more information
using GraphColouringProject;

//Question1(); //Greedy algorithm color tests
Question4(); //Clique algorithm tests


void Question1()
{

    Console.WriteLine("Question 1");
    int numberOfRandomGraphs = 10;

    int verticesInRandomGraphs = 70;
    double probability = 0.5;


    //Do the G(70, 0.5) ones
    Console.WriteLine("Doing tests for G(70, 0.5)");
    List<VertexOrderedGraph> randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability));
    }
    Question1TestsWithGraphs(randomGraphs.ToArray());

    Console.WriteLine("Doing tests for G3(70, 0.75)");
    //Do the G3(70, 0.75) ones
    probability = 0.75;
    randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability, 3));
    }
    Question1TestsWithGraphs(randomGraphs.ToArray());

}

void Question4()
{

    



    Console.WriteLine("Question 4");

    //Do an example clique test

    int verticesInRandomGraphs = 10;
    double probability = 0.5;

    VertexOrderedGraph cliqueTestGraph = VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability);

    Console.WriteLine("Finding the cliques for the following graph:");
    Console.WriteLine(cliqueTestGraph.ToString());

    int[][] cliques = cliqueTestGraph.FindCliques();

    int cliqueNumber = cliques[0].Length;

    Console.WriteLine("It has clique number " + cliqueNumber);

    Console.WriteLine("Clique(s) found:");

    foreach (int[] clique in cliques)
    {
        Console.WriteLine(clique.ToFormattedString());
    }


    int numberOfRandomGraphs = 10;

    verticesInRandomGraphs = 70;
    

    //Do the G(70, 0.5) ones
    Console.WriteLine("Doing tests for G(70, 0.5)");
    List<VertexOrderedGraph> randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability));
    }
    Question4TestsWithGraphs(randomGraphs.ToArray());

    Console.WriteLine("Doing tests for G3(70, 0.75)");
    //Do the G3(70, 0.75) ones
    probability = 0.75;
    randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability, 3));
    }
    Question4TestsWithGraphs(randomGraphs.ToArray());
}

void Question1TestsWithGraphs(VertexOrderedGraph[] randomGraphs)
{
    VertexOrderedGraph[] orderedAscending = randomGraphs.Select(x => x.ReorderByAscendingDegree()).ToArray();
    VertexOrderedGraph[] orderedDescending = randomGraphs.Select(x => x.ReorderByDescendingDegree()).ToArray();
    VertexOrderedGraph[] orderedMinimumDegreeAmongSubgraph = randomGraphs.Select(x => x.ReorderByMinimalAmongSubgraphs()).ToArray();
    VertexOrderedGraph[] orderedRandomly = randomGraphs.Select(x => x.ReorderRandomly()).ToArray();


    for (int i = 0; i < randomGraphs.Length; i++)
    {
        Console.WriteLine("Graph " + (i+1) + " is " + randomGraphs[i].ToString());
    }



    for (int i = 0; i < randomGraphs.Length; i++)
    {
        Console.WriteLine((i + 1) + "\t" + orderedAscending[i].GreedyColouringNumberOfColours() + "\t" + orderedDescending[i].GreedyColouringNumberOfColours() + "\t" + orderedMinimumDegreeAmongSubgraph[i].GreedyColouringNumberOfColours() + "\t" + orderedRandomly[i].GreedyColouringNumberOfColours());
    }
}

void Question4TestsWithGraphs(VertexOrderedGraph[] randomGraphs)
{
    VertexOrderedGraph[] orderedAscending = randomGraphs.Select(x => x.ReorderByAscendingDegree()).ToArray();
    VertexOrderedGraph[] orderedDescending = randomGraphs.Select(x => x.ReorderByDescendingDegree()).ToArray();
    VertexOrderedGraph[] orderedMinimumDegreeAmongSubgraph = randomGraphs.Select(x => x.ReorderByMinimalAmongSubgraphs()).ToArray();
    VertexOrderedGraph[] orderedRandomly = randomGraphs.Select(x => x.ReorderRandomly()).ToArray();

    for (int i = 0; i < randomGraphs.Length; i++)
    {
        Console.WriteLine("Graph " + (i + 1) + " is " + randomGraphs[i].ToString());
    }


    Console.WriteLine("num\tupper bound\tlower bound");
    for (int i = 0; i < randomGraphs.Length; i++)
    {
        int upperBound = (new int[] {orderedAscending[i].GreedyColouringNumberOfColours(), orderedDescending[i].GreedyColouringNumberOfColours(), orderedMinimumDegreeAmongSubgraph[i].GreedyColouringNumberOfColours(), orderedRandomly[i].GreedyColouringNumberOfColours() }).Min();

        int lowerBound = randomGraphs[i].FindCliques()[0].Length; //clique number
        Console.WriteLine((i + 1) + "\t" + upperBound + "\t" + lowerBound);
    }

}