using Newtonsoft.Json;

namespace MinecraftMusicDiscPacker;

public class Pack
{
    public List<Song> songs = [];
    public Dictionary<string, string> translationLang => LangTranslater.GenerateFromSongList(songs);

    private void GenerateMcMeta(string root, int packFormat, string description)
    {
        var packMeta = new McMeta
        {
            pack_format = packFormat,
            description = description
        };
        using var outputFile = new StreamWriter(Path.Combine(root, "pack.mcmeta"));
        outputFile.Write(packMeta.JsonRep);
    }

    private void BuildRp(string rpPath)
    {
        Console.WriteLine("Generating metadata...");
        GenerateMcMeta(rpPath, 34, "CustomServerMusicPack - RP");
        Console.WriteLine("Setting up directories...");
        Directory.CreateDirectory($"{rpPath}/assets/minecraft/lang");
        Directory.CreateDirectory($"{rpPath}/assets/minecraft/models");
        Directory.CreateDirectory($"{rpPath}/assets/minecraft/sounds/records");
        Directory.CreateDirectory($"{rpPath}/assets/minecraft/textures");
        Console.WriteLine("Generating lang...");
        using var langFile = new StreamWriter($"{rpPath}/assets/minecraft/lang/en_us.json");
        langFile.Write(JsonConvert.SerializeObject(translationLang));
        Console.WriteLine("Copying songs...");
        foreach (var song in songs)
        {
            var dest = $"{rpPath}/assets/minecraft/sounds/records/{song.id}.ogg";
            Console.WriteLine($"[{song.artist} - {song.name}] {song.pathOnDisk} -> {dest}");
            File.Copy(song.pathOnDisk, dest, true);
        }

        Console.WriteLine("Generating sound list...");
        var sounds = new Sounds();
        foreach (var song in songs) sounds.AddSong(song);
        using var soundFile = new StreamWriter($"{rpPath}/assets/minecraft/sounds.json");
        soundFile.Write(sounds.Json);
    }

    private void BuildDp(string dpPath)
    {
        Console.WriteLine("Generating metadata...");
        GenerateMcMeta(dpPath, 34, "CustomServerMusicPack - DP");
        Console.WriteLine("Generating record data...");
        Directory.CreateDirectory($"{dpPath}/data/discpacker/jukebox_song");
        var songCount = 0;
        foreach (var song in songs)
        {
            var songData = new RecordData(song, songCount % 15 + 1);
            songCount += 1;
            using var songDataFile = new StreamWriter($"{dpPath}/data/discpacker/jukebox_song/{song.id}.json");
            songDataFile.Write(songData.Json);
        }

        Console.WriteLine("Generating functions...");
        Directory.CreateDirectory($"{dpPath}/data/discpacker/function");
        foreach (var song in songs)
        {
            using var functionFile = new StreamWriter($"{dpPath}/data/discpacker/function/{song.id}.mcfunction");
            functionFile.Write(
                $"give @s minecraft:music_disc_11[minecraft:jukebox_playable={{song:'discpacker:{song.id}'}}]");
        }

        Directory.CreateDirectory($"{dpPath}/data/discpacker/recipe");
        // TODO: Recipe generation!
    }

    public void Build(string rpDir, string dpDir)
    {
        try
        {
            Directory.Delete(rpDir, true);
        }
        catch (DirectoryNotFoundException e)
        {
        }

        try
        {
            Directory.Delete(dpDir, true);
        }
        catch (DirectoryNotFoundException e)
        {
        }

        Directory.CreateDirectory(rpDir);
        Directory.CreateDirectory(dpDir);
        BuildRp(rpDir);
        BuildDp(dpDir);
    }
}