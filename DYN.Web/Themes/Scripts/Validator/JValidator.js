/* jquery 表单验证使用实例！  */
//获取Request notnull
var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];    // 加权因子   
var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];            // 身份证验证位值.10代表X   
function IdCardValidate(idCard) {
    idCard = trim(idCard.replace(/ /g, ""));               //去掉字符串头尾空格                     
    if (idCard.length == 15) {
        return isValidityBrithBy15IdCard(idCard);       //进行15位身份证的验证    
    } else if (idCard.length == 18) {
        var a_idCard = idCard.split("");                // 得到身份证数组   
        if (isValidityBrithBy18IdCard(idCard) && isTrueValidateCodeBy18IdCard(a_idCard)) {   //进行18位身份证的基本验证和第18位的验证
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}
/**  
* 判断身份证号码为18位时最后的验证位是否正确  
* @param a_idCard 身份证号码数组  
* @return  
*/
function isTrueValidateCodeBy18IdCard(a_idCard) {
    var sum = 0;                             // 声明加权求和变量   
    if (a_idCard[17].toLowerCase() == 'x') {
        a_idCard[17] = 10;                    // 将最后位为x的验证码替换为10方便后续操作   
    }
    for (var i = 0; i < 17; i++) {
        sum += Wi[i] * a_idCard[i];            // 加权求和   
    }
    valCodePosition = sum % 11;                // 得到验证码所位置   
    if (a_idCard[17] == ValideCode[valCodePosition]) {
        return true;
    } else {
        return false;
    }
}
/**  
* 验证18位数身份证号码中的生日是否是有效生日  
* @param idCard 18位书身份证字符串  
* @return  
*/
function isValidityBrithBy18IdCard(idCard18) {
    var year = idCard18.substring(6, 10);
    var month = idCard18.substring(10, 12);
    var day = idCard18.substring(12, 14);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 这里用getFullYear()获取年份，避免千年虫问题   
    if (temp_date.getFullYear() != parseFloat(year)
          || temp_date.getMonth() != parseFloat(month) - 1
          || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}
/**  
* 验证15位数身份证号码中的生日是否是有效生日  
* @param idCard15 15位书身份证字符串  
* @return  
*/
function isValidityBrithBy15IdCard(idCard15) {
    var year = idCard15.substring(6, 8);
    var month = idCard15.substring(8, 10);
    var day = idCard15.substring(10, 12);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 对于老身份证中的你年龄则不需考虑千年虫问题而使用getYear()方法   
    if (temp_date.getYear() != parseFloat(year)
              || temp_date.getMonth() != parseFloat(month) - 1
              || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}
//去掉字符串头尾空格   
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

function isRequestNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}
//验证不为空 notnull
function isNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}

//验证数字 num
function isInteger(obj) {
    reg = /^[-+]?\d+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证非数字 num
function isNotInteger(obj) {
    reg = /^[-+]?\D+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证数字 num  或者null,空
function isIntegerOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[-+]?\d+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email
function isEmail(obj) {
    reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email   或者null,空
function isEmailOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    //reg = /^\w{3,}@\w+(\.\w+)+$/;
    reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar
function isEnglishStr(obj) {
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar 或者null,空
function isEnglishStrOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否是n位数字字符串编号 nnum
function isLenNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}
//验证8位准入证编号
function isZhunRuZhengNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length != n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}
//验证年份为4位数字
function isYearOfFourNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length != n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证月份为2位数字且小于12
function isMonthOfTwoNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length != n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        if (Number(obj) > 12 || Number(obj) < 1)
            return false
        else
        return true;
    }
}

//验证日为2位数字且小于31
function isDayOfTwoNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length != n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        if (Number(obj) > 31 || Number(obj) < 1)
            return false
        else
            return true;
    }
}

//验证姓名为汉字和英文句点
function isName(obj) {
    //reg = /[^\u4E00-\u9FA5|·]/;改正则表达式无法检验一些生僻字
    reg = /[^\u0391-\uFFE5|·]/;
    if (obj != null && obj != "") {
        if (reg.test(obj)) {
            return false;
        } else {
            return true;
        }
    }
    else
        return false;
}

//验证是否是n位数字字符串编号 nnum或者null,空
function isLenNumOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否小于等于n位数的字符串 nchar
function isLenStr(obj, n) {
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length == 0 || obj.length > n)
        return false;
    else
        return true;
    //    if (!reg.test(obj)) {
    //        return false;
    //    } else {
    //        return true;
    //    }
}

//验证是否小于等于n位数的字符串 nchar或者null,空
function isLenStrOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    //    if (!reg.test(obj)) {
    //        return false;
    //    } else {
    //        return true;
    //    }
    else
        return true;
}

