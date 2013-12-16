using System.Collections.Generic;
using System.IO;

namespace molecule_test.Models
{
    public class Nymex
    {
        public List<NymexInfo> readNymexFile(string fileName)
        {
            var nymexList = new List<NymexInfo>();

            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() != -1)
                {
                    var line = sr.ReadLine();
                    var field = line.Split(',');

                    var obj = new NymexInfo(field[0], field[1], field[2], field[3], field[4], field[5], field[6], field[7], field[8]);

                    nymexList.Add(obj);
                }

                sr.Close();
            }

            return nymexList;
        }
    }

    public class NymexInfo
    {
        private string MonthYear, Open, High, Low, Last, Change, Settle, EstimatedVolume, PriorDayOpenInterest;

        public string getMonthYear { get { return MonthYear; } }
        public string getOpen { get { return Open; } }
        public string getHigh { get { return High; } }
        public string getLow { get { return Low; } }
        public string getLast { get { return Last; } }
        public string getChange { get { return Change; } }
        public string getSettle { get { return Settle; } }
        public string getEstimatedVolume { get { return EstimatedVolume; } }
        public string getPriorDayOpenInterest { get { return PriorDayOpenInterest; } }

        public NymexInfo(string monthYear, string open, string high, string low, string last, string change, string settle, string estimatedVolume, string priorDayOpenInterest)
        {
            this.MonthYear = monthYear;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Last = last;
            this.Change = change;
            this.Settle = settle;
            this.EstimatedVolume = estimatedVolume;
            this.PriorDayOpenInterest = priorDayOpenInterest;
        }

    }

}
