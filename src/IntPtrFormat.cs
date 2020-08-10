using System;
using System.Buffers;

using Cysharp.Text;


namespace ZStringFormatExtension
{
    public static partial class FormatExtension
    {
        public static void RegisterIntPtr()
        {
            Utf8ValueStringBuilder.RegisterTryFormat<IntPtr>(TryFormat);
            Utf16ValueStringBuilder.RegisterTryFormat<IntPtr>(TryFormat);
            Utf8ValueStringBuilder.RegisterTryFormat<UIntPtr>(TryFormat);
            Utf16ValueStringBuilder.RegisterTryFormat<UIntPtr>(TryFormat);
        }

        static bool TryFormat(IntPtr value, Span<byte> destination, out int written, StandardFormat _)
        {
            Span<char> utf16 = stackalloc char[64];
            int utf16len;
            if (System.IntPtr.Size == 4)
            {
                if (!value.ToInt32().TryFormat(utf16, out utf16len))
                {
                    written = 0;
                    return false;
                }
            }
            else
            {
                if (!value.ToInt64().TryFormat(utf16, out utf16len))
                {
                    written = 0;
                    return false;
                }
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }

        static bool TryFormat(IntPtr value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _)
        {
            if (System.IntPtr.Size == 4)
                return value.ToInt32().TryFormat(destination, out charsWritten);
            else
                return value.ToInt64().TryFormat(destination, out charsWritten);
        }


        static bool TryFormat(UIntPtr value, Span<byte> destination, out int written, StandardFormat _)
        {
            Span<char> utf16 = stackalloc char[64];
            int utf16len;
            if (System.IntPtr.Size == 4)
            {
                if (!value.ToUInt32().TryFormat(utf16, out utf16len))
                {
                    written = 0;
                    return false;
                }
            }
            else
            {
                if (!value.ToUInt64().TryFormat(utf16, out utf16len))
                {
                    written = 0;
                    return false;
                }
            }

            var utf8len = utf8encoding.GetByteCount(utf16);
            written = utf8encoding.GetBytes(utf16.Slice(0, utf16len), destination);
            return true;
        }

        static bool TryFormat(UIntPtr value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _)
        {
            if (System.IntPtr.Size == 4)
                return value.ToUInt32().TryFormat(destination, out charsWritten);
            else
                return value.ToUInt64().TryFormat(destination, out charsWritten);
        }

    }
}
