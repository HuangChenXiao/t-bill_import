using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImport
{
    public class HM_DataImportInfo
    {
        public HM_DataImportInfo()
        { }
        #region Model
        //主表字段
        public int id { get; set; }//id
        public decimal retailprice { get; set; }//零售价
        public string code { get; set; }//单据编号
        public string warehouse { get; set; }//仓库
        public string ccusname { get; set; }//客户名称
        public string linkman { get; set; }//联系人
        public string address { get; set; }//收货地址
        public string contactphone{get;set;}//手机号
        public string priuserdefnvc1 { get; set; }//销售属性
        public string priuserdefnvc2 { get; set; }//物流公司运单号
        public string priuserdefnvc3 { get; set; }//卖家备注
        public string saleInvoiceNo { get; set; }//发票
        public string memo { get; set; }//备注
        //子表字段
        public string idinventory { get; set; }//存货编码
        public string inventoryname { get; set; }//存货名称
        public decimal quantity { get; set; }//数量
        public decimal priuserdefdecm1 { get; set; }//运费
        public decimal taxamount { get; set; }//含税金额
        #endregion Model

    }
}

