namespace MinecraftMusicDiscPacker;

internal class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Correct usage: mcpacker [sourceDir] [outputDir]");
            return 1;
        }

        var pack = new Pack();

        var sourceDir = args[0];
        var rpDir = $"{args[1]}/ResourcePack/";
        var dpDir = $"{args[1]}/DataPack/";
        var files = Directory.GetFiles(sourceDir);

        // Filter down to just ogg vorbis
        var oggPaths = files.Where(file => file.EndsWith(".ogg")).ToList();

        // Setup the pack data
        var packData = new PackData
        {
            packId = "servermusicpack"
        };

        // Add all songs to the pack
        foreach (var file in oggPaths) pack.songs.Add(new Song(file, packData));

        // Execute the pack's build method
        pack.Build(rpDir, dpDir);

        Console.WriteLine("packing complete");
        return 0;
    }
}