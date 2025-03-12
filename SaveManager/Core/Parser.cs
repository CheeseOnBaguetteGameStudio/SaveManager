using System.Reflection;
using SaveManager.Hash;

namespace SaveManager.Core;

/// <summary>
///     Code to parse data to <c>class</c> and <c>class</c> to data
/// </summary>
internal static class Parser
{
    public static void ParseSaveData<T>(T saveData, Dictionary<uint, FieldInfo> hashesTable, uint hash, byte[] data)
    {
        if (!hashesTable.TryGetValue(hash, out var field)) return;
        // saveData.GetType().GetField(field.Name).SetValue(saveData, hash);

        var converted = Converter.ConvertToStructure(field.FieldType, data);

        field.SetValue(saveData, converted);
        Console.WriteLine(converted);
    }

    /// <summary>
    ///     Take a save class and extract field and create a hash of their name with <see cref="Fnv1AHash32" />
    /// </summary>
    /// <param name="saveData">The class to hash</param>
    /// <returns>Hashed field and <see cref="FieldInfo" /></returns>
    /// <seealso cref="Fnv1AHash32" />
    /// <seealso cref="Fnv1AHash32.Hash" />
    internal static Dictionary<uint, FieldInfo> HashFieldData(ISaveData saveData)
    {
        // Get the fields of the class of the save struct
        var members = saveData.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);


        // Return dictionary with FieldInfo and hash
        return members.ToDictionary(member => Fnv1AHash32.Hash(member.Name), member => member);
    }
}
