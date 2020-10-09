using System;
using System.Linq;

namespace SmartJson.Util {
  public static class ExtensionMethods {
    public static bool IsNumeric(this Type value) {
      var numericTypes = new[] { typeof(byte), typeof(decimal), typeof(double),
        typeof(int), typeof(Int16), typeof(Int32), typeof(Int64), typeof(sbyte),
        typeof(Single), typeof(uint), typeof(UInt16), typeof(UInt32), typeof(UInt64)};

      return numericTypes.Contains(value);
    }
  }
}
