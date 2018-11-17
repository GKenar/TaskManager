﻿using System;
using System.ComponentModel;
using System.Linq;

namespace TaskManagerView.Components
{
    public static class EnumValueDescription
    {
        public static string GetDescription(this Enum enumVal)
        {
            Type enumType = enumVal.GetType();
            Array enumValues = Enum.GetValues(enumType);

            foreach (var value in enumValues)
            {
                if(value.Equals(enumVal))
                {
                    var fieldsInfo = enumType.GetField(enumType.GetEnumName(value));
                    var descriptionAttribute = fieldsInfo
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                        return descriptionAttribute.Description;
                }
            }

            return null;
        }
    }
}
