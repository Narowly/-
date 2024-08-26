using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace ProjectService.Helper
{
    public class DateTimeJsonConverter : JsonConverter<DateTime?>
    {
        private readonly string _dateFormat;
        private readonly CultureInfo _cultureInfo;

        public DateTimeJsonConverter(string dateFormat, CultureInfo cultureInfo = null)
        {
            _dateFormat = dateFormat;
            _cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();
            if (!string.IsNullOrWhiteSpace(dateString))
            {
                if (DateTime.TryParse(dateString, out var date)) return date;

                return DateTime.ParseExact(dateString, _dateFormat, _cultureInfo);
            }
            return null;

        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                string formattedDate = value.Value.ToString(_dateFormat, _cultureInfo);
                writer.WriteStringValue(formattedDate);
            }            
        }
    }   
}
