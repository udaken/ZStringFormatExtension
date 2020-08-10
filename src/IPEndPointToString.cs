using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ZStringFormatExtension
{
    public static class IPEndPointToString
    {
        static bool TryAppendChar(ref Span<char> destination, char c)
        {
            if (destination.Length > 0)
            {
                destination[0] = c;
                destination = destination.Slice(1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TryFormat(this System.Net.IPEndPoint value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> _ = default)
        {
            charsWritten = 0;
            if ((value.AddressFamily == AddressFamily.InterNetworkV6))
            {
                if (!TryAppendChar(ref destination, '['))
                {
                    return false;
                }
                charsWritten++;
            }

            if (!value.Address.TryFormat(destination, out var addressWritten))
            {
                return false;
            }

            destination = destination.Slice(addressWritten);
            charsWritten += addressWritten;

            if ((value.AddressFamily == AddressFamily.InterNetworkV6))
            {
                if (!TryAppendChar(ref destination, ']'))
                {
                    return false;
                }
                charsWritten++;
            }

            if (!TryAppendChar(ref destination, ':'))
            {
                return false;
            }

            if (!value.Port.TryFormat(destination, out var portWritten))
            {
                return false;
            }

            destination = destination.Slice(portWritten);
            charsWritten += portWritten;
            return true;
        }
    }
}
