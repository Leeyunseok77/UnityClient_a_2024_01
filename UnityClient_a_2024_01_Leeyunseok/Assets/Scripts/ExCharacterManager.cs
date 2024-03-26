using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterManager : MonoBehaviour
{
    public List<ExCharacter> characterList = new List<ExCharacter>();
    //가장 상위 클래스로 등록해도 ExCharacterFast, ExCharacterUp 자식 클래스가 List에 담아진다.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))            //키를 누르면 파괴된다.
        { 
            for (int i = 0; i < characterList.Count; i++)
            {
                characterList[i].DestoryCharacter();
            }
        }
        
    }
}
