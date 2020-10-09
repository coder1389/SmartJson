using SmartJson.Util;
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
      var properties = type.GetProperties();

      IterateProperties(properties);

      serialized += SpecialCharacters.ClosingCurlyBracket;
    }

    private void IterateProperties(PropertyInfo[] properties) {
      for (int i = 0; i < properties.Length; i++) {
        var property = properties[i];
        WriteProperty(property);

        if (i != properties.Length - 1) {
          serialized += SpecialCharacters.Comma;
        }
      }
    }

    private void WriteProperty(PropertyInfo property) {
      var name = property.Name;
      var value = property.GetValue(serializable);

      WriteName(name);
      WriteValue(value);
    }

    private void WriteName(string text) {
      serialized += SpecialCharacters.QuotationMark;
      serialized += text;
      serialized += SpecialCharacters.QuotationMark;
      serialized += SpecialCharacters.Colon;
    }

    private void WriteValue(object value) {
      var valueType = value?.GetType();

      if (valueType == typeof(string)) {
        WriteValueWithQuotationMarks(value);
      } else if (valueType.IsNumeric()) {
        WriteValueWithOutQuotationMarks(value);
      } else if (valueType == typeof(bool)) {
        WriteValueWithOutQuotationMarks(value.ToString().ToLower());
      } else if (valueType == typeof(object)) {
        WriteObject(value);
      } else if (valueType == null) {
        WriteValueWithOutQuotationMarks("null");
      }
    }

    private void WriteValueWithQuotationMarks(object value) {
      serialized += SpecialCharacters.QuotationMark;
      serialized += value;
      serialized += SpecialCharacters.QuotationMark;
    }

    private void WriteValueWithOutQuotationMarks(object value) {
      serialized += value;
    }

    private void WriteObject(object value) {
      var valueType = value.GetType();
      serialized += SpecialCharacters.OpenCurlyBracket;

      var properties = valueType.GetProperties();

      IterateProperties(properties);

      serialized += SpecialCharacters.ClosingCurlyBracket;
    }

    #endregion
  }
}
