using System;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using Microsoft.CSharp.RuntimeBinder;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents application settings set from app or web config file.
    /// </summary>
    public static class AppSettings
    {
        private static readonly DynamicAppSettings _instance = new DynamicAppSettings();

        /// <summary>
        /// Gets a value indicating a dynamic object to get application settings.
        /// </summary>
        public static dynamic Instance
        {
            get
            {
                return _instance;
            }
        }

        private class DynamicAppSettings : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                if (binder == null)
                {
                    throw new ArgumentNullException("binder");
                }

                result = ConfigurationManager.AppSettings[binder.Name];
                if (result != null)
                {
                    return true;
                }

                throw new RuntimeBinderException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The '{0}' app-setting is not defined in the config file.",
                    binder.Name));
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                throw new RuntimeBinderException("AppSettings does not support settings a value.");
            }
        }
    }
}