using System.Text;

namespace SaveManager.Hash;

public static class Fnv1AHash32
{
    private const uint FnvOffsetBasis = 2166136261;
    private const uint FnvPrime = 16777619;

    /// <summary>
    ///     Simple FNV 1a hash algorithm
    /// </summary>
    /// <param name="data">String to hash</param>
    /// <returns>Hashed string</returns>
    public static uint Hash(string data)
    {
        var hash = FnvOffsetBasis;
        foreach (var b in Encoding.UTF8.GetBytes(data))
        {
            hash ^= b;
            hash *= FnvPrime;
        }

        return hash;
    }
}
