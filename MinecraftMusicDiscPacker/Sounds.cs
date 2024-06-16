using Newtonsoft.Json;

namespace MinecraftMusicDiscPacker;

internal struct SoundSources
{
    public string name;
    public bool stream;
}

internal struct SoundMetadata
{
    public List<SoundSources> sounds = new();

    public SoundMetadata()
    {
    }
}

public class Sounds
{
    private readonly Dictionary<string, SoundMetadata> songLookup = new();

    public string Json => JsonConvert.SerializeObject(songLookup);

    public void AddSong(Song song)
    {
        var sm = new SoundMetadata();
        sm.sounds.Add(new SoundSources
        {
            name = $"records/{song.id}",
            stream = true
        });
        songLookup[$"music_disc.{song.id}"] = sm;
    }
}