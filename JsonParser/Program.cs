using System;

namespace JsonParser {
  class Program {
    static void Main(string[] args) {
      var test = new Test {
        Id = 1,
        Name = "DMA",
        HasPhone = true,
        Nullable = null
      };

      var serialized = new JsonSerializer().Serialize(test);

      Console.WriteLine(serialized);
    }
  }
}
