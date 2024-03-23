using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CountryNameJson
{
    [JsonProperty("elements")]
    public List<ElementName> elements { get; set; }

}

[System.Serializable]
public class ElementName
{
    [JsonProperty("id")]
    public int id { get; set; }
    [JsonProperty("tags")]
    public Tags tags { get; set; }
}

[System.Serializable]
public class Tags
{
    [JsonProperty("name:ko")]
    public string name_ko { get; set; }
}

[System.Serializable]
public class ElementGeom
{
    [JsonProperty("coodinate")]
    public List<loc> locs { get; set; }
    [JsonProperty("center")]
    public centerLoc bounds { get; set; }
}

[System.Serializable]
public class Square
{
    public float minlat { get; set; }
    public float minlon { get; set; }
    public float maxlat { get; set; }
    public float maxlon { get; set; }
}

[System.Serializable]
public class Way
{
    [JsonProperty("role")]
    public string role { get; set; }
    [JsonProperty("geometry")]
    public List<loc> geometry { get; set; }

}

[System.Serializable]
public class loc
{
    [JsonProperty("x")]
    public float x { get; set; }
    [JsonProperty("y")]
    public float y { get; set; }
}

[System.Serializable]
public class centerLoc
{
    [JsonProperty("cx")]
    public float x { get; set; }
    [JsonProperty("cy")]
    public float y { get; set; }
}