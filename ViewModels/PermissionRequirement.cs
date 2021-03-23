using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 無權限action
        /// </summary>
        public string DeniedAction { get; set; } = "/Manager/Login";

        /// <summary>
        /// 認證授權類型
        /// </summary>
        public string ClaimType { internal get; set; }
        /// <summary>
        /// 默認登錄頁面
        /// </summary>
        public string LoginPath { get; set; } = "/Manager/Login";
        /// <summary>
        /// 過期時間
        /// </summary>
        public TimeSpan Expiration { get; set; }
 
        public PermissionRequirement(string deniedAction, string claimType, TimeSpan expiration)
        {
            ClaimType = claimType;
            DeniedAction = deniedAction;
            Expiration = expiration;
        }
    }
}
