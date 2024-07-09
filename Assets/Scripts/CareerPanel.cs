using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CareerPanel : MonoBehaviour
{
    public Text nameTxt;
    public Text ageTxt;
    public Text timeTxt;
    public Text contentTxt;
    public Button closeBtn;

    public UnityEvent OnFinished;
    
    void Start()
    {
        closeBtn.onClick.AddListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        gameObject.SetActive(false);
        OnFinished?.Invoke();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}