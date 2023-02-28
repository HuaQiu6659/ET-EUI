﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ET
{
	public static class MD5Helper
	{
        /// <summary>
        /// 获取文件MD5码
        /// </summary>
		public static string FileMD5(string filePath)
		{
			byte[] retVal;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
	            MD5 md5 = MD5.Create();
				retVal = md5.ComputeHash(file);
			}
			return retVal.ToHex("x2");
		}

        /// <summary>
        /// 字符串转换成MD5码
        /// </summary>
        public static string StringMD5(string content)
        {
            MD5 md5 = MD5.Create();
            byte[] result = Encoding.Default.GetBytes(content);
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", default);
        }
    }
}
