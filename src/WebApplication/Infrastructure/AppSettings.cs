using System;
using System.Configuration;
using System.Dynamic;
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
                return result != null;
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                throw new RuntimeBinderException("Supports only read-only properties.");
            }
        }
    }
}