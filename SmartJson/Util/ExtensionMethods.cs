using System;
using System.Linq;

namespace SmartJson.Util {
  internal static class ExtensionMethods {
    internal static bool IsNumeric(this Type value) {
      var numericTypes = new[] { typeof(byte), typeof(decimal), typeof(double),
        typeof(int), typeof(Int16), typeof(Int32), typeof(Int64), typeof(sbyte),
        typeof(Single), typeof(uint), typeof(UInt16), typeof(UInt32), typeof(UInt64)};

      return numericTypes.Contains(value);
    }

    internal static string Repeat(this string value, int count) {
      var result = string.Empty;

      for (int i = 0; i < count; i++) {
        result += value;
      }

      return result;
    }
  }
}
