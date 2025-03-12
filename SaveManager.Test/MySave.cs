using SaveManager.Core;

namespace SaveManager.Test;

public class MySave(int major, int minor, string name) : ISaveData
{
    public int Major { get; } = major;
    public int Minor { get; } = minor;

    public string Name { get; set; } = name;
    public string PotatoChipsUltimate = "Potato";
    public string Abcdefghijklmopqrstuvwxyz = "Abcdefghijklmopqrstuvwxyz";

    public bool IsEmpty = false;
    public bool IsValid = true;
}
