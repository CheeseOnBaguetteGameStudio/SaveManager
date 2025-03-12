using System.Text;

namespace SaveManager.Core;

public static class Converter
{
    /// <summary>
    ///     Convert the data in the type specified
    /// </summary>
    /// <param name="type">Type to convert to</param>
    /// <param name="data">Data to convert</param>
    /// <returns>Converted data</returns>
    /// <exception cref="NotSupportedException">Type isn't supported</exception>
    public static object ConvertToStructure(Type type, byte[] data)
    {
        object? converted = type switch
        {
            not null when type == typeof(string) => Encoding.UTF8.GetString(data),
            not null when type == typeof(int) => BitConverter.ToInt32(data, 0),
            not null when type == typeof(float) => BitConverter.ToSingle(data, 0),
            not null when type == typeof(bool) => BitConverter.ToBoolean(data, 0),
            _ => throw new NotSupportedException($"Type {type} is not supported")
        };

        return converted;
    }

    /// <summary>
    ///     Convert the data in bytes
    /// </summary>
    /// <param name="data">Data to convert</param>
    /// <returns>Converted data</returns>
    /// <exception cref="NotSupportedException">Data not in a supported format</exception>
    public static byte[] ConvertToByte(object data)
    {
        var bytes = data switch
        {
            int intVal => BitConverter.GetBytes(intVal),
            short shortVal => BitConverter.GetBytes(shortVal),
            float floatVal => BitConverter.GetBytes(floatVal),
            string stringVal => Encoding.UTF8.GetBytes(stringVal),
            bool boolVal => BitConverter.GetBytes(boolVal),
            _ => throw new NotSupportedException($"Type {data.GetType()} is not supported")
        };

        return bytes;
    }
}
