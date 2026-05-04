using System;
using System.Collections.Generic;
using System.Linq;

namespace ProbabilityAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] data = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21, 186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };
            
            Array.Sort(data);
            int n = data.Length;

            double mean = data.Average();

            var mode = data.GroupBy(x => x)
                           .OrderByDescending(g => g.Count())
                           .First().Key;

            double median = CalculatePercentile(data, 50);

            double variance = data.Select(x => Math.Pow(x - mean, 2)).Sum() / n;

            double stdDev = Math.Sqrt(variance);

            double p20 = CalculatePercentile(data, 20);
            double p50 = median;
            double q1 = CalculatePercentile(data, 25);
            double q2 = median;
            double q3 = CalculatePercentile(data, 75);

            double range = data.Max() - data.Min();

            double iqr = q3 - q1;

            double sumDeviations = data.Select(x => x - mean).Sum();

            Console.WriteLine($"--- Statistics for Q1 ---");
            Console.WriteLine($"(i) Mean: {mean:F2}");
            Console.WriteLine($"(ii) Mode: {mode}");
            Console.WriteLine($"(iii) Median: {median}");
            Console.WriteLine($"(iv) Variance: {variance:F2}");
            Console.WriteLine($"(v) P20: {p20}");
            Console.WriteLine($"(vi) P50: {p50}");
            Console.WriteLine($"(vii/ix) Third Quartile (Q3): {q3}");
            Console.WriteLine($"(viii) Second Quartile (Q2): {q2}");
            Console.WriteLine($"(x) Range: {range}");
            Console.WriteLine($"(xi) Interquartile Range (IQR): {iqr}");
            Console.WriteLine($"(xii) Standard Deviation: {stdDev:F2}");
            Console.WriteLine($"(xiii) Summation of Deviations: {sumDeviations:F10}");

            Console.WriteLine("\n--- Outlier Detection for Q2 ---");
            double lowerBound = q1 - (1.5 * iqr);
            double upperBound = q3 + (1.5 * iqr);
            
            foreach (var val in data)
            {
                bool isOutlier = val < lowerBound || val > upperBound;
                Console.WriteLine($"Number {val}: {(isOutlier ? "Is an Outlier!" : "Normal")}");
            }
        }

        static double CalculatePercentile(double[] sortedData, double percentile)
        {
            int n = sortedData.Length;
            double rank = (percentile / 100) * (n + 1);
            int index = (int)rank;
            double fraction = rank - index;

            if (index <= 0) return sortedData[0];
            if (index >= n) return sortedData[n - 1];

            return sortedData[index - 1] + fraction * (sortedData[index] - sortedData[index - 1]);
        }
    }
}
