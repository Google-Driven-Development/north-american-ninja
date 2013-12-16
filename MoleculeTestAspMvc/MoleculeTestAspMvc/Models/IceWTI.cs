using System.Collections.Generic;
using System.IO;

namespace molecule_test.Models
{
    public class IceWTI
    {
        public List<IceWTIInfo> ReadIceWtiFile(string fileName)
        {
            var iceWtiList = new List<IceWTIInfo>();

            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() != -1)
                {
                    var line = sr.ReadLine();
                    var field = line.Split(',');

                    var obj = new IceWTIInfo(field[0], field[1], field[2], field[3], field[4], field[5], field[6], field[7], field[8], field[9], field[10], field[11]);

                    iceWtiList.Add(obj);
                }

                sr.Close();
            }
            return iceWtiList;
        }
    }

    public class IceWTIInfo
    {
        private string Month, Open, High, Low, Settle, Chg, Vol, EFP, EFS, Block, PrevDayVol, PrevDayOpen;

        public string getMonthYear { get { return Month; } }
        public string getOpen { get { return Open; } }
        public string getHigh { get { return High; } }
        public string getLow { get { return Low; } }
        public string getSettle { get { return Settle; } }
        public string getChg { get { return Chg; } }
        public string getVol { get { return Vol; } }
        public string getEFP { get { return EFP; } }
        public string getEFS { get { return EFS; } }
        public string getBlock { get { return Block; } }
        public string getPrevDayVol { get { return PrevDayVol; } }
        public string getPrevDayOpen { get { return PrevDayOpen; } }

        public IceWTIInfo(string monthYear, string open, string high, string low, string settle, string chg, string vol, string efp, string efs, string block, string prevDayVol, string prevDayOpen)
        {
            this.Month = monthYear;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Settle = settle;
            this.Chg = chg;
            this.Vol = vol;
            this.EFP = efp;
            this.EFS = efs;
            this.Block = block;
            this.PrevDayVol = prevDayVol;
            this.PrevDayOpen = prevDayOpen;
        }

    }

}
