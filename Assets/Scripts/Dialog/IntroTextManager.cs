using System.Collections;
using UnityEngine;

namespace Dialog
{
    public class IntroTextManager : MonoBehaviour
    {

        private int currentDialog = 0;
        private int currentLetterNumber = 0;
        private string currentText = "";
        [SerializeField] private float timeBetweenLetters=0.15f;
        
        public string[] dialogTexts;

        public AudioSource[] dialogVoicelines;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PrintCurrentString());
            PlayVoiceLine(0);
        }

        private void PlayVoiceLine(int pNr)
        {
            dialogVoicelines[pNr].Play();
        }
        IEnumerator PrintDialogLine()
        {
            currentText = "";
            yield return new WaitForSeconds(2);
            if (currentDialog < dialogTexts.Length - 1)
            {
                currentDialog++;
                PlayVoiceLine(currentDialog);
                StartCoroutine(PrintCurrentString());
            }
            else
            {
                DialogManager.instance.FadeOut();
            }
        }

        IEnumerator PrintCurrentString()
        {
            
            for (int i = 0; i <= dialogTexts[currentDialog].Length; i++)
            {
                currentText=dialogTexts[currentDialog].Substring(0,i);
                yield return new WaitForSeconds(timeBetweenLetters);
                DialogManager.instance.PrintDialog(currentText);
            }
            StartCoroutine(PrintDialogLine());

        }
    }
}
