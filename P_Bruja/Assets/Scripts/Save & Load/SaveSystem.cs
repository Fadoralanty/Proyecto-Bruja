using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveGameData(GameData gameData)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "GameData.json");
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);
            string dataToStore = JsonUtility.ToJson(gameData, true);
            using (FileStream stream=new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer= new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }    

    public static GameData LoadGameData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "GameData.json");
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream=new FileStream(fullPath,FileMode.Open))
                {
                    using (StreamReader reader=new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error when loading data from: " + fullPath +"\n+" + e);
            }
        }

        return loadedData;
    }

}
