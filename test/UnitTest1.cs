using Cysharp.Text;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using Xunit;
using ZStringFormatExtension;

namespace test
{

    public class UnitTest1
    {
        public UnitTest1()
        {
            FormatExtension.Register();
        }

        [Fact]
        public void Test1()
        {
            var list = Dns.GetHostAddresses("localhost");
            using (var sb = ZString.CreateUtf8StringBuilder())
            {
                foreach (var a in list)
                {
                    sb.AppendFormat("aaa{0}bbb", IPAddress.Parse("2404:7a81:fe0:3e00:6ee4:daff:fe5b:7b18"));
                    sb.AppendFormat("aaa{0}bbb", BigInteger.Parse("99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"));
                    sb.AppendLine();
                }

            }
        }
    }
}
