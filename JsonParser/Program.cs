﻿using System;
using System.Collections.Generic;

namespace JsonParser {
  class Program {
    static void Main(string[] args) {
      var test = new Test {
        Id = 1,
        Name = "DMA",
        HasPhone = true,
        Nullable = null,
        Test2 = new Test2 {
          Id2 = 2,
          Name2 = "Malinovic"
        },
        Test2s = new List<Test2> {
           new Test2 {
            Id2 = 2,
            Name2 = "bla"
          },
           new Test2 {
            Id2 = 2,
            Name2 = "foo"
          }
        },
        Names = new List<string> {
          "Pablo",
          "escobar"
        }
      };

      var serialized = new JsonSerializer().Serialize(test);

      Console.WriteLine(serialized);
    }
  }
}
