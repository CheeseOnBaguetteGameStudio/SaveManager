using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SaveManager.Core;

internal class IO
{
    private readonly byte[] _emptyBytes = new byte[4];
    private readonly Encoding _encoding = Encoding.ASCII;

    private readonly Dictionary<uint, FieldInfo> _hashes;
    private readonly ISaveData _saveData;

    public IO(ISaveData saveData)
    {
        _saveData = saveData;
        _hashes = Parser.HashFieldData(_saveData);
    }

    /// <summary>
    ///     Save the <see cref="ISaveData" /> class to disk
    /// </summary>
    /// <param name="outputFilePath">Where to save</param>
    /// <exception cref="NotSupportedException">The variable type can't be saved</exception>
    public void SaveToDisk(string outputFilePath)
    {
        using var writer = new FileStream(outputFilePath, FileMode.Create);

        // Header
        writer.Write(_encoding.GetBytes("SaveData"));
        writer.Write(_emptyBytes);
        writer.Write(BitConverter.GetBytes(_hashes.Count));
        writer.WriteByte((byte)_saveData.Major);
        writer.WriteByte((byte)_saveData.Minor);

        // Write the variable + value_size + value
        foreach (var (hash, member) in _hashes)
        {
            writer.Write(BitConverter.GetBytes(hash));
            Console.WriteLine($"{hash} - {member} | {BitConverter.ToString(BitConverter.GetBytes(hash))}");

            var value = member.GetValue(_saveData);
            // Avoid null reference
            Debug.Assert(value is not null);
            // if (value is null)
            //     throw new MissingMemberException($"No value found for member {member.Name}");

            var bytes = value switch
            {
                int intVal => BitConverter.GetBytes(intVal),
                short shortVal => BitConverter.GetBytes(shortVal),
                float floatVal => BitConverter.GetBytes(floatVal),
                string stringVal => Encoding.UTF8.GetBytes(stringVal),
                bool boolVal => BitConverter.GetBytes(boolVal),
                _ => throw new NotSupportedException($"Type {value.GetType()} is not supported")
            };

            // Ensure little-endian
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);

            writer.Write(BitConverter.GetBytes(bytes.Length));
            writer.Write(bytes);
        }

        writer.Close();
    }

    /// <summary>
    ///     Read the save from the disk, write them directly into <see cref="_saveData" />
    /// </summary>
    /// <param name="inputFilePath">Path to the save data</param>
    public void ReadFromDisk(string inputFilePath)
    {
        using var reader = new FileStream(inputFilePath, FileMode.Open);
        foreach (var (hash, bytes) in Scanner.Scan(reader))
        {
            Console.WriteLine((hash, BitConverter.ToString(bytes)));
            Parser.ParseSaveData(_saveData, _hashes, hash, bytes);
        }
    }
}
