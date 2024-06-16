using Newtonsoft.Json;

namespace MinecraftMusicDiscPacker;

public struct McMeta
{
    public int pack_format;
    public string description = "";

    public McMeta()
    {
        pack_format = 0;
    }

    [JsonIgnore] public string JsonRep => $"{{\"pack\":{JsonConvert.SerializeObject(this)}}}";
}