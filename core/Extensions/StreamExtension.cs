using System;
using System.Collections.Generic;
using System.Net;

using hello.transaction.core.Middleware;
using System.IO;
using System.Text;

namespace hello.transaction.core.Extensions
{
    public static class StreamExtension
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            //string base64 = Convert.ToBase64String(bytes);
            return bytes;
        }
    }
}