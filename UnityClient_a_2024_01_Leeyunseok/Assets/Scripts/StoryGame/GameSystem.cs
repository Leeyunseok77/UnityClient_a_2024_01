using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;          //에디터
using System.Text;          //텍스트 사용

namespace STORYGAME //이름 충돌 방지
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor      //유니티 에디터를 상속
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gameSystem = (GameSystem)target;

            if (GUILayout.Button("Reset Stroy Models"))      //버튼을 생성
            {
                gameSystem.ResetStoryModels();
            }
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem instance;          //간단한 싱글톤 화

        private void Awake()
        {
            instance = this;
        }

        public StoryTableObject[] storyModels;

#if UNITY_EDITOR
        [ContextMenu("Reset Stroy Models")]

        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");//Resources 폴더 아래 모든 StroyModel 불러 오;기
        }
#endif
    }
}