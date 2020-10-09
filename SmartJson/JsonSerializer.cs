using SmartJson.Configuration;
using SmartJson.Util;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JsonParser {
  public class JsonSerializer {

    #region - Instance Variables -

    private object serializable;
    private string serialized;

    private int currentLevel = 1;

    private SmartJsonConfigurationOptions options;

    #endregion


    #region - Constructor -

    public JsonSerializer() {
      options = new SmartJsonConfigurationOptions();
    }

    public JsonSerializer(SmartJsonConfigurationOptions options) {
      this.options = options ?? new SmartJsonConfigurationOptions();
    }

    #endregion

    #region - Serialization -

    public string Serialize(object serializable) {
      this.serializable = serializable;

      SerializeProperties();

      return serialized;
    }

    public void SerializeToFile(object serializable, string filePath) {
      this.serializable = serializable;

      SerializeProperties();

      File.WriteAllText(filePath, serialized);
    }

    #endregion

    #region - Private Methods - 

    #region - Property Handling -

    private void SerializeProperties() {
      serialized = SpecialCharacters.OpeningCurlyBracket.ToString() + Indent();

      var type = serializable.GetType();
      var properties = type.GetProperties();

      IterateProperties(properties);

      serialized += SpecialCharacters.ClosingCurlyBracket;
    }

    private void IterateProperties(PropertyInfo[] properties, object parent = null) {
      for (int i = 0; i < properties.Length; i++) {
        var property = properties[i];
        WriteProperty(property, parent);

        if (i != properties.Length - 1) {
          serialized += SpecialCharacters.Comma;
        } else {
          if (parent != null) {
            DecreaseLevelIfPrettyPrint();
          }
        }

        serialized += Indent();
      }
    }

    private void WriteProperty(PropertyInfo property, object parent = null) {
      var name = property.Name;
      var value = property.GetValue(parent ?? serializable);

      WriteName(name);
      WriteValue(value);
    }

    private void WriteName(string text) {
      serialized += SpecialCharacters.QuotationMark;
      serialized += text;
      serialized += SpecialCharacters.QuotationMark;
      serialized += SpecialCharacters.Colon;
      serialized += SpecialCharacters.Space;
    }

    private void WriteValue(object value) {
      var valueType = value?.GetType();

      if (valueType == typeof(string)) {
        WriteValueWithQuotationMarks(value);
      } else if (valueType.IsNumeric()) {
        WriteValueWithOutQuotationMarks(value);
      } else if (valueType != null && typeof(IEnumerable).IsAssignableFrom(valueType)) {
        WriteArray(value);
      } else if (valueType == typeof(bool)) {
        WriteValueWithOutQuotationMarks(value.ToString().ToLower());
      } else if (valueType == null) {
        WriteValueWithOutQuotationMarks("null");
      } else {
        WriteObject(value);
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
      IncreaseLevelIfPrettyPrint();

      var valueType = value.GetType();
      serialized += SpecialCharacters.OpeningCurlyBracket;
      serialized += Indent();

      var properties = valueType.GetProperties();

      IterateProperties(properties, value);

      DecreaseLevelIfPrettyPrint();
      serialized += SpecialCharacters.ClosingCurlyBracket;
    }

    private void WriteArray(object value) {
      IncreaseLevelIfPrettyPrint();
      var array = (value as IEnumerable).Cast<object>();
      serialized += SpecialCharacters.OpeningSquareBracket;
      serialized += Indent();

      for (int i = 0; i < array.Count(); i++) {
        WriteValue(array.ElementAt(i));

        if (i != array.Count() - 1) {
          serialized += SpecialCharacters.Comma;
        }
      }

      serialized += SpecialCharacters.ClosingSquareBracket;
    }

    #endregion

    #region - Pretty Print -

    private void IncreaseLevelIfPrettyPrint() {
      if (options.PrettyPrint) {
        ++currentLevel;
      }
    }

    private void DecreaseLevelIfPrettyPrint() {
      if (options.PrettyPrint) {
        --currentLevel;
      }
    }

    private string Indent() {
      return options.PrettyPrint ? EscapeCharacters.Break + EscapeCharacters.Tab.Repeat(currentLevel) : "";
    }

    #endregion

    #endregion
  }
}
