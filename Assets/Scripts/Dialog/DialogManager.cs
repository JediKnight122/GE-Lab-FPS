using TMPro;
using UnityEngine;

namespace Dialog
{
   public class DialogManager : MonoBehaviour
   {
      [SerializeField] private TMP_Text dialogText;
      public static DialogManager instance;
      
      private void Awake()
      {
         if(instance != null && instance != this)
         { 
            Destroy(this); 
         } 
         else 
         { 
            instance = this; 
         } 
      }

      public void FadeIn()
      {
         Debug.Log("Fading in");
         GetComponent<Animator>().SetTrigger("FadeIn");
      }
      public void FadeOut()
      {
         GetComponent<Animator>().SetTrigger("FadeOut");
      }
      public void PrintDialog(string pText)
      {
         dialogText.SetText(pText);
      }
   }
}
