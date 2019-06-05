using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace AffirmationPlayer
{
    public class AffirmationPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI affirmationText;
        [SerializeField] private TMP_InputField affirmationInputField;

        private List<string> affirmations;
        private Stack<int> affirmationIndexStack;

        public void Play()
        {
            InitFromString(affirmationInputField.text);

            GenerateRandomIndexSequence();
        
            ShowNext();
        }

        private void GenerateRandomIndexSequence()
        {
            var shuffleRandom = new Random();
            var shuffle = Enumerable.Range(0, affirmations.Count).OrderBy(a => shuffleRandom.NextDouble());
            affirmationIndexStack = new Stack<int>(shuffle);
        }

        private void InitFromString(string affirmationLines)
        {
            affirmations = affirmationLines.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void ShowNext()
        {
            if (!affirmationIndexStack.Any())
            {
                GenerateRandomIndexSequence();
            }
        
            var nextIndex = affirmationIndexStack.Pop();
            affirmationText.text = affirmations[nextIndex];
        }
    }
}
