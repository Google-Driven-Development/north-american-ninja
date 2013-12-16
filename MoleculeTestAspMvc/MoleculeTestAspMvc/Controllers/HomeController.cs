using System.IO;
using System.Web.Mvc;
using molecule_test.Controllers;

namespace MoleculeTestAspMvc.Controllers
{
    public class HomeController : Controller
    {

        private const string c_csvFilePath = "~/Content/DB/";
        private const string c_iceWtiFileName = "ice_wti.csv";
        private const string c_nymexFileName = "nymex.csv";
        private const string c_tradesFileName = "trades.csv";
        private const string c_iceBrentFileName = "ice_brent.csv";

        private MTMCalculator m_mtmCalculator;

        private MTMCalculator MtmCalculator
        {
            get
            {
                return m_mtmCalculator ?? (m_mtmCalculator = new MTMCalculator()
                                                                {
                                                                    IceWtiFile = Path.Combine(Server.MapPath(c_csvFilePath), c_iceWtiFileName),
                                                                    NymexFile = Path.Combine(Server.MapPath(c_csvFilePath), c_nymexFileName),
                                                                    TradeFile = Path.Combine(Server.MapPath(c_csvFilePath), c_tradesFileName),
                                                                    IceBrentFile = Path.Combine(Server.MapPath(c_csvFilePath), c_iceBrentFileName)
                                                                });
            }
        }

        public ActionResult Index()
        {
            ViewBag.MtmValues = MtmCalculator.CalculateMtm();
            ViewBag.PortfolioSum = MtmCalculator.CalculatePortfolio();
            ViewBag.Trades = MtmCalculator.TradeInfoList;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
