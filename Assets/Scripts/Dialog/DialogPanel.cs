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
        if (currentDialogIndex < m_Dialogues.Length && !isDialogEnd)
        {
            var currentDialog = m_Dialogues[currentDialogIndex];
            
            nameText.text = currentDialog.speaker;
            dialogText.text = currentDialog.content;

            ShowChoices(currentDialog.choices);
            if (currentDialog.autoNext)
            {
                currentDialogIndex++;
                isDialogEnd = false;
            }
            else
            {
                isDialogEnd = true;
            }
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
            dialogText.text = currentDialog.content;
            ShowChoices(currentDialog.choices);
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
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].content;
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
        var currentDialog = m_Dialogues[currentDialogIndex - 1];
        currentDialogIndex = currentDialog.choices[choiceIndex].nextDialogIndex;
        
        ShowNextDialog();
    }

    public void OnNextButtonClicked()
    {
        ShowNextDialog();
    }

}