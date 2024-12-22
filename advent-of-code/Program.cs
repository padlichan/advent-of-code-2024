// See https://aka.ms/new-console-template for more information
using advent_of_code.december_1;
using advent_of_code.december_2;

//December 1
Console.WriteLine("December 1");
var data1 = Challenge1.GetData();
var distanceSum = Challenge1.CalculateDistance(data1);
Console.WriteLine($"Sum of distance: {distanceSum}");
var similarityScore = Challenge1.CalculateSimilarityScore(data1);
Console.WriteLine($"Similarity score: {similarityScore}");


//December 2
Console.WriteLine();
Console.WriteLine("December 2");
var data2 = Challenge2.GetData();
Console.WriteLine($"Safe reports: {Challenge2.CountSafeReports(data2)}");
