using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesSystem.Viewmodel
{
    public static class RankInfo
    {
        private static string rank_id;
        public static string Rank_id{ get { return rank_id; } set { rank_id = value; } }

        private static int rank_type;
        public static int Rank_type { get { return rank_type; } set { rank_type = value; } }
        private static int cash_limit;
        public static int Cash_limit { get { return cash_limit; } set { cash_limit = value; } }
        private static int discount;
        public static int Discount { get { return discount; } set { discount = value; } }

    }
}
