using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond;

    public static DialogManager Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialog(Dialog dialog)
    {
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));

    }

    public IEnumerator TypeDialog(string lines) //makes dialog appear letter by letter
    {
        dialogText.text = "";
        foreach( var letter in lines.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }
}
