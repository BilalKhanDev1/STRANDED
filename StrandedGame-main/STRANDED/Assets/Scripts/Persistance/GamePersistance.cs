using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistance : MonoBehaviour
{
    public GameData _gameData;

    void Start() => LoadGame();

    void Update() => SaveGame();

    void SaveGame()
    {
        var data = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", data);
        Debug.Log("Saved");
    }

    void LoadGame()
    {
        var data = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(data);
        if (_gameData == null)
            _gameData = new GameData();

        Inventory.Instance.Bind(_gameData.SlotDatas);
    }
}