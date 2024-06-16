using File = TagLib.File;

namespace MinecraftMusicDiscPacker;

public class Song
{
    public string artist;
    public File diskMetadata;
    public string id;
    public string name;
    public PackData pack;
    public string pathOnDisk;

    public Song(string diskPath, PackData packData)
    {
        diskMetadata = File.Create(diskPath);
        id = Sanitiser.GenerateSongId(diskMetadata);
        name = diskMetadata.Tag.Album != ""
            ? $"{diskMetadata.Tag.Album} - {diskMetadata.Tag.Title}"
            : diskMetadata.Tag.Title;
        artist = diskMetadata.Tag.FirstAlbumArtist;
        pathOnDisk = diskPath;
        pack = packData;
    }

    public string langTranslationId => $"jukebox_song.{pack.packId}.{id}";
    public string langTranslationValue => $"{artist} - {name}";
}