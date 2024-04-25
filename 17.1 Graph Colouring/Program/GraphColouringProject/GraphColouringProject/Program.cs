// See https://aka.ms/new-console-template for more information
using GraphColouringProject;

Question1(); //Greedy algorithm color tests
Question4(); //Clique algorithm tests
Question5(); //I1, I2, I3... tests
Question6(); //running time tests
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

void Question5()

{
    Console.WriteLine("Question 5");

    
    //Do an example clique test

    int verticesInRandomGraphs = 10;
    double probability = 0.5;

    VertexOrderedGraph cliqueTestGraph = VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability);

    Console.WriteLine("Finding I1, I2, I3, ... for the following graph:");
    Console.WriteLine(cliqueTestGraph.ToString());

    int[][] Is = cliqueTestGraph.ColourByQuestion5Method();

    

    Console.WriteLine("Found:");

    for (int i = 1; i <= Is.Length; i++)
    {
        Console.WriteLine("I_" + i + " = " + Is[i - 1].ToFormattedString());
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
    Question5TestsWithGraphs(randomGraphs.ToArray());

    //really slow
    /*
    Console.WriteLine("Doing tests for G3(70, 0.75)");
    //Do the G3(70, 0.75) ones
    probability = 0.75;
    randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability, 3));
    }
    Question5TestsWithGraphs(randomGraphs.ToArray());
    */

    
    Console.WriteLine("Doing tests for G3(70, 0.5)");
    //Do the G3(70, 0.5) ones
    probability = 0.5;
    randomGraphs = new List<VertexOrderedGraph>();

    for (int i = 0; i < numberOfRandomGraphs; i++)
    {
        randomGraphs.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, probability, 7));
    }
    Question5TestsWithGraphs(randomGraphs.ToArray());
    

    //Lets investigate different p

    
    //int numberOfRandomGraphs;
    numberOfRandomGraphs = 5; //it takes long so lets do half as much

    //int verticesInRandomGraphs = 70;

    for (int numberVertices = 30; numberVertices <= 70; numberVertices += 5)
    {
        Console.WriteLine("For " + numberVertices);
        Question5VaryProbabilityTests(numberOfRandomGraphs, numberVertices);
    }
    
    


}

