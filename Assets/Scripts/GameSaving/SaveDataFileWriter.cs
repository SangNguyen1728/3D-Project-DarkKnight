using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Unity.Collections;
using System.Linq.Expressions;

public class SaveDataFileWriter
{
    public string saveDataDirectotyPath = "";
    public string saveFileName = "";

    // check to see to if one of this character slot already exists(max: 10)
    public bool CheckToSeeFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectotyPath, saveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // used to delete character save file
    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectotyPath, saveFileName));
    }

    // use to create a save file upon starting a new game
    public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
    {
        // make a path to save the file (location on the machine)
        string savePath = Path.Combine(saveDataDirectotyPath, saveFileName);


        // try: 
        //Note: Reading game data does not error if the file does not exist.
        //Note: Prevents calculation errors in logic games
        //Note: Free up memory, avoid memory leak
        //Do not overuse try-catch: As it can degrade performance if called repeatedly in Update() or loops.

        try
        {
            // create the directoty the file will be written to, if it does not already exist
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("create save file, at save path:" + savePath);

            // serialize c# game data object into json
            string dataToStore = JsonUtility.ToJson(characterData, true);

            // write the file to our system
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("LỖI KHI CỐ LƯU DỮ LIỆU NHÂN VẬT, GAME KHÔNG LƯU" + savePath + "\n" + ex);
        }
    }

    // use to load a save file upon loading a previous game
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = null;
        // make a path to load the file (location on the machine)
        string loadPath = Path.Combine(saveDataDirectotyPath, saveFileName);

        if(File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize the data from json back to unity 
                characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            }
            catch (Exception ex)
            {
               
            }
            
        }
        
        return characterData;
    }
}
