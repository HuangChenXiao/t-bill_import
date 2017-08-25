using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImport.model
{
    public class Sync_Table_PurchaseOrder
    {
        public int id { get; set; }
        public string voucherdate { get; set; }
        public string clerk_name { get; set; }
        public string partner_name { get; set; }
        public decimal pubuserdefdecm2 { get; set; }
        public string cinvcode { get; set; }
        public string cinvname { get; set; }
        public string priuserdefnvc1 { get; set; }
        public string freeItem1 { get; set; }
        public string freeItem2 { get; set; }
        public string freeItem3 { get; set; }
        public decimal quantity { get; set; }
        public decimal pubuserdefdecm1 { get; set; }
        public decimal quantity2 { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }

        public decimal pubuserdefdecm3 { get; set; }
        public decimal pubuserdefdecm4 { get; set; }
        public decimal priuserdefdecm1 { get; set; }
        public decimal priuserdefdecm2 { get; set; }

    }
}
