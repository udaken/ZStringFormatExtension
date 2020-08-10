
using System;
using System.Buffers;
using System.Text;

using Cysharp.Text;

namespace ZStringFormatExtension
{
    public static partial class FormatExtension
    {
        private static void RegisterUtf8Adaptor()
        {
#if !NETSTANDARD2_0
            Utf8ValueStringBuilder.RegisterTryFormat<System.Net.IPAddress>(TryFormat);
            Utf8ValueStringBuilder.RegisterTryFormat<System.Net.IPEndPoint>(TryFormat);
            Utf8ValueStringBuilder.RegisterTryFormat<System.Numerics.BigInteger>(TryFormat);
            Utf8ValueStringBuilder.RegisterTryFormat<System.Version>(TryFormat);
#endif
        }

#if !NETSTANDARD2_0
        static bool TryFormat(System.Net.IPAddress value, Span<byte> destination, out int written, StandardFormat format)
        {
            int buflen = 256;
            Span<char> utf16 = stackalloc char[buflen];
            int utf16len;
            do
            {
                utf16 = stackalloc char[buflen];
                if (value.TryFormat(utf16, out utf16len))
                {
                    break;
                }
                buflen *= 2;
            }while(buflen <= 64 * 1024);

            if(utf16len == 0)
            {
                written = 0;
                return false;
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }
        static bool TryFormat(System.Net.IPEndPoint value, Span<byte> destination, out int written, StandardFormat format)
        {
            int buflen = 256;
            Span<char> utf16 = stackalloc char[buflen];
            int utf16len;
            do
            {
                utf16 = stackalloc char[buflen];
                if (value.TryFormat(utf16, out utf16len))
                {
                    break;
                }
                buflen *= 2;
            }while(buflen <= 64 * 1024);

            if(utf16len == 0)
            {
                written = 0;
                return false;
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }
        static bool TryFormat(System.Numerics.BigInteger value, Span<byte> destination, out int written, StandardFormat format)
        {
            int buflen = 256;
            Span<char> utf16 = stackalloc char[buflen];
            int utf16len;
            do
            {
                utf16 = stackalloc char[buflen];
                if (value.TryFormat(utf16, out utf16len, format.ToString()))
                {
                    break;
                }
                buflen *= 2;
            }while(buflen <= 64 * 1024);

            if(utf16len == 0)
            {
                written = 0;
                return false;
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }
        static bool TryFormat(System.Version value, Span<byte> destination, out int written, StandardFormat format)
        {
            int buflen = 256;
            Span<char> utf16 = stackalloc char[buflen];
            int utf16len;
            do
            {
                utf16 = stackalloc char[buflen];
                if (value.TryFormat(utf16, out utf16len))
                {
                    break;
                }
                buflen *= 2;
            }while(buflen <= 64 * 1024);

            if(utf16len == 0)
            {
                written = 0;
                return false;
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }
#endif
    }
}