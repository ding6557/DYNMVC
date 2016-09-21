//=====================================================================================
// All Rights Reserved , Copyright © Learun 2013
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Maticsoft.Common
{
    /// <summary>
    /// 客户端提示信息帮助类
    /// </summary>
    public class ShowMsgHelper
    {
        /// <summary>
        /// 默认成功提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert(string message)
        {
            
            return string.Format("showTipsMsg('{0}','4000','4');windowload();", message);
        }
        /// <summary>
        /// 默认成功提示，刷新父窗口函数关闭页面
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertMsg(string message)
        {
            return string.Format("showTipsMsg('{0}','2500','4');top.main.windowload();OpenClose();", message);
        }
        /// <summary>
        /// 提示提示，关闭页面并刷新父窗口
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertCallback(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);

        }
        /// <summary>
        /// 提示提示，关闭页面并刷新父窗口
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertErrCallback(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','5');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);
        }

        /// <summary>
        /// 提示提示，关闭页面显示父窗口不刷新父页面
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertAndClose(string message)
        {
            return string.Format("showWarningMsg('{0}');OpenClose();", message);
        }

        /// <summary>
        /// 提示提示，关闭页面并刷新父窗口
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertCloseAndRefresh(string message)
        {
            return string.Format("showWarningMsg('{0}');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);
        }


        /// <summary>
        /// 默认成功提示，刷新父窗口函数关闭页面   --------------无效
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertParmCallback(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');top.$('#' + Current_iframeID())[0].contentWindow.target_right.windowload();OpenClose();", message);
        }
        /// <summary>
        /// 默认成功提示，刷新父窗口函数关闭页面   --------------有效
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertParmCallback_New(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);
        }
        /// <summary>
        /// 默认错误提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_Error(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','5');", message);
        }
        /// <summary>
        /// 默认警告提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_Warn(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','3');", message);
        }
        /// <summary>
        /// 提示警告信息
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string showFaceMsg(string message)
        {
            return string.Format("showFaceMsg('{0}');", message);
        }
        /// <summary>
        /// 提示警告信息
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string showWarningMsg(string message)
        {
            return string.Format("showWarningMsg('{0}');", message);
        }
        /// <summary>
        /// 默认提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string showMsg(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','4');OpenClose();", message);
        }
      
        /// <summary>
        /// 默认错误提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_ErrorMsg(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','5');bntback();", message);
        }

        /// <summary>
        /// 默认成功提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_SuccessMsg(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','4');bntback();", message);
        }
        /// <summary>
        /// 提示提示，关闭注册页面并跳转到系统页面
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertGoHome(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');window.parent.window.GoHome();OpenClose();", message);
        }
       
    }
}
