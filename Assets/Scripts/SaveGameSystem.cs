using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveGameSystem 
{
    public static bool SaveGame(SaveData saveGame, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
                Debug.Log("Game Saved");
            } catch (Exception)
            {
                return false;
            }
        }
        return true;
    }    

    public static SaveData LoadGame(string name)
    {
        if (!DoesSaveGameExist(name))
        {
            return new SaveData();
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
        {
            try
            {
                SaveData myData = formatter.Deserialize(stream) as SaveData;
                return myData;
            }
            catch (Exception)
            {
                SaveData save = new SaveData();
                return save;
            }
        }
    }

    public static bool DeleteSaveGame(string name)
    {
        try
        {
            File.Delete(GetSavePath(name));
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public static bool DoesSaveGameExist(string name)
    {
        return File.Exists(GetSavePath(name));
    }

    public static string GetSavePath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".sav");
    }
}
