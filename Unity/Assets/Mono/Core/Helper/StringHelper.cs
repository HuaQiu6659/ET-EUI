using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ET
{
	public static class StringHelper
	{
		public static IEnumerable<byte> ToBytes(this string str)
		{
			byte[] byteArray = Encoding.Default.GetBytes(str);
			return byteArray;
		}

		public static byte[] ToByteArray(this string str)
		{
			byte[] byteArray = Encoding.Default.GetBytes(str);
			return byteArray;
		}

	    public static byte[] ToUtf8(this string str)
	    {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return byteArray;
        }

		public static byte[] HexToBytes(this string hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
			}

			var hexAsBytes = new byte[hexString.Length / 2];
			for (int index = 0; index < hexAsBytes.Length; index++)
			{
				string byteValue = "";
				byteValue += hexString[index * 2];
				byteValue += hexString[index * 2 + 1];
				hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return hexAsBytes;
		}

		public static string Fmt(this string text, params object[] args)
		{
			return string.Format(text, args);
		}

		public static string ListToString<T>(this List<T> list)
		{
			StringBuilder sb = new StringBuilder();
			foreach (T t in list)
			{
				sb.Append(t);
				sb.Append(",");
			}
			return sb.ToString();
		}
		
		public static string ArrayToString<T>(this T[] args)
		{
			if (args == null)
			{
				return "";
			}

			string argStr = " [";
			for (int arrIndex = 0; arrIndex < args.Length; arrIndex++)
			{
				argStr += args[arrIndex];
				if (arrIndex != args.Length - 1)
				{
					argStr += ", ";
				}
			}

			argStr += "]";
			return argStr;
		}
        
		public static string ArrayToString<T>(this T[] args, int index, int count)
		{
			if (args == null)
			{
				return "";
			}

			string argStr = " [";
			for (int arrIndex = index; arrIndex < count + index; arrIndex++)
			{
				argStr += args[arrIndex];
				if (arrIndex != args.Length - 1)
				{
					argStr += ", ";
				}
			}

			argStr += "]";
			return argStr;
        }
        private static StringBuilder stringBuilder = new StringBuilder();

        public static StringBuilder GetShareStringBuilder()
        {
            stringBuilder.Clear();
            return stringBuilder;
        }

        public static string Format(string src, params object[] args)
        {
            stringBuilder.Clear();
            stringBuilder.AppendFormat(src, args);
            return stringBuilder.ToString();
        }

        public static string Concat(string head, string append, params string[] strArray)
        {
            stringBuilder.Clear();
            stringBuilder.Append(head);
            stringBuilder.Append(append);
            if (strArray.Length > 0)
                for (int i = 0; i < strArray.Length; i++)
                    stringBuilder.Append(strArray[i]);
            return stringBuilder.ToString();
        }

        public static string ConcatLines(string head, params string[] strArray)
        {
            if (strArray.Length < 1)
                return head;

            stringBuilder.Clear();
            stringBuilder.Append(head);
            for (int i = 0; i < strArray.Length; i++)
            {
                stringBuilder.Append('\n');
                stringBuilder.Append(strArray[i]);
            }
            return stringBuilder.ToString();
        }

		public static bool IsEmail(string input) =>
			//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
			Regex.IsMatch(input, "^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");

		/// <summary>
		/// 含有除字母 数字以外的特殊符号
		/// </summary>
		/// <param name="input"></param>
		/// <returns>True：含有</returns>
		public static bool HasSymbol(string input) => !Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
	}
}