using System;
using UnityEngine;
using UnityEngine.UI;
namespace DefaultNamespace.UI
{
    public class TutorialSequence : MonoBehaviour
    {
        public Transform[] tutorialElements; // List of tutorial elements (transforms)
        private Gameloop _gameLoop;

        private int currentIndex = 0;

        private void Start()
        {
            _gameLoop = Gameloop.Instance;

            // Hide all tutorial elements except the first one.
            for (int i = 1; i < tutorialElements.Length; i++)
            {
                tutorialElements[i].gameObject.SetActive(false);
            }

            // Show the first tutorial element.
            // tutorialElements[currentIndex].gameObject.SetActive(true);

            // Search for the "Continue" button as a child of each tutorial element.
            foreach (Transform tutorialElement in tutorialElements)
            {
                Button continueButton = tutorialElement.GetComponentInChildren<Button>();
                if (continueButton != null)
                {
                    // Add a listener to each "Continue" button.
                    continueButton.onClick.AddListener(OnContinueButtonClicked);
                }
            }

            gameObject.SetActive(false);
        }

        public void StartSequence()
        {
            gameObject.SetActive(true);
            // Show the first tutorial element.
            tutorialElements[currentIndex].gameObject.SetActive(true);
        }

        private void OnContinueButtonClicked()
        {
            // Disable the current element.
            tutorialElements[currentIndex].gameObject.SetActive(false);

            // Increase the index to show the next element (if available).
            currentIndex++;

            if (currentIndex < tutorialElements.Length)
            {
                // Show the next tutorial element.
                tutorialElements[currentIndex].gameObject.SetActive(true);
            }
            else
            {
                // Tutorial is complete; you can send out the "TutorialComplete" event here.
                // _gameLoop.UiBarManager.DisplayPopupText("tutorial Complete");
                gameObject.SetActive(false);
                OnTutorialCompleted(EventArgs.Empty);
                // Alternatively, you can invoke a custom event here using delegates or UnityEvent.
                // For example:
                // TutorialCompletedEvent.Invoke();
            }
        }

        public event EventHandler<EventArgs> TutorialCompleted;

        protected virtual void OnTutorialCompleted(EventArgs e)
        {
            TutorialCompleted?.Invoke(this, e);
        }
    }
}