void Question6()
{
    int numberOfRandomGraphsPerTest = 30;
    double edgeProbability = 0.5;
    Console.WriteLine("Question 6");

    Console.WriteLine("Find greedy algorithm - q1");

    Console.WriteLine("n\taverageSecondsToComputeAsc\taverageSecondsToComputeDsc\taverageSecondsToComputeMinimalAmong\taverageSecondsToComputeRandom");

    for (int numberOfVertices = 20; numberOfVertices <= 100; numberOfVertices += 5)
    {
        List<VertexOrderedGraph> randomGraphs = new List<VertexOrderedGraph>();

        for (int i = 0; i < numberOfRandomGraphsPerTest; i++)
        {
            randomGraphs.Add(VertexOrderedGraph.RandomGraph(numberOfVertices, edgeProbability));
        }

        //have the random graphs.
        VertexOrderedGraph[] orderedAscending = randomGraphs.Select(x => x.ReorderByAscendingDegree()).ToArray();
        VertexOrderedGraph[] orderedDescending = randomGraphs.Select(x => x.ReorderByDescendingDegree()).ToArray();
        VertexOrderedGraph[] orderedMinimumDegreeAmongSubgraph = randomGraphs.Select(x => x.ReorderByMinimalAmongSubgraphs()).ToArray();
        VertexOrderedGraph[] orderedRandomly = randomGraphs.Select(x => x.ReorderRandomly()).ToArray();

        //do the tests

        List<double> secondsToComputeAscending = new List<double>();
        List<double> secondsToComputeDescending = new List<double>();
        List<double> secondsToComputeMinimumDegreeAmongSubgraph = new List<double>();
        List<double> secondsToComputeRandomly = new List<double>();

        for (int i = 0; i < randomGraphs.Count; i++)
        {
            double seconds;
            DateTime startTime, endTime;

            //ascend
            startTime = DateTime.Now;
            orderedAscending[i].GreedyColouring();
            endTime = DateTime.Now;
            seconds = (endTime - startTime).TotalSeconds;
            secondsToComputeAscending.Add(seconds);


            //descend
            startTime = DateTime.Now;
            orderedDescending[i].GreedyColouring();
            endTime = DateTime.Now;
            seconds = (endTime - startTime).TotalSeconds;
            secondsToComputeDescending.Add(seconds);

            //minimal among
            startTime = DateTime.Now;
            orderedMinimumDegreeAmongSubgraph[i].GreedyColouring();
            endTime = DateTime.Now;
            seconds = (endTime - startTime).TotalSeconds;
            secondsToComputeMinimumDegreeAmongSubgraph.Add(seconds);

            //random
            startTime = DateTime.Now;
            orderedRandomly[i].GreedyColouring();
            endTime = DateTime.Now;
            seconds = (endTime - startTime).TotalSeconds;
            secondsToComputeRandomly.Add(seconds);


        }
        double averageSecondsAscending = secondsToComputeAscending.Average();
        double averageSecondsDescending = secondsToComputeDescending.Average();
        double averageSecondsMinimal = secondsToComputeMinimumDegreeAmongSubgraph.Average();
        double averageSecondsRandomly = secondsToComputeRandomly.Average();

        Console.WriteLine(numberOfVertices + "\t" + averageSecondsAscending + "\t" + averageSecondsDescending + "\t" + averageSecondsMinimal + "\t" + averageSecondsRandomly);


    }

    Console.WriteLine("Find cliques algorithm - q4");

    Console.WriteLine("n\taverageSecondsToCompute");
    

    for (int numberOfVertices = 30; numberOfVertices <= 70; numberOfVertices += 5)
    {
        List<VertexOrderedGraph> randomGraphs = new List<VertexOrderedGraph>();

        for (int i = 0; i < numberOfRandomGraphsPerTest; i++)
        {
            randomGraphs.Add(VertexOrderedGraph.RandomGraph(numberOfVertices, edgeProbability));
        }

        //have the random graphs.

        //do the tests

        List<double> secondsToCompute = new List<double>();

        foreach (VertexOrderedGraph randomGraph in randomGraphs)
        {
            DateTime startTime = DateTime.Now;

            randomGraph.FindCliques();

            DateTime endTime = DateTime.Now;

            double seconds = (endTime - startTime).TotalSeconds;

            secondsToCompute.Add(seconds);
        }
        double averageSeconds = secondsToCompute.Average();

        Console.WriteLine(numberOfVertices + "\t" + averageSeconds);


    }

    Console.WriteLine("Find I1, I2, ... algorithm - q5");

    Console.WriteLine("n\taverageSecondsToCompute");

    for (int numberOfVertices = 30; numberOfVertices <= 70; numberOfVertices += 5)
    {
        List<VertexOrderedGraph> randomGraphs = new List<VertexOrderedGraph>();

        for (int i = 0; i < numberOfRandomGraphsPerTest; i++)
        {
            randomGraphs.Add(VertexOrderedGraph.RandomGraph(numberOfVertices, edgeProbability));
        }

        //have the random graphs.

        //do the tests

        List<double> secondsToCompute = new List<double>();

        foreach (VertexOrderedGraph randomGraph in randomGraphs)
        {
            DateTime startTime = DateTime.Now;

            randomGraph.ColourByQuestion5Method();

            DateTime endTime = DateTime.Now;

            double seconds = (endTime - startTime).TotalSeconds;

            secondsToCompute.Add(seconds);
        }
        double averageSeconds = secondsToCompute.Average();

        Console.WriteLine(numberOfVertices + "\t" + averageSeconds);


    }

}
void Question5VaryProbabilityTests(int numberOfRandomGraphs, int verticesInRandomGraphs)
{
    Console.WriteLine("p\tGoldUpperBound\tGnewUpperBound\tG7oldUpperBound\tG7newUpperBound");
    for (int pTimes100 = 40; pTimes100 <= 60; pTimes100 += 1)
    {
        double p = (double)pTimes100 / 100; //to avoid weird stuff like 0.1 + 0.2 = 0.30000001 or whatever due to how doubles are stored
        List<VertexOrderedGraph> randomGraphsG = new List<VertexOrderedGraph>();
        List<VertexOrderedGraph> randomGraphsG7 = new List<VertexOrderedGraph>();

        for (int i = 0; i < numberOfRandomGraphs; i++)
        {
            randomGraphsG.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, p));
            randomGraphsG7.Add(VertexOrderedGraph.RandomGraph(verticesInRandomGraphs, p, 7));
        }
        Console.WriteLine();
        Console.Write(p + "\t");

        double GoldUpperBound = Question1UpperBoundAverage(randomGraphsG.ToArray());

        Console.Write(GoldUpperBound + "\t");
        double GnewUpperBound = Question5UpperBoundAverage(randomGraphsG.ToArray());
        Console.Write(GnewUpperBound + "\t");

        double G7oldUpperBound = Question1UpperBoundAverage(randomGraphsG7.ToArray());
        Console.Write(G7oldUpperBound + "\t");
        double G7newUpperBound = Question5UpperBoundAverage(randomGraphsG7.ToArray());
        Console.Write(G7newUpperBound);
        
        //Console.WriteLine(p + "\t" + GoldUpperBound + "\t" + GnewUpperBound + "\t" + G7oldUpperBound + "\t" + G7newUpperBound);
    }
    Console.WriteLine();
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

