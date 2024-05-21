using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;                 //������ ��Ŭ�� ȭ
    public StoryModel currentStoryModel;                //���� ���丮 �� ����

    public enum TEXTSYSTEM
    {
        NONE,
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                          //�� ���ڰ� ��Ÿ���µ� �ɸ��� �ð� 
    public string fullText;                             //��ü ǥ���� �ؽ�Ʈ
    private string currentText = "";                    //������� ǥ�õ� �ؽ�Ʈ
    public Text textComponent;                          //text ������Ʈ UI
    public Text storyIndex;                             //���丮 ��ȣ ǥ���� UI
    public Image imageComponent;                        //�̹��� UI

    public Button[] buttonWay = new Button [3];          //������ ��ư �߰�
    public Text[] buttonWayText = new Text [3];          //������ ��ư Text

    public TEXTSYSTEM currentTextShow = TEXTSYSTEM.NONE;

    public void Awake()
    {
        instance = this;
    }


    void Start()
    {
        for(int i = 0; i < buttonWay.Length; i++)
        {
            int wayIndex = i;
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));
        }

        CoShowText();

        //StroyModelinit();
        //StartCoroutine(ShowText());
    }

    public void StroyModelinit()
    {
        fullText = currentStoryModel.storyText;
        storyIndex.text = currentStoryModel.storyNumber.ToString();

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;        //��ư �̸��� ����
        }
    }
    public void OnWayClick(int index)
    {
        if (currentTextShow == TEXTSYSTEM.DOING)
            return;

        bool CheckEventTypeNone = false;            //�⺻���� NONE�϶��� ������ �����̶�� �Ǵ��ϰ� ���нÿ� �ٽ� �Ҹ��°��� ���ϱ� ���ؼ� Boll ����
        StoryModel playStoryModel = currentStoryModel;

        if(playStoryModel.options[index] .eventCheck .eventType == StoryModel .Eventcheck .EventType.NONE)
        {
            for(int i = 0; i < playStoryModel.options[index] .eventCheck .suceessResult.Length; i++)
            {
                GameSystem.instance.ApplyChoice(currentStoryModel.options[index].eventCheck.suceessResult[i]);
                CheckEventTypeNone = true;
            }
        }
    }
    public void CoShowText()
    {
        StroyModelinit();
        ResetShow();
        StartCoroutine(ShowText());
    }




    public void ResetShow()
    {
        textComponent.text = "";

        for(int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    IEnumerator ShowText()                                  //�ڷ�ƾ �Լ� ���
    {
        currentTextShow = TEXTSYSTEM.DOING;
        if(currentStoryModel.MainImage != null)
        {
            //Texture2D�� Sprtie ��ȯ

            Rect rect = new Rect(0, 0, currentStoryModel.MainImage.width, currentStoryModel.MainImage.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);        //��������Ʈ�� ��(�߽�) ����
            Sprite sprite = Sprite.Create(currentStoryModel.MainImage, rect, pivot);

            imageComponent.sprite = sprite; ;
        }
        else
        {
            Debug.LogError("�ؽ��� �ε��� ���� �ʾҽ��ϴ�. : " + currentStoryModel.MainImage.name);
        }

        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);         //Substring ���ڿ� �ڸ��� �Լ�
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);         //delay �ʸ�ŭ For ���� ���� ��Ų��.
        }

        for(int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);

        currentTextShow = TEXTSYSTEM.NONE;
    }
}
