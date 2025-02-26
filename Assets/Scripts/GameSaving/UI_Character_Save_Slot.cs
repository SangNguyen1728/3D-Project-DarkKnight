using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class UI_Character_Save_Slot : MonoBehaviour
{
    SaveDataFileWriter saveFileWriter;

    [Header("Game Slot")]
    public CharacterSlot characterSlot;

    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timePlayed;

    private void OnEnable()
    {
        LoadSaveSlots();
    }

    private void LoadSaveSlots()
    {
        saveFileWriter = new SaveDataFileWriter();
        saveFileWriter.saveDataDirectotyPath = Application.persistentDataPath;
        
        // slot 01
        if(characterSlot == CharacterSlot.CharacterSlot_01)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if(saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot01.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        //slot 02
        else if (characterSlot == CharacterSlot.CharacterSlot_02)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot02.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        // slot 03
        else if (characterSlot == CharacterSlot.CharacterSlot_03)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot03.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        // slot 04
        else if (characterSlot == CharacterSlot.CharacterSlot_04)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot04.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        //slot 05
        else if (characterSlot == CharacterSlot.CharacterSlot_05)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot05.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        // slot 06
        else if (characterSlot == CharacterSlot.CharacterSlot_06)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot06.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        // slot 07
        else if (characterSlot == CharacterSlot.CharacterSlot_07)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot07.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        //slot 08
        else if (characterSlot == CharacterSlot.CharacterSlot_08)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot08.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        //slot 09
        else if (characterSlot == CharacterSlot.CharacterSlot_09)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot09.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
        //slot 10
        else if (characterSlot == CharacterSlot.CharacterSlot_10)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // if file exist, get information
            if (saveFileWriter.CheckToSeeFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot10.CharacterName;
            }
            // if not, disable this gameobject
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
