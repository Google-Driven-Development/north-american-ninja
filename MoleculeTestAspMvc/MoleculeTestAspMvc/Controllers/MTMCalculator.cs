#region Imports
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

//***************************************************************************************************
#endregion

namespace molecule_test.Controllers
{

    internal class MTMCalculator
    {

        #region Constants
        //***************************************************************************************************

        //Constants for string literals
        private const string c_nymex = "nymex";
        private const string c_wti = "wti";
        private const string c_sp = " ";

        //***************************************************************************************************
        #endregion 

        #region Declarations
        //***************************************************************************************************

        //hold mtm values of trades
        private decimal[] mtmValues = new decimal[9];
        private List<Models.TradeInfo> m_tradeInfoList;
        private List<Models.NymexInfo> m_nymexInfoList;
        private List<Models.IceWTIInfo> m_iceWtiInfoList;
        private List<Models.IceBrentInfo> m_iceBrentInfoList;

        //***************************************************************************************************
        #endregion

        #region Properties
        //***************************************************************************************************

        //holds file path information
        public string TradeFile { get; set; }
        public string NymexFile { get; set; }
        public string IceWtiFile { get; set; }
        public string IceBrentFile { get; set; }

        //Stores generic List<T> data
        public List<Models.TradeInfo> TradeInfoList
        {
            get
            {
                if( m_tradeInfoList == null )
                {
                    var trade = new Models.Trade();
                    m_tradeInfoList = trade.ReadTradeFile( TradeFile );
                }
                return m_tradeInfoList;
            }
        }

        public List<Models.NymexInfo> NymexInfoList
        {
            get
            {
                if( m_nymexInfoList == null )
                {
                    var nymex = new Models.Nymex();
                    m_nymexInfoList = nymex.readNymexFile(NymexFile);
                }
                return m_nymexInfoList;
            }
        }

        public List<Models.IceWTIInfo> IceWtiInfoList
        {
            get
            {
                if( m_iceWtiInfoList == null )
                {
                    var icewti = new Models.IceWTI();
                    m_iceWtiInfoList = icewti.ReadIceWtiFile( IceWtiFile );
                }
                return m_iceWtiInfoList;
            }
        }

        public List<Models.IceBrentInfo> IceBrentInfoList
        {
            get
            {
                if( m_iceBrentInfoList == null )
                {
                    var icebrent = new Models.IceBrent();
                    m_iceBrentInfoList = icebrent.ReadIceBrentFile(IceBrentFile);
                }
                return m_iceBrentInfoList;
            }
        }

        //***************************************************************************************************
        #endregion

        #region Public Methods
        //***************************************************************************************************

        public decimal[] CalculateMtm()
        {
            //search through everything in the trade.csv
            for( var index = 1; index < 10; index++ )
            {
                string tenorStart = string.Empty, tenorEnd = string.Empty, mtmCurve = string.Empty;
                var volume = 0;
                decimal price = 0;

                //grabbing tenorStart, tenorEnd, mtmCurveType and volume sold
                tenorStart = TradeInfoList[ index ].getTenorStartColumn;
                tenorEnd = TradeInfoList[ index ].getTenorEndColumn;
                mtmCurve = TradeInfoList[ index ].getMTMCurveColumn;

                volume = Int32.Parse( TradeInfoList[ index ].getVolumeColumn );
                price = Convert.ToDecimal( TradeInfoList[ index ].getPriceColumn );
                tenorStart = tenorStart.Replace( '-', ' ' );
                tenorEnd = tenorEnd.Replace( '-', ' ' );

                //checking the curve type
                if( mtmCurve.ToLower().Contains( c_nymex ) )
                {
                    var marketPrice = Convert.ToDecimal( NymexInfoList[ index ].getSettle );
                    //search through nymex.csv

                    SearchNymexInfoList(NymexInfoList, mtmValues, index, tenorStart, tenorEnd, volume, price, marketPrice );
                }
                else if( mtmCurve.ToLower().Contains( c_wti ) )
                {
                    tenorStart = tenorStart.Replace( c_sp, string.Empty );
                    tenorEnd = tenorEnd.Replace( c_sp, string.Empty );

                    var marketPrice = Convert.ToDecimal( IceWtiInfoList[ index ].getSettle );

                    //search through ice_wti.csv
                    SearchIceWtiList(IceWtiInfoList, mtmValues, index, tenorStart, tenorEnd, volume, price, marketPrice );
                }
                else
                {
                    break;
                }
            }

            return mtmValues;
        }

        //calculate total portfolio mtm values
        public decimal CalculatePortfolio()
        {
            return mtmValues.Sum();
        }

        //***************************************************************************************************
        #endregion

        #region Static Methods
        //***************************************************************************************************

        //search through ice_wti.csv
        private static void SearchIceWtiList(List<Models.IceWTIInfo> icewtiInfoList, decimal[] mtmValues, int index, string tenorStart, string tenorEnd, int volume, decimal price, decimal marketPrice)
        {
            foreach(var icewtiIndex in icewtiInfoList.Where(icewtiIndex => icewtiIndex.getMonthYear.ToLower().Trim() == tenorStart.ToLower().Trim()))
            {
                if( icewtiIndex.getMonthYear.ToLower().Trim() != tenorEnd.ToLower().Trim() )
                {
                    mtmValues[ index - 1 ] = mtmValues[ index - 1 ] + volume * ( price - marketPrice );
                }
                else //add last tenorDate before exiting loop
                {
                    mtmValues[ index - 1 ] = mtmValues[ index - 1 ] + volume * ( price - marketPrice );
                    break;
                }
            }
        }

        //search through nymex.csv
        private static void SearchNymexInfoList(List<Models.NymexInfo> nymexInfoList, decimal[] mtmValues, int index, string tenorStart, string tenorEnd, int volume, decimal price, decimal marketPrice)
        {
            foreach( var nymIndex in nymexInfoList.Where( nymIndex => nymIndex.getMonthYear.ToLower().Trim() == tenorStart.ToLower().Trim() ) )
            {
                if( nymIndex.getMonthYear.ToLower().Trim() != tenorEnd.ToLower().Trim() )
                {
                    mtmValues[ index - 1 ] = mtmValues[ index - 1 ] + volume * ( price - marketPrice );
                }
                else //add last tenorDate before exiting loop
                {
                    mtmValues[ index - 1 ] = mtmValues[ index - 1 ] + volume * ( price - marketPrice );
                    break;
                }
            }
        }

        //***************************************************************************************************
        #endregion

    }

}