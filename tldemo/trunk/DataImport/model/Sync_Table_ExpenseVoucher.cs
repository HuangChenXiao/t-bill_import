using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImport.model
{
    /// <summary>
	/// Sync_Table_ExpenseVoucher:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class Sync_Table_ExpenseVoucher
    {
        public Sync_Table_ExpenseVoucher()
        { }
        #region Model
        private int _id;
        private string _parent_name;
        private string _pubuserdefnvc4;
        private string _voucherdate;
        private string _priuserdefnvc1;
        private string _priuserdefnvc2;
        private decimal? _priuserdefdecm1;
        private decimal? _priuserdefdecm2;
        private decimal? _priuserdefdecm3;
        private string _priuserdefnvc5;//子表 字符公用自自定义项4 pubuserdefnvc4
        private decimal? _pubuserdefdecm3;
        private decimal? _pubuserdefdecm4;
        private string _priuserdefnvc3;
        private string _priuserdefnvc4;
        private string _pubuserdefnvc1;
        private decimal? _pubuserdefdecm1;
        private string _pubuserdefnvc3;
        private string _pubuserdefnvc2;
        private decimal? _price;
        private decimal? _money;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parent_name
        {
            set { _parent_name = value; }
            get { return _parent_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pubuserdefnvc4
        {
            set { _pubuserdefnvc4 = value; }
            get { return _pubuserdefnvc4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string voucherdate
        {
            set { _voucherdate = value; }
            get { return _voucherdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string priuserdefnvc1
        {
            set { _priuserdefnvc1 = value; }
            get { return _priuserdefnvc1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string priuserdefnvc2
        {
            set { _priuserdefnvc2 = value; }
            get { return _priuserdefnvc2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? priuserdefdecm1
        {
            set { _priuserdefdecm1 = value; }
            get { return _priuserdefdecm1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? priuserdefdecm2
        {
            set { _priuserdefdecm2 = value; }
            get { return _priuserdefdecm2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? priuserdefdecm3
        {
            set { _priuserdefdecm3 = value; }
            get { return _priuserdefdecm3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string priuserdefnvc5
        {
            set { _priuserdefnvc5 = value; }
            get { return _priuserdefnvc5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? pubuserdefdecm3
        {
            set { _pubuserdefdecm3 = value; }
            get { return _pubuserdefdecm3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? pubuserdefdecm4
        {
            set { _pubuserdefdecm4 = value; }
            get { return _pubuserdefdecm4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string priuserdefnvc3
        {
            set { _priuserdefnvc3 = value; }
            get { return _priuserdefnvc3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string priuserdefnvc4
        {
            set { _priuserdefnvc4 = value; }
            get { return _priuserdefnvc4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pubuserdefnvc1
        {
            set { _pubuserdefnvc1 = value; }
            get { return _pubuserdefnvc1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? pubuserdefdecm1
        {
            set { _pubuserdefdecm1 = value; }
            get { return _pubuserdefdecm1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pubuserdefnvc3
        {
            set { _pubuserdefnvc3 = value; }
            get { return _pubuserdefnvc3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pubuserdefnvc2
        {
            set { _pubuserdefnvc2 = value; }
            get { return _pubuserdefnvc2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? money
        {
            set { _money = value; }
            get { return _money; }
        }
        #endregion Model

    }
}

