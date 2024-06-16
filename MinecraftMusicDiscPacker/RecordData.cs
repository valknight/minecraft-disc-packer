using Newtonsoft.Json;

namespace MinecraftMusicDiscPacker;

internal struct Description
{
    public string translate;
}

internal struct SoundEvent
{
    public string sound_id;
}

public class RecordData
{
    [JsonProperty] private int comparator_output;
    [JsonProperty] private Description description;
    [JsonProperty] private int length_in_seconds;
    [JsonProperty] private SoundEvent sound_event;

    public RecordData(Song song, int compOutput)
    {
        comparator_output = compOutput;
        description.translate = song.langTranslationId;
        length_in_seconds = (int)song.diskMetadata.Properties.Duration.TotalSeconds;
        sound_event.sound_id = $"minecraft:music_disc.{song.id}";
    }

    [JsonIgnore] public string Json => JsonConvert.SerializeObject(this);
}