//验证是否电话号码 phone
function isTelephone(obj) {
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否电话号码 phone或者null,空
function isTelephoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile
function isMobile(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return false;
    }
    reg = /^0?(13[0-9]|15[0123456789]|18[0123456789]|17[0123456789]|14[0123456789])[0-9]{8}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile或者null,空
function isMobileOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^0?(13[0-9]|15[012356789]|18[0236789]|14[57])[0-9]{8}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone 
function isMobileOrPhone(obj) {
    reg_mobile = /^(\+\d{2,3}\-)?\d{11}$/;
    reg_phone = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg_mobile.test(obj) && !reg_phone.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone或者null,空
function isMobileOrPhoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    reg2 = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj) && !reg2.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri
function isUri(obj) {
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri或者null,空
function isUriOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证两个值是否相等 equals
function isEqual(obj1, controlObj) {
    if (obj1.length != 0 && controlObj.length != 0) {
        if (obj1 == controlObj)
            return true;
        else
            return false;
    }
    else
        return false;
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date
function isDate(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return false;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}
//判断日期类型是否为YYYY-MM格式的类型 date
function isNianYue(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return false;
    }
    if (obj.length != 0) {
        var _value = obj.toString() + '-01';
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(_value)) {
            return false;
        }
        else {
            return true;
        }
    }
}
//判断日期类型是否为YYYY-MM-DD格式的类型 date或者null,空
function isDateOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime
function isDateTime(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime或者null,空
function isDateTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time
function isTime(obj) {
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time或者null,空
function isTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为中文 cchar 
function isChinese(obj) {
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }

}

//判断输入的字符是否为中文 cchar或者null,空
function isChineseOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip
function isZip(obj) {
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip或者null,空
function isZipOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double
function isDouble(obj) {
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double或者null,空
function isDoubleOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断是否为身份证 idcard
function isIDCard(obj) {
    if (obj.length != 0) {
        if (!IdCardValidate(obj))
            return false;
        else
            return true;
    }
}

//判断是否为身份证 idcard或者null,空
function isIDCardOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{15}(\d{2}[A-Za-z0-9;])?$/;
        if (!reg.test(obj))
            return false;
        else
            return true;
    }
}
//判断是否为IP地址格式
function isIP(obj) {
    var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
    if (re.test(obj)) {
        if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
    }
    return false;
}
//判断是否为IP地址格式 或者null,空
function isIPOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
    if (re.test(obj)) {
        if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
    }
    return false;
}


//验证脚本
//obj为当前input所在的空间容器 (例如：Div,Panel)
//脚本中 checkvalue 验证函数  err 属性表示提示【中文名称】
function JudgeValidate(obj) {
    var Validatemsg = "";
    var Validateflag = true;
    $(obj).find("[datacol=yes]").each(function () {
        if ($(this).attr("checkexpession") != undefined) {
            switch ($(this).attr("checkexpession")) {
                case "default":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NotNull":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "不能为空！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Num":
                    {
                        if (!isInteger($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NotNum":
                    {
                        if (!isNotInteger($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "不能为空且不能有数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NumOrNull":
                    {
                        if (!isIntegerOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Email":
                    {
                        if (!isEmail($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EmailOrNull":
                    {
                        if (!isEmailOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStr":
                    {
                        if (!isEnglishStr($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseName":
                    {
                        if (!isName($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为汉字或·！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStrOrNull":
                    {
                        if (!isEnglishStrOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNum":
                    {
                        if (!isLenNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }                    
                case "isYearOfFourNum":
                    {
                        if (!isYearOfFourNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为符合年份的" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }                    
                case "isMonthOfTwoNum":
                    {
                        if (!isMonthOfTwoNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为符合月份的" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }                    
                case "isDayOfTwoNum":
                    {
                        if (!isDayOfTwoNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为符合天数的" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }                   
                case "isZhunRuZhengNum":
                    {
                        if (!isZhunRuZhengNum($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNumOrNull":
                    {
                        if (!isLenNumOrNull($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStr":
                    {
                        if (!isLenStr($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStrOrNull":
                    {
                        if (!isLenStrOrNull($(this).attr("value"), $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Phone":
                    {
                        if (!isTelephone($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Fax":
                    {
                        if (!isTelephoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为传真格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "PhoneOrNull":
                    {
                        if (!isTelephoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Mobile":
                    {
                        if (!isMobile($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrNull":
                    {
                        if (!isMobileOrnull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhone":
                    {
                        if (!isMobileOrPhone($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhoneOrNull":
                    {
                        if (!isMobileOrPhoneOrNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Uri":
                    {
                        if (!isUri($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "UriOrNull":
                    {
                        if (!isUriOrnull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Equal":
                    {
                        if (!isEqual($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "不相等！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Date":
                    {
                        var controlObj = $(this).attr("value");
                        if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
                            Validateflag = false;
                            ChangeCss($(this), "请选择毕业时间");
                            return false;
                        }
                        if (!isDateOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NianYue":
                    {
                        if (!isNianYue($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateOrNull":
                    {
                        if (!isDateOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTime":
                    {
                        if (!isDateTime($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTimeOrNull":
                    {
                        if (!isDateTimeOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Time":
                    {
                        if (!isTime($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "TimeOrNull":
                    {
                        if (!isTimeOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStr":
                    {
                        if (!isChinese($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为汉字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStrOrNull":
                    {
                        if (!isChineseOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为汉字！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Zip":
                    {
                        if (!isZip($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ZipOrNull":
                    {
                        if (!isZipOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Double":
                    {
                        if (!isDouble($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DoubleOrNull":
                    {
                        if (!isDoubleOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCard":
                    {
                        if (!isIDCard($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为18位身份证格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCardOrNull":
                    {
                        if (!isIDCardOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为身份证格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "RequestNotNull":
                    {
                        if (isNotNull($(this).attr("value"))) {
                            Validatemsg = $(this).attr("err") + "！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IsExist":
                    {
                        Validatemsg = $(this).attr("err") + "！\n";
                        Validateflag = false;
                        ChangeCss($(this), Validatemsg); return false;
                        break;
                    }
                case "IsIP":
                    {
                        if (!isIP($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为IP格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IPOrNull":
                    {
                        if (!isIPOrNullOrNull($(this).attr("value"), $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为IP格式！\n";
                            Validateflag = false;
                            ChangeCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "XueLi":
                    {
                        var controlObj = $(this).attr("value");
                        if (controlObj == "请选择") {
                            Validateflag = false;
                            ChangeCss($(this), "请选择学历");
                            return false;
                        }
                        break;
                    }
                case "XueZhi":
                    {
                        var controlObj = $(this).attr("value");
                        if (controlObj == "请选择") {
                            Validateflag = false;
                            ChangeCss($(this), "请选择学制");
                            return false;
                        }
                        break;
                    }
                case "PeiYangFangShi":
                    {
                        var controlObj = $(this).attr("value");
                        if (controlObj == "请选择") {
                            Validateflag = false;
                            ChangeCss($(this), "请选择培养方式");
                            return false;
                        }
                        break;
                    }

                default:
                    break;
            }
        }
    });
    if (Validatemsg.length > 0) {
        return Validateflag;
    }
    return Validateflag;
}
//修改出错的input的外观
function ChangeCss(obj, Validatemsg) {
    top.showTopMsg(Validatemsg, 5000, 'error');
    $('#tipTable').hide();
    $('.tooltipinputerr').removeClass("tooltipinputerr");
    $(obj).removeClass("x");
    if ($(obj).attr('class') == 'txt') {
        $(obj).addClass("tooltipinputerr");
        $(obj).removeClass("txt");
    } else if ($(obj).attr('class') == 'select') {
        $(obj).addClass("tooltipselecterr");
        $(obj).removeClass("select");
    }
    //$(obj).focus(); //焦点
    $('body').append('<table id="tipTable" class="tableTip"><tr><td  class="leftImage"></td> <td class="contenImage" align="left"></td> <td class="rightImage"></td></tr></table>');
    var X = $(obj).offset().top;
    var Y = $(obj).offset().left;
    $('#tipTable').css({ left: Y - 2 + 'px', top: X + 25 + 'px' });
    $('#tipTable').show()
    $('.contenImage').html(Validatemsg);
    $(obj).change(function () {
        if ($(obj).val() != "") {
            if ($(obj).attr('class') == 'txt') {
                $(obj).addClass("txt");
                $(obj).removeClass("tooltipinputerr");
            } else if ($(obj).attr('class') == 'select') {
                $(obj).addClass("select");
                $(obj).removeClass("tooltipselecterr");
            }
            $('#tipTable').remove()
        }
    });
    $(obj).blur(function () {
        if ($(obj).val() != "") {
            if ($(obj).attr('type') == 'text') {
                $(obj).addClass("txt");
                $(obj).removeClass("tooltipinputerr");
            } else {
                $(obj).removeClass("tooltipselecterr");
            }
            $('#tipTable').remove()
        }
    });
}

//修改出错的input的外观没有下标签提示
function ChangeCssWithoutDiv(obj,Validatemsg) {
    showTipsMsg(Validatemsg, '5000', '5');  
}

//验证脚本
//obj为当前input (例如：Div,Panel)
//脚本中 checkvalue 验证函数  err 属性表示提示【中文名称】
function JudgeValidateByID(obj) {
    var Validatemsg = "";
    var Validateflag = true;
    obj = "#" + obj;
    if ($(obj).attr("checkexpession") != undefined) {
        switch ($(obj).attr("checkexpession")) {
            case "default":
                {
                    if (isNotNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "NotNull":
                {
                    if (isNotNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "不能为空！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Num":
                {
                    if (!isInteger($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "NotNum":
                {
                    if (!isNotInteger($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "不能为空且者不能有数字！\n\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "NumOrNull":
                {
                    if (!isIntegerOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Email":
                {
                    if (!isEmail($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为E-mail格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "EmailOrNull":
                {
                    if (!isEmailOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为E-mail格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "EnglishStr":
                {
                    if (!isEnglishStr($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为字符串！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "EnglishStrOrNull":
                {
                    if (!isEnglishStrOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为字符串！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "ChineseName":
                {
                    if (!isName($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为汉字或·！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                        //ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "isYearOfFourNum":
                {
                    if (!isYearOfFourNum($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为符合年份的" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "isMonthOfTwoNum":
                {
                    if (!isMonthOfTwoNum($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为符合月份的" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "isDayOfTwoNum":
                {
                    if (!isDayOfTwoNum($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为符合天数的" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "isZhunRuZhengNum":
                {
                    if (!isZhunRuZhengNum($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "LenNum":
                {
                    if (!isLenNum($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "LenNumOrNull":
                {
                    if (!isLenNumOrNull($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须为" + $(obj).attr("length") + "位数字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "LenStr":
                {
                    if (!isLenStr($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须小于" + $(obj).attr("length") + "位字符！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "LenStrOrNull":
                {
                    if (!isLenStrOrNull($(obj).attr("value"), $(obj).attr("length"))) {
                        Validatemsg = $(obj).attr("err") + "必须小于" + $(obj).attr("length") + "位字符！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Phone":
                {
                    if (!isTelephone($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须电话格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Fax":
                {
                    if (!isTelephoneOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为传真格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "PhoneOrNull":
                {
                    if (!isTelephoneOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须电话格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Mobile":
                {
                    if (!isMobile($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为手机格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "MobileOrNull":
                {
                    if (!isMobileOrnull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为手机格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "MobileOrPhone":
                {
                    if (!isMobileOrPhone($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为电话格式或手机格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "MobileOrPhoneOrNull":
                {
                    if (!isMobileOrPhoneOrNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为电话格式或手机格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Uri":
                {
                    if (!isUri($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为网址格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "UriOrNull":
                {
                    if (!isUriOrnull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "必须为网址格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Equal":
                {
                    if (!isEqual($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "不相等！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Date":
                {
                    if (!isDateOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为日期格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "NianYue":
                {
                    if (!isNianYue($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为日期格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "DateOrNull":
                {
                    if (!isDateOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为日期格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "DateTime":
                {
                    if (!isDateTime($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为日期时间格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "DateTimeOrNull":
                {
                    if (!isDateTimeOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为日期时间格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Time":
                {
                    if (!isTime($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为时间格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "TimeOrNull":
                {
                    if (!isTimeOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为时间格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "ChineseStr":
                {
                    if (!isChinese($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为汉字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "ChineseStrOrNull":
                {
                    if (!isChineseOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为汉字！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Zip":
                {
                    if (!isZip($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为邮编格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "ZipOrNull":
                {
                    if (!isZipOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为邮编格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "Double":
                {
                    if (!isDouble($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为小数！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "DoubleOrNull":
                {
                    if (!isDoubleOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为小数！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "IDCard":
                {
                    if (!isIDCard($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为身份证格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "IDCardOrNull":
                {
                    if (!isIDCardOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为18位身份证格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "RequestNotNull":
                {
                    if (isNotNull($(obj).attr("value"))) {
                        Validatemsg = $(obj).attr("err") + "！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "IsExist":
                {
                    Validatemsg = $(obj).attr("err") + "！\n";
                    Validateflag = false;
                    ChangeCssWithoutDiv(obj,Validatemsg);
                    break;
                }
            case "IsIP":
                {
                    if (!isIP($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为IP格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "IPOrNull":
                {
                    if (!isIPOrNullOrNull($(obj).attr("value"), $(obj).attr("eqvalue"))) {
                        Validatemsg = $(obj).attr("err") + "必须为IP格式！\n";
                        Validateflag = false;
                        ChangeCssWithoutDiv(obj,Validatemsg);
                    }
                    break;
                }
            case "XueLi":
                {
                    var controlObj = $(obj).attr("value");
                    if (controlObj == "请选择") {
                        Validateflag = false;
                        ChangeCss($(obj), "请选择学历");
                        return false;
                    }
                    break;
                }
            case "PeiYangFangShi":
                {
                    var controlObj = $(obj).attr("value");
                    if (controlObj == "请选择") {
                        Validateflag = false;
                        ChangeCss($(obj), "请选择培养方式");
                        return false;
                    }
                    break;
                }
            default:
                break;
        }
    }
    if (Validatemsg.length > 0) {
        return Validateflag;
    }
    return Validateflag;
}