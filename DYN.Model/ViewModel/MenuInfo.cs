using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model.ViewModel
{
    public class MenuInfo
    {
        public MenuInfo()
        {
            SysMenu = new SysMenu();
        }

        /// <summary>
        /// 获得所有节点的下拉菜单数据
        /// </summary>
        public List<SysMenu> JieDianXiaLa { get; set; }


        /// <summary>
        /// 当前菜单节点
        /// </summary>
        public SysMenu SysMenu { get; set; }
    }
}
