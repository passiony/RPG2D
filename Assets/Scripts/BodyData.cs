using System;

public enum EBodyPart
{
    Hair,
    Skin,
    Top,
    Down,
}

[Serializable]
public class ClothData
{
    public int Index;
    public int Color;
}

[Serializable]
public class BodyData
{
    public string Name;
    public int Age;
    public string From;

    public ClothData[] Cloths = new ClothData[4];
}