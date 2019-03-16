using System;
using System.Linq;

namespace Calculator.Common.Tools
{
    public static class EnumExtensions
    {
        public static ILookup<TEnum, TAttribute> GetAttribute<TEnum, TAttribute>()
            where TAttribute : Attribute
            where TEnum : Enum
        {
            var type = typeof(TEnum);

            return Enum.GetValues(type)
                .Cast<TEnum>()
                .SelectMany(t =>
                    type.GetField(t.ToString()).GetCustomAttributes(false).OfType<TAttribute>()
                        .Select<TAttribute, ValueTuple<TEnum, TAttribute>>(a => (t, a)))
                .ToLookup(k => k.Item1, v => v.Item2);
        }
    }
}