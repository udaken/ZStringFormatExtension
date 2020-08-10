using System;
using System.Text;
using Cysharp.Text;

namespace ZStringFormatExtension
{
    public static partial class FormatExtension
    {
        static readonly UTF8Encoding utf8encoding = new UTF8Encoding(false);

        public static void Register()
        {
            RegisterUtf8Adaptor();
            Utf16ValueStringBuilder.RegisterTryFormat<System.Net.IPAddress>(TryFormat);
            Utf16ValueStringBuilder.RegisterTryFormat<System.Numerics.BigInteger>(TryFormat);
            Utf16ValueStringBuilder.RegisterTryFormat<System.Version>(TryFormat);
        }

        static bool TryFormat(System.Net.IPAddress value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _)
        {
            return value.TryFormat(destination, out charsWritten);
        }
        static bool TryFormat(System.Numerics.BigInteger value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _)
        {
            return value.TryFormat(destination, out charsWritten);
        }
        static bool TryFormat(System.Version value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _)
        {
            return value.TryFormat(destination, out charsWritten);
        }
    }
}
