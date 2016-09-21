/**
 * 函数返回值为：Datatable、Model实体、数值的方法调用此类的对应方法生成固定格式的json字符串
 * 固定格式的字符串调用相应的方法又可以转换为Datatable、Model实体、数值
 * json格式如下：1、返回datatable的格式：{success:true/false,data:[{},{},{}],msgcode:XXX}
 *               2、返回model的格式：{success：true/false,data:{},msgcode:XXX}
 *               3、返回int值的格式：{success：true/false,data:[{value:X}],msgcode:XXX}
 * 常聚川 
 * 2012年5月11号
 **/
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;

namespace XAUT.Common
{
    public static class JSONHelper
    {
        /// <summary>
        /// Model转JSON
        /// </summary>
        /// <param name="obj">model</param>
        /// <returns>JSON格式的字符串</returns>
        public static string ModelToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string temp = string.Empty;
                string result = string.Empty;
                temp = jss.Serialize(obj);
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != '\"')
                    {
                        result += temp[i];
                    }
                    else
                    {
                        result += "\"";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ModelToJSON(): " + ex.Message);
            }
        }

        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>指定类型的对象</returns>
        public static T JSONToModel<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON格式的字符串</returns>
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string str = jss.Serialize(obj);
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>指定类型的对象</returns>
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary>
        /// dataTable转换成Json格式  ，不包含表名的转换  
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string TableToJson(DataTable dt)
        {
            string result = string.Empty;
            string temp = string.Empty;
            temp = ObjectToJSON(DataTableToList(dt));
            return temp;
        }
        //public static string TableToJson1(DataTable dt)
        //{
        //    string result = string.Empty;
        //    string temp = string.Empty;
        //    temp = ObjectToJSON(DataTableToList(dt));
        //    try
        //    {

        //        for (int i = 0; i < temp.Length; i++)
        //        {
        //            if (temp[i] != '\"')
        //            {
        //                result += temp[i];
        //            }
        //            else
        //            {
        //                result += "\"";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("TableToJson1.ModelToJSON(): " + ex.Message);
        //    }
        //    return result;
        //}

        /// <summary>
        /// Json转DataTable，不包含表名的转换
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = jss.Deserialize<ArrayList>(json);
            DataTable dtb = new DataTable();
            if (dic.Count > 0)
            {
                foreach (Dictionary<string, object> drow in dic)
                {
                    if (dtb.Columns.Count == 0)
                    {
                        foreach (string key in drow.Keys)
                        {
                            if (drow[key] != null)
                                dtb.Columns.Add(key, drow[key].GetType());
                            else
                                dtb.Columns.Add(key, "null".GetType());
                        }
                    }
                    DataRow row = dtb.NewRow();
                    foreach (string key in drow.Keys)
                    {
                        try
                        {
                            row[key] = drow[key];
                        }
                        catch (Exception)
                        {
                            row[key] = DBNull.Value; ;
                        }
                    }
                    dtb.Rows.Add(row);
                }
            }
            return dtb;
        }

        /// <summary>
        /// 数据表转键值对集合
        /// 把DataTable转成 List集合, 存每一行
        /// 集合中放的是键值对字典,存每一列
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>哈希表数组</returns>
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list
            = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        #region 设置指定类型数据为 XAUT.OA 指定JSON格式


        /// <summary>
        /// 返回值为int类型的函数转换为json格式
        /// </summary>
        /// <param name="returnValue">函数返回值</param>
        /// <param name="boolValue">是否成功</param>
        /// <param name="msgCode">成功与错误的编码</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnIntToJSON(int returnValue, bool boolValue, int msgCode)
        {
            string result = string.Empty;
            result = "{\"success\":" + boolValue.ToString().ToLower() + ",\"msgcode\":" + msgCode.ToString() + ",\"data\":" + returnValue + "}";
            return result;
        }
        /// <summary>
        /// 返回值为int类型的函数转换为json格式
        /// </summary>
        /// <param name="returnValue">函数返回值</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnIntToJSON(int returnValue)
        {
            return ReturnIntToJSON(returnValue, true, 0);
        }

        /// <summary>
        /// 返回值为model类型的函数转换为json格式
        /// </summary>
        /// <param name="model">返回的字符串</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnStringToJSON(string String, bool boolValue, int msgCode)
        {
            string result = string.Empty;
            if (String[0]=='{')//String若是JSON格式则不加“”号
                result = "{\"success\":" + boolValue.ToString().ToLower() + ",\"msgcode\":" + msgCode.ToString() + ",\"data\":" + String + "}";
            else
                result = "{\"success\":" + boolValue.ToString().ToLower() + ",\"msgcode\":" + msgCode.ToString() + ",\"data\":\"" + String + "\"}";
            return result;
        }
        /// <summary>
        /// 返回值为model类型的函数转换为json格式
        /// </summary>
        /// <param name="model">返回的字符串</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnStringToJSON(string String)
        {
            return ReturnStringToJSON(String, true, 0);
        }

        /// <summary>
        /// 返回值为datatable类型的函数转换为json格式
        /// </summary>
        /// <param name="dataTable">函数返回值</param>
        /// <param name="boolValue">是否成功</param>
        /// <param name="msgCode">成功与错误的编码</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnDataTableToJSON(DataTable dataTable, bool boolValue, int msgCode)
        {
            string result = string.Empty;
            result = "{\"success\":" + boolValue.ToString().ToLower() + ",\"msgcode\":" + msgCode.ToString() + ",\"data\":" + TableToJson(dataTable) + "}";
            return result.Replace("null", "\" \"");
        }
        /// <summary>
        /// 返回值为datatable类型的函数转换为json格式
        /// </summary>
        /// <param name="dataTable">函数返回值</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnDataTableToJSON(DataTable dataTable)
        {
            return ReturnDataTableToJSON(dataTable, true, 0);
        }

        public static string ReturnDataTableToJSON1(DataTable dataTable)
        {
            return ReturnDataTableToJSON(dataTable, true, dataTable.Rows.Count);
        }
        /// <summary>
        /// 返回值为datatable类型的函数转换为json格式,
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="intValue">附带的一个整型数据</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnDataTableToJSON(DataTable dataTable, int intValue)
        {
            return ReturnDataTableToJSON(dataTable, true, intValue);
        }

        /// <summary>
        /// 返回值为model类型的函数转换为json格式
        /// </summary>
        /// <param name="model">函数返回值</param>
        /// <param name="boolValue">是否成功</param>
        /// <param name="msgCode">成功与错误的编码</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnModelToJSON(object model, bool boolValue, int msgCode)
        {
            string result = string.Empty;
            result = "{\"success\":" + boolValue.ToString().ToLower() + ",\"msgcode\":" + msgCode.ToString() + ",\"data\":" + ModelToJSON(model) + "}";
            return result;
        }
        /// <summary>
        /// 返回值为model类型的函数转换为json格式
        /// </summary>
        /// <param name="model">函数返回值</param>
        /// <returns>固定格式的json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        public static string ReturnModelToJSON(object model)
        {
            return ReturnModelToJSON(model, true, 0);
        }


        #endregion

        #region 获取数据，从 XAUT.OA 指定JSON格式的值中解析得出相应格式的数据
        /// <summary>
        /// 把含有DataTable数据的固定格式的json字符串转换为DataTable数据
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTableFromJSON(string json)
        {
            string temp = GetDataFromJSON(json);
            return JsonToDataTable(temp);
        }
        public static DataTable GetDataTableFromDataJSON(string json)
        {
            return JsonToDataTable(json);
        }
        /// <summary>
        /// 从 XAUT.OA 指定JSON格式的字符串中解析出指定Model类型的数据
        /// </summary>
        /// <typeparam name="T">model类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>Model实体</returns>
        public static T GetModelFromJSON<T>(string json)
        {
            string temp = GetDataFromJSON(json);
            return JSONToModel<T>(temp);
        }
        /// <summary>
        ///  从 XAUT.OA 指定JSON格式的字符串中解析出 int 数据
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>int值</returns>
        public static int GetIntFromJSON(string json)
        {
            string temp = GetDataFromJSON(json);
            return System.Convert.ToInt32(temp);
        }

        /// <summary>
        ///  从 XAUT.OA 指定JSON格式的字符串中解析出 string 数据
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>int值</returns>
        public static string GetStingFromJSON(string json)
        {
            return GetDataFromJSON(json);
        }

        #endregion

        /// <summary>
        /// 从 XAUT.OA 指定JSON格式的字符串中解析出执行是否成功的success的值：true/false
        /// </summary>
        /// <param name="json">返回的固定格式的json字符串</param>
        /// <returns>success的值：true/false</returns>
        public static string GetSuccessFromJSON(string json)
        {
            string temp = string.Empty;
            temp = GetStringBetween(json, "{\"success\":", ",\"msgcode");
            return temp;
        }
        /// <summary>
        ///  从 XAUT.OA 指定JSON格式的字符串中解析出msgCode的值：三位数字
        /// </summary>
        /// <param name="json">返回的固定格式的json字符串</param>
        /// <returns>msgcode的值：XXX</returns>
        public static string GetMsgcodeFromJSON(string json)
        {
            string temp = string.Empty;
            temp = GetStringBetween(json, ",\"msgcode\":", ",\"data\":");
            return temp;
        }
        /// <summary>
        /// 从 XAUT.OA 指定JSON格式的字符串中解析出 Data 部分的内容
        /// </summary>
        /// <param name="json">返回的固定格式的json字符串</param>
        /// <returns>success的值：true/false</returns>
        public static string GetDataFromJSON(string json)
        {
            string temp = string.Empty;
            int begin = json.IndexOf("\"content\":");
            if (begin > 0)
                begin += 10;
            else
            {
                begin = 0;
            }
            int end = json.Length - 1;
            temp = json.Substring(begin, end - begin);
            temp = temp.Trim();
            if (!temp.StartsWith("{")) temp = "{" + temp;
            if (!temp.EndsWith("}")) temp = temp + "}";
            return temp;

        }


        /// <summary>
        /// 截取一个字符串中给定两个字符串之间的部分
        /// </summary>
        /// <param name="o_str">待截取的字符串</param>
        /// <param name="b_str">起始字符串</param>
        /// <param name="e_str">结束字符串</param>
        /// <returns></returns>
        private static string GetStringBetween(string o_str, string b_str, string e_str)
        {
            int i, j;

            i = o_str.IndexOf(b_str);
            if (i < 0)
                i = 0;
            else
                i = i + b_str.Length;

            j = o_str.IndexOf(e_str);
            if (j < 0)
                j = o_str.Length;

            return o_str.Substring(i, j - i);
        }


        /// <summary>
        /// 返回值为int类型的函数转换为简单json格式
        /// </summary>
        /// <param name="returnValue">函数返回值</param>
        /// <returns>固定格式的简单json字符串，{success：X，data：[{}]，msgcode:XXX}</returns>
        private static string ReturnIntToShortJSON(int returnValue)
        {
            string result = string.Empty;
            result = "{\"data\":[{\"value\":" + returnValue + "}]}";
            return result;
        }
        /// <summary>
        ///  把含有Int值的数据的固定格式的简单json字符串转换为int数据
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>int值</returns>
        private static int GetDataFromShortJsonToInt(string json)
        {
            string resultInt = string.Empty;
            string temp = string.Empty;

            resultInt = GetStringBetween(json, "value\":", "}]");
            int result = System.Convert.ToInt32(resultInt);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="Content"></param>
        /// <param name="PageCount"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string ReturnShowListJSON(string RowCount, string Content, string PageCount, string PageIndex)
        {
            string result = string.Empty;
            result = "{count:" + RowCount + ",content:" + Content + ",pa:" + PageCount + ",pn:" + PageIndex + "}";
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="Content"></param>
        /// <param name="PageCount"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string ReturnddlListJSON(DataTable dt)
        {
            string json = string.Empty;

            json = "{success:true,content:[{t:\'请选择\',v:\'\'},";
            if (dt.Rows.Count == 1)
            {
                string ID = dt.Rows[0]["ID"].ToString();
                string MingCheng = dt.Rows[0]["MingCheng"].ToString();
                json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'}]}";
                return json;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i == dt.Rows.Count - 1)
                    {
                        string ID = dt.Rows[i]["ID"].ToString();
                        string MingCheng = dt.Rows[i]["MingCheng"].ToString();
                        json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'}]}";
                    }
                    else
                    {
                        string ID = dt.Rows[i]["ID"].ToString();
                        string MingCheng = dt.Rows[i]["MingCheng"].ToString();
                        json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'},";
                    }
                }
                return json;
            }
        }


        public static string ReturnddlchepaihaoListJSON(DataTable dt)
        {
            string json = string.Empty;

            json = "{success:true,content:[{t:\'请选择\',v:\'\'},";
            if (dt.Rows.Count == 1)
            {
                string ID = dt.Rows[0]["ID"].ToString();
                string ChePaiHao = dt.Rows[0]["ChePaiHao"].ToString();
                json += "{t:\'" + ChePaiHao + "\',v:\'" + ID + "\'}]}";
                return json;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i == dt.Rows.Count - 1)
                    {
                        string ID = dt.Rows[i]["ID"].ToString();
                        string ChePaiHao = dt.Rows[i]["ChePaiHao"].ToString();
                        json += "{t:\'" + ChePaiHao + "\',v:\'" + ID + "\'}]}";
                    }
                    else
                    {
                        string ID = dt.Rows[i]["ID"].ToString();
                        string ChePaiHao = dt.Rows[i]["ChePaiHao"].ToString();
                        json += "{t:\'" + ChePaiHao + "\',v:\'" + ID + "\'},";
                    }
                }
                return json;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="Content"></param>
        /// <param name="PageCount"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string ReturnddlMingChengListJSON(DataTable dt)
        {
            string json = string.Empty;

            json = "{success:true,content:[{t:\'请选择\',v:\'\'},";
            if (dt.Rows.Count == 1)
            {
                string ID = dt.Rows[0]["MingCheng"].ToString();
                string MingCheng = dt.Rows[0]["MingCheng"].ToString();
                json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'}]}";
                return json;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i == dt.Rows.Count - 1)
                    {
                        string ID = dt.Rows[i]["MingCheng"].ToString();
                        string MingCheng = dt.Rows[i]["MingCheng"].ToString();
                        json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'}]}";
                    }
                    else
                    {
                        string ID = dt.Rows[i]["MingCheng"].ToString();
                        string MingCheng = dt.Rows[i]["MingCheng"].ToString();
                        json += "{t:\'" + MingCheng + "\',v:\'" + ID + "\'},";
                    }
                }
                return json;
            }
        }
    }
}