using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 对话页面
/// </summary>
public class DialogPanel : MonoBehaviour
{
    private static DialogPanel _instance;
    public static DialogPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DialogPanel>(true);
            }
            return _instance;
        }
    }

    public Text nameText;
    public Text dialogText;
    public Button nextBtn;
    public Button[] choiceButtons; // 选项按钮

    private Dialogue[] m_Dialogues;
    private int currentDialogIndex;
    private bool isDialogEnd;
    public UnityEvent OnComplete;
    
    void Start()
    {
        nextBtn.onClick.AddListener(OnNextButtonClicked);
    }

    public void ShowDialog(Dialogue[] dialogues)
    {
        this.gameObject.SetActive(true);
        m_Dialogues = dialogues;
        isDialogEnd = false;
        ShowNextDialog();
    }
    
    /// <summary>
    /// 展示下一幕聊天
    /// </summary>
    public void ShowNextDialog()
    {
        if (currentDialogIndex < m_Dialogues.Length)
        {
            var currentDialog = m_Dialogues[currentDialogIndex];
            
            nameText.text = currentDialog.Speaker;
            dialogText.text = currentDialog.Content;

            ShowChoices(currentDialog.Choices);
            // if (currentDialog.autoNext)
            // {
            //     currentDialogIndex++;
            //     isDialogEnd = false;
            // }
            // else
            // {
            //     isDialogEnd = true;
            // }
        }
        else
        {
            this.gameObject.SetActive(false);
            OnComplete?.Invoke();
        }
    }

    /// <summary>
    /// 展示上一幕聊天
    /// </summary>
    public void ShowFrontDialog()
    {
        currentDialogIndex--;
        if (currentDialogIndex > 0)
        {
            var currentDialog = m_Dialogues[currentDialogIndex];
            dialogText.text = currentDialog.Content;
            ShowChoices(currentDialog.Choices);
            currentDialogIndex++;
        }
    }

    /// <summary>
    /// 展示不同选择
    /// </summary>
    /// <param name="choices"></param>
    public void ShowChoices(Choice[] choices)
    {
        nextBtn.gameObject.SetActive(choices.Length == 0);
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].Content;
                int choiceIndex = i;
                choiceButtons[i].onClick.AddListener(() => OnChoiceClicked(choiceIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 选项点击
    /// </summary>
    /// <param name="choiceIndex"></param>
    public void OnChoiceClicked(int choiceIndex)
    {
        var currentDialog = m_Dialogues[currentDialogIndex];
        var next = currentDialog.Choices[choiceIndex].NextDialogIndex;
        if (next == -1)
        {
            isDialogEnd = true;
            gameObject.SetActive(false);
            OnComplete?.Invoke();
            Debug.Log("获得Tag："+currentDialog);
            return;
        }

        currentDialogIndex = next;
        ShowNextDialog();
    }

    public void OnNextButtonClicked()
    {
        currentDialogIndex++;
        ShowNextDialog();
    }

}