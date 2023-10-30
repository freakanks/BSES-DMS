using System.Text.Json;
using System.Text.Json.Serialization;

namespace BSES.DocumentManagementSystem.Common
{
    public class InterfaceConverter<TImplementation, TInterface> : JsonConverter<TInterface>
    where TImplementation : class, TInterface
    {
        public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => JsonSerializer.Deserialize<TImplementation>(ref reader, options);

        public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
        {
        }
    }
    public class InterfaceConverterFactory<TImplementation, TInterface> : JsonConverterFactory where TImplementation : class, TInterface
    {
        public Type ImplementationType { get; }
        public Type InterfaceType { get; }

        public InterfaceConverterFactory()
        {
            ImplementationType = typeof(TImplementation);
            InterfaceType = typeof(TInterface);
        }

        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == InterfaceType;

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(InterfaceConverter<,>).MakeGenericType(ImplementationType, InterfaceType);
            return Activator.CreateInstance(converterType) as JsonConverter;
        }
    }
    public static class SerializationHelper
    {

        public static string Serialize<T>(this T instance)
        {
            if (instance == null) return String.Empty;
            return JsonSerializer.Serialize(instance, typeof(T), new JsonSerializerOptions() { IgnoreNullValues = true });
        }
        public static T Deserialize<T, TInterface>(this string json) where T : class, TInterface
        {
            // Create deserializer options with interface converter factory.
            var deserializerOptions = new JsonSerializerOptions
            {
                Converters = { new InterfaceConverterFactory<T, TInterface>() }
            };

            if (!String.IsNullOrEmpty(json)) return JsonSerializer.Deserialize<T>(json, deserializerOptions);
            return default;
        }
        public static T Deserialize<T>(this string json) where T : class
        {
            if (!String.IsNullOrEmpty(json)) return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return default;
        }
    }
}
