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
    public DialogConfig Dialog;
    public Text titleText;
    public Text nameText;
    public Text dialogText;
    public Button nextBtn;
    public Button[] choiceButtons; // 选项按钮
    
    public GameObject elertPanel;
    public Button resetBtn;

    private int currentDialogIndex;
    private bool isDialogEnd;
    public UnityEvent onDialogEnd;

    private UnityAction dialogAction;
    void Start()
    {
        titleText.text = Dialog.title;
        ShowNextDialog();
        nextBtn.onClick.AddListener(OnNextButtonClicked);
        resetBtn.onClick.AddListener(OnRestButtonClicked);
    }

    /// <summary>
    /// 展示下一幕聊天
    /// </summary>
    public void ShowNextDialog()
    {
        if (currentDialogIndex < Dialog.dialogues.Count && !isDialogEnd)
        {
            var currentDialog = Dialog.dialogues[currentDialogIndex];
            
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
            onDialogEnd.Invoke();
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
            var currentDialog = Dialog.dialogues[currentDialogIndex];
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
        var currentDialog = Dialog.dialogues[currentDialogIndex - 1];
        var choice = currentDialog.choices[choiceIndex];
        currentDialogIndex = currentDialog.choices[choiceIndex].nextDialogIndex;
        
        if (!choice.showAlert)
        {
            ShowNextDialog();
        }
        else
        {
            elertPanel.SetActive(true);
        }
    }

    public void OnNextButtonClicked()
    {
        ShowNextDialog();
    }

    private void OnRestButtonClicked()
    {
        elertPanel.SetActive(false);
        ShowNextDialog();
    }
}