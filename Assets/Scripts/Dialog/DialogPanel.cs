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
    public UnityEvent OnBreak;

    void Start()
    {
        nextBtn.onClick.AddListener(OnNextButtonClicked);
    }

    public void ShowDialog(Dialogue[] dialogues)
    {
        this.gameObject.SetActive(true);
        m_Dialogues = dialogues;
        isDialogEnd = false;
        currentDialogIndex = 0;
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
        }
        else
        {
            isDialogEnd = true;
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
                var lable = choiceButtons[i].GetComponentInChildren<Text>(true);
                lable.text = choices[i].Content;
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
        var choice = currentDialog.Choices[choiceIndex];
        var next = choice.NextDialogIndex;

        switch (currentDialog.BreakType)
        {
            case EBreakType.None:
                if (next == -1)
                {
                    isDialogEnd = true;
                    gameObject.SetActive(false);
                    OnComplete?.Invoke();
                    if (!string.IsNullOrEmpty(currentDialog.Tag))
                    {
                        Debug.Log("获得Tag：" + currentDialog);
                    }
                }
                else
                {
                    currentDialogIndex = next;
                    ShowNextDialog();
                }
                break;
            case EBreakType.Finish:
                isDialogEnd = true;
                gameObject.SetActive(false);
                OnComplete?.Invoke();
                break;
            case EBreakType.Repeat:
                gameObject.SetActive(false);
                break;
            case EBreakType.Break:
                isDialogEnd = true;
                gameObject.SetActive(false);
                OnBreak?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnNextButtonClicked()
    {
        var currentDialog = m_Dialogues[currentDialogIndex];
        switch (currentDialog.BreakType)
        {
            case EBreakType.None:
                currentDialogIndex++;
                ShowNextDialog();
                break;
            case EBreakType.Finish:
                isDialogEnd = true;
                gameObject.SetActive(false);
                OnComplete?.Invoke();
                break;
            case EBreakType.Repeat:
                gameObject.SetActive(false);
                break;
            case EBreakType.Break:
                isDialogEnd = true;
                gameObject.SetActive(false);
                OnBreak?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}