void Question5TestsWithGraphs(VertexOrderedGraph[] randomGraphs)
{
    VertexOrderedGraph[] orderedAscending = randomGraphs.Select(x => x.ReorderByAscendingDegree()).ToArray();
    VertexOrderedGraph[] orderedDescending = randomGraphs.Select(x => x.ReorderByDescendingDegree()).ToArray();
    VertexOrderedGraph[] orderedMinimumDegreeAmongSubgraph = randomGraphs.Select(x => x.ReorderByMinimalAmongSubgraphs()).ToArray();
    VertexOrderedGraph[] orderedRandomly = randomGraphs.Select(x => x.ReorderRandomly()).ToArray();

    for (int i = 0; i < randomGraphs.Length; i++)
    {
        Console.WriteLine("Graph " + (i + 1) + " is " + randomGraphs[i].ToString());
    }


    Console.WriteLine("num\toldUpperBound\tnewUpperBound\tlowerBound");
    for (int i = 0; i < randomGraphs.Length; i++)
    {
        int upperBoundOld = (new int[] { orderedAscending[i].GreedyColouringNumberOfColours(), orderedDescending[i].GreedyColouringNumberOfColours(), orderedMinimumDegreeAmongSubgraph[i].GreedyColouringNumberOfColours(), orderedRandomly[i].GreedyColouringNumberOfColours() }).Min();

        int upperBoundNew = randomGraphs[i].ColourByQuestion5Method().Length;

        int lowerBound = randomGraphs[i].FindCliques()[0].Length; //clique number
        Console.WriteLine((i + 1) + "\t" + upperBoundOld + "\t" + upperBoundNew + "\t" + lowerBound);
    }

}


double Question1UpperBoundAverage(VertexOrderedGraph[] randomGraphs)
{
    VertexOrderedGraph[] orderedAscending = randomGraphs.Select(x => x.ReorderByAscendingDegree()).ToArray();
    VertexOrderedGraph[] orderedDescending = randomGraphs.Select(x => x.ReorderByDescendingDegree()).ToArray();
    VertexOrderedGraph[] orderedMinimumDegreeAmongSubgraph = randomGraphs.Select(x => x.ReorderByMinimalAmongSubgraphs()).ToArray();
    VertexOrderedGraph[] orderedRandomly = randomGraphs.Select(x => x.ReorderRandomly()).ToArray();

    int[] upperBounds = new int[randomGraphs.Length];
    for (int i = 0; i < randomGraphs.Length; i++)
    {
        int upperBound = (new int[] { orderedAscending[i].GreedyColouringNumberOfColours(), orderedDescending[i].GreedyColouringNumberOfColours(), orderedMinimumDegreeAmongSubgraph[i].GreedyColouringNumberOfColours(), orderedRandomly[i].GreedyColouringNumberOfColours() }).Min();

        upperBounds[i] = upperBound;
    }

    int sum = upperBounds.Sum();

    return (double)sum / (double)randomGraphs.Length;
}

double Question5UpperBoundAverage(VertexOrderedGraph[] randomGraphs)
{
    int[] upperBounds = randomGraphs.Select(x => x.ColourByQuestion5Method().Length).ToArray();

    int sum = upperBounds.Sum();

    return (double)sum / (double)randomGraphs.Length;
}