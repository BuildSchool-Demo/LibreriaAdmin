using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ManagerViewModel
    {
        public class ManagerBaseModel
        {
          
            public int ManagerID { get; set; }
            /// <summary>
            /// 帳號
            /// </summary>
            public string ManagerName { get; set; }
            /// <summary>
            /// 密碼
            /// </summary>
            public string ManagerPassword { get; set; }
            /// <summary>
            /// 管理者名稱
            /// </summary>
            public string ManagerUserName { get; set; }
            /// <summary>
            /// 管理者等級
            /// </summary>
            public int ManagerRoleID { get; set; }
        }
        public class ManagerListResult
        {
            public List<ManagerSingleResult> ManagerList { get; set; }
        }
        public class ManagerSingleResult : ManagerBaseModel
        {

        }        
    }
}
