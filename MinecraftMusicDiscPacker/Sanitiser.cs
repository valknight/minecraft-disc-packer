using File = TagLib.File;

namespace MinecraftMusicDiscPacker;

public static class Sanitiser
{
    private static readonly char[] Alpha = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public static string GenerateSongId(File sourceFile)
    {
        return
            $"{SanitiseArbitraryString(sourceFile.Tag.FirstAlbumArtist)}{SanitiseArbitraryString(sourceFile.Tag.Album)}{SanitiseArbitraryString(sourceFile.Tag.Title)}-{(int)sourceFile.Properties.Duration.TotalMilliseconds}"
                .ToLower();
    }

    private static string SanitiseArbitraryString(string source)
    {
        return source.ToLowerInvariant().Where(c => Alpha.Contains(c)).Aggregate("", (current, c) => current + c);
    }
}