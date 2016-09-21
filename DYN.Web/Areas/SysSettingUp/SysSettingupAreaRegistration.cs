using System.Web.Mvc;

namespace DYN.Framwork.Areas.SysSettingUp
{
    public class SysSettingUpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SysSettingUp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SysSettingUp_default",
                "SysSettingUp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}