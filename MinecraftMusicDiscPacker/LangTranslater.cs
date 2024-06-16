namespace MinecraftMusicDiscPacker;

public static class LangTranslater
{
    public static Dictionary<string, string> GenerateFromSongList(List<Song> songs)
    {
        var d = new Dictionary<string, string>();
        foreach (var song in songs) d[song.langTranslationId] = song.langTranslationValue;
        return d;
    }
}