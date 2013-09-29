﻿using System;
using System.Text;

namespace UmbracoTemplate.Common.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] data)
        {
            return SafeConvert.ToHexString(data);
        }

        public static string ToBase64String(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static string ToUtf8String(this byte[] data)
        {
            return new UTF8Encoding().GetString(data);
        }

        public static byte[] ToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] ToByteArrayUtf8(this string str)
        {
            return new UTF8Encoding().GetBytes(str);
        }

        public static byte[] ToByteArrayBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}