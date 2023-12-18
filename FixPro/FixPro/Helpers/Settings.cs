using System;
using System.Collections.Generic;
using System.Text;

// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FixPro.Helpers

{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private const string LanguageKey = "Language_key";
        private const string UserNameKey = "UserName_key";
        private const string UserFristNameKey = "FristName_key";
        private const string UserLastNameKey = "LastName_key";
        private const string UserIdKey = "UserId_key";
        private const string EmailKey = "Email_key";
        private const string PhoneKey = "Phone_key";
        private const string AccountTypeKey = "AccountType_key";
        private const string AddressKey = "Address_key";
        private const string AddressArKey = "AddressAr_key";
        private const string PasswordKey = "Password_key";
        private const string UserPrictureKey = "UserPricture_key";
        private const string UserApprovedDataKey = "UserApprovedData_key";
        private const string ThemesKey = "Themes_key";
        private const string SortKey = "Sort_key";
        private const string CreateDateKey = "CreateDate_key";
        private const string AccountIdKey = "AccountId_key";
        private const string AccountNameKey = "AccountName_key";
        private const string BranchIdKey = "BranchId_key";
        private const string BranchNameKey = "BranchName_key";
        private const string UserRoleKey = "UserRol_key";
        private const string UserEmployeesKey = "UserEmployees_key";
        private const string DeviceIdKey = "DeviceId_key";
        private const string PlayerIdKey = "PlayerId_key";
        private const string OneSignalAppIdKey = "OneSignalAppId_key";
        private const string OneSignalRestKey = "OneSignalRest_key";
        private const string OneSignalAuthKey = "OneSignalAuth_key";
        private const string TypeTrackingSch_InvoKey = "TypeTrackingSch_Invo_key";


        private static readonly string SettingsDefault = string.Empty;


        #endregion

        public static string OneSignalAppId
        {
            get
            {
                return AppSettings.GetValueOrDefault(OneSignalAppIdKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(OneSignalAppIdKey, value);
            }
        }

        public static string OneSignalRest
        {
            get
            {
                return AppSettings.GetValueOrDefault(OneSignalRestKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(OneSignalRestKey, value);
            }
        }

        public static string OneSignalAuth
        {
            get
            {
                return AppSettings.GetValueOrDefault(OneSignalAuthKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(OneSignalAuthKey, value);
            }
        }

        public static string TypeTrackingSch_Invo
        {
            get
            {
                return AppSettings.GetValueOrDefault(TypeTrackingSch_InvoKey, "1");
            }
            set
            {
                AppSettings.AddOrUpdateValue(TypeTrackingSch_InvoKey, value);
            }
        }

        public static string DeviceId
        {
            get
            {
                return AppSettings.GetValueOrDefault(DeviceIdKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(DeviceIdKey, value);
            }
        }

        public static string PlayerId
        {
            get
            {
                return AppSettings.GetValueOrDefault(PlayerIdKey, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(PlayerIdKey, value);
            }
        }


        public static string UserEmployees
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserEmployeesKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserEmployeesKey, value);
            }
        }

        public static string AccountId
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccountIdKey, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccountIdKey, value);
            }
        }

        public static string AccountName
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccountNameKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccountNameKey, value);
            }
        }

        public static string BranchId
        {
            get
            {
                return AppSettings.GetValueOrDefault(BranchIdKey, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(BranchIdKey, value);
            }
        }
        public static string UserRole
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserRoleKey, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserRoleKey, value);
            }
        }


        public static string BranchName
        {
            get
            {
                return AppSettings.GetValueOrDefault(BranchNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(BranchNameKey, value);
            }
        }

        public static string AccountType
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccountTypeKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccountTypeKey, value);
            }
        }

        public static string SortData
        {
            get
            {
                return AppSettings.GetValueOrDefault(SortKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SortKey, value);
            }
        }

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static string Language
        {
            get
            {
                return AppSettings.GetValueOrDefault(LanguageKey, "en");
            }
            set
            {

                AppSettings.AddOrUpdateValue(LanguageKey, value);
            }
        }

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

        public static string UserFristName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserFristNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserFristNameKey, value);
            }
        }

        public static string UserLastName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserLastNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserLastNameKey, value);
            }
        }

        public static string CreateDate
        {
            get
            {
                return AppSettings.GetValueOrDefault(CreateDateKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CreateDateKey, value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }
        
        public static string Phone
        {
            get
            {
                return AppSettings.GetValueOrDefault(PhoneKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PhoneKey, value);
            }
        }

        public static string Address
        {
            get
            {
                return AppSettings.GetValueOrDefault(AddressKey, null);
            }
            set
            {

                AppSettings.AddOrUpdateValue(AddressKey, value);
            }
        }

        public static string AddressAr
        {
            get
            {
                return AppSettings.GetValueOrDefault(AddressArKey, null);
            }
            set
            {

                AppSettings.AddOrUpdateValue(AddressArKey, value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(PasswordKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PasswordKey, value);
            }
        }

        public static string ApprovedData
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserApprovedDataKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserApprovedDataKey, value);
            }
        }

        public static string UserPricture
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserPrictureKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserPrictureKey, value);
            }
        }

        public static string Themes
        {
            get
            {
                return AppSettings.GetValueOrDefault(ThemesKey, "Light");
            }
            set
            {
                AppSettings.AddOrUpdateValue(ThemesKey, value);
            }
        }
    }
}
