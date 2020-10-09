using SmartJson.Util;
using System.Linq;
using System.Reflection;

namespace JsonParser {
  public class JsonSerializer {

    #region - Instance Variables -

    private object serializable;
    private string serialized;

    #endregion

    #region - Serialization

    public string Serialize(object serializable) {
      this.serializable = serializable;

      SerializeProperties();

      return serialized;
    }

    private void SerializeProperties() {
      serialized = SpecialCharacters.OpenCurlyBracket.ToString();

      var type = serializable.GetType();
      var properties = type.GetProperties().ToList();

      foreach (var property in properties) {
        WriteProperty(property);

        if (properties.IndexOf(property) != properties.Count) {
          serialized += SpecialCharacters.Comma;
        }
      }

      serialized += SpecialCharacters.ClosingCurlyBracket;
    }

    private void WriteProperty(PropertyInfo property) {
      var name = property.Name;
      var value = property.GetValue(serializable);

      WriteName(name);
    }

    private void WriteName(string text) {
      serialized += SpecialCharacters.QuotationMark;
      serialized += text;
      serialized += SpecialCharacters.QuotationMark;
      serialized += SpecialCharacters.Colon;
    }

    private void WriteValue(object value) {

    }

    #endregion
  }
}
