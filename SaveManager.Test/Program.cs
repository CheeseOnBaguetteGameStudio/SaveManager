namespace SaveManager.Test;

internal class Program
{
    private static void Main(string[] args)
    {
        var save = new MySave(1, 5, "Mat");
        var saveManager = new SaveManager(save);
        save.Abcdefghijklmopqrstuvwxyz = "Mato";

        // for (var i = 0; i < ioSave.MembersNameList.Count; i++)
        // {
        //     // Console.WriteLine(ioSave.MembersNameList[i]);
        //     Console.WriteLine((byte)ioSave.MembersHashList[i]);
        // }

        // Console.WriteLine("Hello, World!");

        // Console.WriteLine(Lexer.Tokenize("test.yml"));

        saveManager.SaveToDisk("test.dat");
        saveManager.LoadSaveData("test.dat");
        // ioSave.SaveToDisk("test.dat");

        Console.WriteLine(save.Abcdefghijklmopqrstuvwxyz);
        Console.WriteLine(Path.GetFullPath("test.dat"));
    }
}
