using System.Collections.Generic;

namespace JsonParser {
  public class Test {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool HasPhone { get; set; }
    public string Nullable { get; set; }
    public Test2 Test2 { get; set; }
    public IList<Test2> Test2s { get; set; }
    public IList<string> Names { get; set; }
  }

  public class Test2 {
    public int Id2 { get; set; }
    public string Name2 { get; set; }
  }
}
