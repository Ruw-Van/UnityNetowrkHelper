
using System;
using UnityEngine;

[Serializable]
public class Character
{
    public string Name;
    public string CharaName;
    public int Hp;
    public float Mp;

    public Character(string name, string charaName, int hp, float mp)
    {
        Name = name;
        CharaName = charaName;
        Hp = hp;
        Mp = mp;
    }

    public override string ToString()
    {
        return $"Character: {Name} ({CharaName}), HP: {Hp}, MP: {Mp}";
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static Character FromJson(string json)
    {
        return JsonUtility.FromJson<Character>(json);
    }
}