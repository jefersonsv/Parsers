using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Parsers.Internacional
{
    internal class PhoneNumberConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return new PhoneNumber();
            }
            if (value is string)
            {
                string s = value as string;
                if (s.Length <= 0)
                {
                    return new PhoneNumber();
                }
                return new PhoneNumber(s, true, culture);
            }
            throw new FormatException("Can not convert to type System.PhoneNumber.");
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(string)) | (destinationType == typeof(InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value != null)
            {
                if (value.GetType() != typeof(PhoneNumber))
                {
                    throw new ArgumentException("Invalid object type, expected type: System.PhoneNumber.", "value");
                }
            }

            if (destinationType == typeof(string))
            {
                if (value == null)
                {
                    return string.Empty;
                }
                PhoneNumber p = (PhoneNumber)value;
                return p.ToString(culture);
            }

            if (destinationType == typeof(InstanceDescriptor))
            {
                if (value == null)
                {
                    return null;
                }
                PhoneNumber p = (PhoneNumber)value;
                MemberInfo member = null;
                object[] arguments = null;

                member = typeof(PhoneNumber).GetConstructor(new Type[] { typeof(string), typeof(bool), typeof(IFormatProvider) });
                arguments = new object[] { p.ToString(culture), true, culture };

                if (member != null)
                {
                    return new InstanceDescriptor(member, arguments, true);
                }
                else
                {
                    return null;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
