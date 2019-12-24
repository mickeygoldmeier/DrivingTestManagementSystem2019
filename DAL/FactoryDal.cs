using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        private FactoryDal() { } // make the constructor private so it would be imposible to create

        private static IDAL instance = null;

        public static IDAL getDal()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }
    }
}
