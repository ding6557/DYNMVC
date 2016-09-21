﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Maticsoft.Common
{
    public class XMLHelper
    {
        public static Hashtable XMLToHashtable(string xmlData)
        {
            DataTable dt = XMLToDataTable(xmlData);
            return DataTableHelper.DataTableToHashtable(dt);
        }

        public static DataTable XMLToDataTable(string xmlData)
        {
            if (!String.IsNullOrEmpty(xmlData))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(xmlData));
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            return null;
        }

        public static DataSet XMLToDataSet(string xmlData)
        {
            if (!String.IsNullOrEmpty(xmlData))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(xmlData));
                return ds;
            }
            return null;
        }
    }
}
