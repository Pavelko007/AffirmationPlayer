using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Affirmations:MonoBehaviour
{
    [SerializeField] private TMP_InputField affirmationInputField;
    
    public List<string> affirmationsList;

    public void LoadLines()
    {
        InitFromString(affirmationInputField.text);
    }
    
    private void InitFromString(string affirmationLines)
    {
        affirmationsList = affirmationLines.Split(
                new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }

    public void Init(Saver.Save save)
    {
        affirmationsList = save.affirmationsList;
        affirmationInputField.text = String.Join(Environment.NewLine, affirmationsList);
    }
}