using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class saveGame  {

    public static List<Game> savedGames = new List<Game>();

    public static void Save()
    {
        saveGame.savedGames.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/saves.gd"); //you can call it anything you want
        bf.Serialize(file, saveGame.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saves.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saves.gd", FileMode.Open);
            saveGame.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }

	
	
}
