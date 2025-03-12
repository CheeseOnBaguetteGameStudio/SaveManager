namespace SaveManager.Core;

internal static class Scanner
{
    /// <summary>
    ///     Scan a save file and extract hash and data
    /// </summary>
    /// <param name="fileStream"><see cref="FileStream" /> of the file to read</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}" /> with the hash and data of the save</returns>
    internal static Dictionary<uint, byte[]> Scan(FileStream fileStream)
    {
        fileStream.Seek(18, SeekOrigin.Begin);
        var scanResults = new Dictionary<uint, byte[]>();

        using var reader = new BinaryReader(fileStream);
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var hash = reader.ReadUInt32();
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);
            scanResults.Add(hash, data);
        }

        reader.Close();
        fileStream.Close();

        return scanResults;
    }
}
