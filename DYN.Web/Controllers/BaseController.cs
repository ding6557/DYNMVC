using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DYN.Framwork.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 字段验证的错误信息汇总
        /// </summary>
        protected string ErrorMessage
        {
            get
            {
                StringBuilder errorMessage = new StringBuilder();
                ModelState.ToList().ForEach(
                    m =>
                    {
                        if (m.Value.Errors.Count > 0)
                        {
                            m.Value.Errors.ToList().ForEach(
                                err =>
                                {
                                    if (!string.IsNullOrEmpty(err.ErrorMessage))
                                    {
                                        errorMessage.Append(err.ErrorMessage);
                                    }                                  
                                    else
                                    {
                                        errorMessage.Append("类型转换异常");
                                    }
                                    errorMessage.Append(";");
                                });
                        }
                    }
                    );
                return errorMessage.ToString().Trim(';') + "。";
            }
        }
    }
}