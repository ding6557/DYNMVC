using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DYN.DAL;

namespace DYN.BLL.Contract
{
    public interface ILogService
    {
        void LogExcuteSql(string sql);
    }
}
