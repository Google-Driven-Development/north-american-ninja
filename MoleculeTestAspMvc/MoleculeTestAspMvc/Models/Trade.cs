using System.Collections.Generic;
using System.IO;
using System.Web;

namespace molecule_test.Models
{

    public class Trade
    {

        public List<TradeInfo> ReadTradeFile(string fileName)
        {
            var tradeList = new List<TradeInfo>();

            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() != -1)
                {
                    var line = sr.ReadLine();
                    var field = line.Split(',');

                    var obj = new TradeInfo(field[0], field[1], field[2], field[3], field[4], field[5], field[6], field[7], field[8], field[9], field[10], field[11], field[12], field[13]);

                    tradeList.Add(obj);
                }

                sr.Close();
            }

            return tradeList;
        }

    }

    public class TradeInfo
    {

        private string IDColumn, CounterPartyColumn, TraderColumn, MTMCurveColumn, BuySellColumn, PriceColumn, VolumeColumn, TenorStartColumn, TenorEndColumn, DeliveryLocationColumn, InstrumentColumn, BrokerageColumn, TagsColumn, NotesColumn;

        public string getIDColumn { get { return IDColumn; } }
        public string getCounterPartyColumn { get { return CounterPartyColumn; } }
        public string getTraderColumn { get { return TraderColumn; } }
        public string getMTMCurveColumn { get { return MTMCurveColumn; } }
        public string getBuySellColumn { get { return BuySellColumn; } }
        public string getPriceColumn { get { return PriceColumn; } }
        public string getVolumeColumn { get { return VolumeColumn; } }
        public string getTenorStartColumn { get { return TenorStartColumn; } }
        public string getTenorEndColumn { get { return TenorEndColumn; } }
        public string getDeliveryLocationColumn { get { return DeliveryLocationColumn; } }
        public string getInstrumentColumn { get { return InstrumentColumn; } }
        public string getBrokerageColumn { get { return BrokerageColumn; } }
        public string getTagsColumn { get { return TagsColumn; } }
        public string getNotesColumn { get { return NotesColumn; } }

        public TradeInfo(string idColumn, string counterPartyColumn, string traderColumn, string mtmCurveColumn, string buySellColumn, string priceColumn, string volumeColumn, string tenorStartColumn, string tenorEndColumn, string deliveryLocationcolumn, string instrumentColumn, string brokerageColumn, string tagsColumn, string notesColumn)
        {
            this.IDColumn = idColumn;
            this.CounterPartyColumn = counterPartyColumn;
            this.TraderColumn = traderColumn;
            this.MTMCurveColumn = mtmCurveColumn;
            this.BuySellColumn = buySellColumn;
            this.PriceColumn = priceColumn;
            this.VolumeColumn = volumeColumn;
            this.TenorStartColumn = tenorStartColumn;
            this.TenorEndColumn = tenorEndColumn;
            this.DeliveryLocationColumn = deliveryLocationcolumn;
            this.InstrumentColumn = instrumentColumn;
            this.BrokerageColumn = brokerageColumn;
            this.TagsColumn = tagsColumn;
            this.NotesColumn = notesColumn;
        }

    }

}
