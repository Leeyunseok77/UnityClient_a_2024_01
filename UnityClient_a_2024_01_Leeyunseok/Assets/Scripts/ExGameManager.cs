using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExGameManager : MonoBehaviour
{
    public ExGameData gameData;
    private void Start()
    {
        Debug.Log("Game Name : " + gameData.gameName);
        Debug.Log("Game Score : " + gameData.gameScore);
        Debug.Log("Is Game Active : " + gameData.isGameActive);
    }
}
