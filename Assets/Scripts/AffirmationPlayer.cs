using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace AffirmationPlayer
{
    public class AffirmationPlayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text remainingTimeText;
        [SerializeField] private TMP_InputField durationInputField;

        [SerializeField] private TextMeshProUGUI affirmationText;

        [SerializeField] private Affirmations affirmations;

        private Stack<int> affirmationIndexStack;

        private double timeElapsed;


        [SerializeField]
        private UnityEvent sessionComplete;

        public List<string> Affirmations
        {
            get { return affirmations.affirmationsList; }
            set { affirmations.affirmationsList = value; }
        }

        public void Play()
        {
            if (!int.TryParse(durationInputField.text, out int durationMinutes))
            {
                Debug.LogError("can't parse duration text");
                return;
            }

            StartTimer(durationMinutes*60);
            affirmations.LoadLines();

            GenerateRandomIndexSequence();
        
            ShowNext();
        }

        private void StartTimer(int durationSeconds)
        {
            
            StartCoroutine(SessionTimer(durationSeconds));
        }

        private IEnumerator SessionTimer(int durationSeconds)
        {
            int remainingTime = durationSeconds;
            SetRemainingTimeText(remainingTime);
            do
            {
                yield return new WaitForSeconds(1);

                remainingTime -= 1;
                SetRemainingTimeText(remainingTime);
            } while (remainingTime > 0);
            
            yield return new WaitForSeconds(.5f);
            
            sessionComplete.Invoke();
        }

        private void SetRemainingTimeText(int remainingTime)
        {
            remainingTimeText.text = $"{remainingTime / 60:00}:{remainingTime % 60:00}";
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
