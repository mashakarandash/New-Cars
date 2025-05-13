using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _printText;
    [SerializeField] private string _textToWrite;
    [SerializeField] private float _delay;
    [SerializeField] private MainMenuAnimation _mainMenuPanel;
    
    public IEnumerator Print()
    {
        for(int i = 0; i < _textToWrite.Length; i++)
        {
            _printText.text = _textToWrite.Substring(0, i); //эффект печатной машинки. substring (начальная буква в предложении, конечная буква в предложении) 
            yield return new WaitForSeconds(_delay);
        }
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _mainMenuPanel.ShowMenu();
    }

    private void Start()
    {
        StartCoroutine(Print());
    }

}
