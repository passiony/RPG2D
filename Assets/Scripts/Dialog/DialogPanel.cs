using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityAction OnComplete;
    public UnityAction OnBreak;
    private Npc m_Npc;
    
    void Start()
    {
        nextBtn.onClick.AddListener(OnNextButtonClicked);
    }

    public void ShowDialog(Npc npc, Dialogue[] dialogues, UnityAction finished, UnityAction failed)
    {
        m_Npc = npc;
        this.gameObject.SetActive(true);
        m_Dialogues = dialogues;
        isDialogEnd = false;
        currentDialogIndex = 0;
        ShowNextDialog();
        OnComplete = finished;
        OnBreak = failed;
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
            dialogText.text = Player.ParseText(currentDialog.Content);

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
                var content = Player.ParseText(choices[i].Content);
                lable.text = content;
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

        if (!string.IsNullOrEmpty(currentDialog.Tag))
        {
            Debug.Log("获得Tag：" + currentDialog);
            Player.Instance.AddTag(currentDialog.Tag);
        }

        if (!string.IsNullOrEmpty(currentDialog.Reward))
        {
            Debug.Log("获得Reward：" + currentDialog);
            Player.Instance.Pack.AddItem(currentDialog.Reward);
        }

        if (choice.Score != 0)
        {
            m_Npc.Score += choice.Score;
            Debug.Log("获得Score：" + choice.Score);
        }

        switch (currentDialog.BreakType)
        {
            case EBreakType.None:
                if (next == -1)
                {
                    isDialogEnd = true;
                    gameObject.SetActive(false);
                    OnComplete?.Invoke();
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
        if (!string.IsNullOrEmpty(currentDialog.Tag))
        {
            Debug.Log("获得Tag：" + currentDialog.Tag);
            Player.Instance.Pack.AddTag(currentDialog.Tag);
        }

        if (!string.IsNullOrEmpty(currentDialog.Reward))
        {
            Debug.Log("获得Reward：" + currentDialog.Reward);
            Player.Instance.Pack.AddItem(currentDialog.Reward);
        }

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