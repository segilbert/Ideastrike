using System;

namespace Ideastrike.Nancy.Models.ViewModels
{
    public class MetricViewModel
    {
        public MetricViewModel()
        {
            
        }

        public string Name { get; set; }
        public decimal Count { get; set; }

        public static string GetPercentage(decimal piTotalIdeas, decimal piCountByAuthor)
        {
            if (piTotalIdeas <= 0 || piCountByAuthor <= 0)
                return "0";

            decimal part = Math.Round(piCountByAuthor/piTotalIdeas,2);

            return GetFormatedPercentage( (int) (part * 100) );
        }

        private static string GetFormatedPercentage(int piValue)
        {
            return piValue.ToString(); // +"%";
        }

    }
}