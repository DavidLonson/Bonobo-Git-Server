using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bonobo.Git.Server.Extensions
{
    public static class TypeExtensions
    {
        public static string GetDisplayValue(this Type type, string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentException("propertyName");

            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new InvalidOperationException("Type with this property does not exists");

            var displayAttribute = propertyInfo.GetCustomAttributes(true).FirstOrDefault(i => i.GetType().IsAssignableFrom(typeof(DisplayAttribute))) as DisplayAttribute;

            if (displayAttribute != null)
            {
                return displayAttribute.GetName();
            }

            return propertyName;
        }

        public static string NormalizeRepoName(this string displayName)
        {
            return displayName.NormalizeRepoName(false);
        }
        public static string NormalizeRepoName(this string displayName, bool forced)
        {
            if (forced || displayName.Any(v => v > 0xff || (!Char.IsLetterOrDigit(v) && v != Char.GetNumericValue('-') && v != Char.GetNumericValue('_'))))
                return Guid.NewGuid().ToString().Replace("-", "");
            else
                return displayName;
        }
    }
}