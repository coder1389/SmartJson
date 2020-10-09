using System;

namespace JsonParser {
  class Program {
    static void Main(string[] args) {
      var serialized = new JsonSerializer().Serialize(new Test { Id = 1, Name = "DMA" });

      Console.WriteLine(serialized);
    }
  }
}
