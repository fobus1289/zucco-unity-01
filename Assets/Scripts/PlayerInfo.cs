using System;
using System.Numerics;
using UnityEngine.Serialization;

[Serializable] 
public class PlayerInfo
{

    public const string Resource = "a/b/c/c/weapons/";
    
    public string fileName = string.Empty;

    public float maxHp;
    
    public string name;
    public float x;
    public float y;
    public float z;
    
    //AxeBasic2_1 ,AxeBasic2_2,AxeBasic2_3,...
    public string[] weapons;

    public override string ToString()
    {
        return $"filename={fileName}\name={name}\nmaxHp={maxHp}\nposition=[x:{x},y:{y},z:{z}]";
    }
}
