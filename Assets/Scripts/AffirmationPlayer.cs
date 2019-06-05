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
        
        [SerializeField] private Affirmations affirmations;
        
        private Stack<int> affirmationIndexStack;

        public List<string> Affirmations
        {
            get { return affirmations.affirmationsList; }
            set { affirmations.affirmationsList = value; }
        }

        public void Play()
        {
            affirmations.LoadLines();

            GenerateRandomIndexSequence();
        
            ShowNext();
        }

        private void GenerateRandomIndexSequence()
        {
            var shuffleRandom = new Random();
            var shuffle = Enumerable.Range(0, Affirmations.Count).OrderBy(a => shuffleRandom.NextDouble());
            affirmationIndexStack = new Stack<int>(shuffle);
        }
        

        public void ShowNext()
        {
            if (!affirmationIndexStack.Any())
            {
                GenerateRandomIndexSequence();
            }
        
            var nextIndex = affirmationIndexStack.Pop();
            affirmationText.text = Affirmations[nextIndex];
        }
      
    }
}
