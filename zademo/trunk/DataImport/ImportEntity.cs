using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataImport
{
    [Serializable]
    public class ImportEntity
    {
        public ImportEntity() { }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string AddUser { get; set; }
        public string IdInventoryClass { get { return ConfigurationManager.AppSettings["IdInventoryClass"]; } }
    }
}
