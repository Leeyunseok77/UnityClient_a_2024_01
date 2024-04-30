using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

#if UNITY_EDITOR
public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        if(GUILayout.Button("Reset Story Models"))      //에디터에서 버튼 생성
        {
            gameSystem.ResetStoryModels();
        }
    }
#endif
}

public class GameSystem : MonoBehaviour
{
    public StoryModel[] storyModels;                //변경된 스토리 모델로 생성

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>(""); // Resources 폴더 아래 모든 StoryModel 불러오기
    }
#endif
}
