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


        private List<string> affirmations;

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[stringRandom.Next(s.Length)]).ToArray());
        }

        private static Random stringRandom = new Random();
        private Stack<int> affirmationIndexStack;

        public void Play()
        {
            void GenerateRandomAffirmations()
            {
                affirmations = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    affirmations.Add(i + 1 + "_" + RandomString(30));
                }
            }

            GenerateRandomAffirmations();

            GenerateRandomIndexSequence();
        
            ShowNext();
        }

        private void GenerateRandomIndexSequence()
        {
            var shuffleRandom = new Random();
            var shuffle = Enumerable.Range(0, affirmations.Count).OrderBy(a => shuffleRandom.NextDouble());
            affirmationIndexStack = new Stack<int>(shuffle);
        }

        public void SetAffirmations(List<string> affirmations)
        {
            this.affirmations = affirmations.ToList();
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
