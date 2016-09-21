using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XABYS.Common
{
    public class ShenFhenZhengHelper
    {
        /// <summary>
        /// 身份证中省级行政区，——前两位所对应的省级名称
        /// </summary>
        /// <param name="strTel"></param>
        /// <returns></returns>
        private static string[] XingZhengDiQu = {
            null, null, null, null, null, null, null, null, null, null, null,
            "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null,
            "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null,
            "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null,
            "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null,
            "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null,
            "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null,
            "台湾", null, null, null, null, null, null, null, null, null,
            "香港", "澳门", null, null, null, null, null, null, null, null,
            "国外"};

        /// <summary>
        /// 验证身份证号是否合法
        /// </summary>
        /// <param name="ShenFenZhengHao"></param>
        /// <returns></returns>
        public static bool isShenFenZhengHao(string ShenFenZhengHao)
        {
            int Len = -1;
            DateTime vBirthday;

            if (string.IsNullOrEmpty(ShenFenZhengHao)) return false; // 如果身份证号为空，返回假

            Len = ShenFenZhengHao.Length;
            if (Len != 18 && Len != 15) return false; // 如果身份证号长度不是18位或者15位，返回假


            int vProvince = int.Parse(ShenFenZhengHao.Substring(0, 2));
            if (vProvince > XingZhengDiQu.Length || string.IsNullOrEmpty(XingZhengDiQu[vProvince]))
            {
                return false; // 地址码错误
            }

            if (Len == 15)
            {
                for (int i = 0; i < 15; i++)
                {
                    if ("0123456789".IndexOf(ShenFenZhengHao[i]) < 0) return false; // 15位都必须是数字
                }
                if (!DateTime.TryParseExact("19" + ShenFenZhengHao.Substring(6, 6), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out vBirthday))
                {
                    return false; // 日期格式错误
                }
            }
            else
            {
                for (int i = 0; i < 17; i++)
                {
                    if ("0123456789".IndexOf(ShenFenZhengHao[i]) < 0) return false; // 前17位必须数字
                }

                if ("0123456789Xx".IndexOf(ShenFenZhengHao[17]) < 0) return false; // 最后一位必须数字或X

                if (!DateTime.TryParseExact(ShenFenZhengHao.Substring(6, 8), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out vBirthday))
                {
                    return false; // 日期格式错误
                }
            }

            if (vBirthday > DateTime.Now) return false; // 生日不可能晚于当前日期
            if (vBirthday < new DateTime(1900, 01, 01)) return false; // 生日不可能早于1900-01-01

            if (Len == 18)
            {
                int T = 0;
                for (int i = 0; i < 18; i++)
                {
                    int j = "xX".IndexOf(ShenFenZhengHao[i]) < 0 ? ShenFenZhengHao[i] - '0' : 10;
                    T += (int)Math.Pow(2, 17 - i) % 11 * j;
                }
                if (T % 11 != 1) return false; // 验证码错误
            }
            return true;
        }


        /// <summary>
        /// 15位身份证号转18位身份证号
        /// </summary>
        /// <param name="oldCard">15位身份证号</param>
        /// <returns>18位身份证号</returns>
        public static string ShenFenZheng15To18_old_err(string oldCard)
        {
            int[] coefficient = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };//17位身份证号每一位的系数
            char[] endNum = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };//身份证最后一位数组
            if (oldCard.Length == 15)//判断是否是15位
            {
                string newID = oldCard.Insert(6, "19");//在15位身份证号的第6位之后添加19，变成17位
                int s = 0;
                for (int i = 0; i < 17; i++)//17位号码每一位都乘以相应的系数
                {
                    int temp = Convert.ToInt32(newID[i]) * coefficient[i];
                    s += temp;//乘系数之后求和
                }
                s %= 11;//除以11取余
                newID += endNum[s].ToString();//17位号码加上 余数所代表的下标在尾数数组endNum中的字符
                return newID;//返回新ID号码
            }
            else
            {
                return "";
            }
        }


        public static string ShenFenZheng15To18(string perIDSrc)
        {
            int iS = 0;

            //加权因子常数
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数
            string LastCode = "10X98765432";
            //新身份证号
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字
            perIDNew += "19";

            perIDNew += perIDSrc.Substring(6, 9);

            //进行加权求和

            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

    }
}
