using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Probability
{
    public static class ProbabilityOP
    {
        public static double Expectation(IEnumerable<double> elements)
        {
            double expectation = 0;

            var enu = elements.GetEnumerator();
            while(enu.MoveNext())
                expectation += enu.Current * (1.0/elements.Count());

            return expectation;
        }

        public static double Variance(IEnumerable<double> elements)
        {
            double variance = 0;
            double expectation = Expectation(elements);

            double expSqrElem = 0;
            var enu = elements.GetEnumerator();
            while (enu.MoveNext())
                expSqrElem += Math.Pow(enu.Current,2) * (1.0 / elements.Count());

            variance = expSqrElem - Math.Pow(expectation,2);

            return variance;
        }

        public static double StandardDeviation(IEnumerable<double> elements)
        {
            return Math.Sqrt(Variance(elements));
        }
    }
}
