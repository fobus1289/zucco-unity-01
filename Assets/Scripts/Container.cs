using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Container
{
    public static PlayerControllerScript SelectPlayer { get; set; }

    public static PlayerInfo PlayerInfo = new PlayerInfo();
    
    public static bool Serialize(this PlayerInfo self,string filename)
    {
        var fullFilename = $"Assets/save/{filename}";
        
        var fileStream = File.OpenWrite(fullFilename);
        Debug.LogWarning(fileStream);
        var formatter = new BinaryFormatter();

        formatter.Serialize(fileStream, self);
        fileStream.Flush();
        fileStream.Close();
        fileStream.Dispose();
        Debug.LogWarning(formatter);
        return true;
    }

    public static PlayerInfo Deserialize(string filename)
    {
        var formatter = new BinaryFormatter();
        var fs = File.Open($"Assets/save/{filename}", FileMode.Open);

        var obj = formatter.Deserialize(fs) as PlayerInfo;
        
        fs.Flush();  
        fs.Close();  
        fs.Dispose();

        return obj;
    }
    
    
}




