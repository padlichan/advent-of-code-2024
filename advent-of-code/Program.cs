// See https://aka.ms/new-console-template for more information
using advent_of_code.december_1;

Console.WriteLine("Hello, World!");

Data data = Challenge1.GetData();
var distanceSum = Challenge1.CalculateDistance(data);
var similarityScore = Challenge1.CalculateSimilarityScore(data);
Console.WriteLine(similarityScore);
