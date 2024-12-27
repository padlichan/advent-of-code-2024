// See https://aka.ms/new-console-template for more information
using advent_of_code.december_1;
using advent_of_code.december_2;
using advent_of_code.december_3;
using advent_of_code.december_4;
using advent_of_code.december_5;
using advent_of_code.december_6;

//December 1
Console.WriteLine("December 1");
var data1 = Challenge1.GetData();
var distanceSum = Challenge1.CalculateDistance(data1);
Console.WriteLine($"Sum of distance: {distanceSum}");
var similarityScore = Challenge1.CalculateSimilarityScore(data1);
Console.WriteLine($"Similarity score: {similarityScore}");
Console.WriteLine();


//December 2
Console.WriteLine("December 2");
var data2 = Challenge2.GetData();
Console.WriteLine($"Safe reports: {Challenge2.CountSafeReports(data2)}");
Console.WriteLine($"Safe reports with dampening: {Challenge2.CountSafeReportsWithDampener(data2)}");
Console.WriteLine();

//December 3
//_ = new Challenge3();

//December 4
//_ = new Challenge4();

//December 5
//_ = new Challenge5();

//December 6
_ = new Challenge6();