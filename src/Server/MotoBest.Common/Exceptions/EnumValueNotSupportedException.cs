namespace MotoBest.Common.Exceptions;

public class EnumValueNotSupportedException<TEnum> : Exception where TEnum : Enum
{
    public EnumValueNotSupportedException(TEnum enumValue)
        : base($"{enumValue} is a not supported {typeof(TEnum).Name} enum value.")
    {
    }
}
