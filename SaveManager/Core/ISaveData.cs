namespace SaveManager.Core;

public interface ISaveData
{
    /// <summary>
    ///     Get the major version of the save
    /// </summary>
    public int Major { get; }

    /// <summary>
    ///     Get the minor version of the save
    /// </summary>
    public int Minor { get; }

    /// <summary>
    ///     Name of the save
    /// </summary>
    public string Name { get; set; }
}
