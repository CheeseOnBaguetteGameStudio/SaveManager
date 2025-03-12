using SaveManager.Core;

namespace SaveManager;

public class SaveManager(ISaveData saveData)
{
    private readonly IO _io = new(saveData);

    public ISaveData SaveData { get; private set; } = saveData;

    /// <inheritdoc cref="IO.ReadFromDisk" />
    public void LoadSaveData(string inputFilePath)
    {
        _io.ReadFromDisk(inputFilePath);
    }

    /// <inheritdoc cref="IO.SaveToDisk" />
    public void SaveToDisk(string outputFilePath)
    {
        _io.SaveToDisk(outputFilePath);
    }

    /// <summary>
    ///     Update the <see cref="SaveData" /> used
    /// </summary>
    /// <param name="saveData">New save data class to use</param>
    public void UpdateSaveData(ISaveData saveData)
    {
        SaveData = saveData;
    }
}
