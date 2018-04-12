using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Partner_CreateUserFromMasterList.Helpers
{
    public static class PropertyBags
    {
        public const String PartnerImport_UserDomain = "PartnerImport.UserDomain";
        public const String PartnerImport_UserContainer = "PartnerImport.UserContainer";
        public const String PartnerImport_ClaimsProvider = "PartnerImport.ClaimsProvider";

        public const String Infrastructure_MaxPwdDays = "Infrastructure.MaxPwdDays";
        public const String Infrastructure_PwdNotificationDays = "Infrastructure.PwdNotificationDays";
    }
}
