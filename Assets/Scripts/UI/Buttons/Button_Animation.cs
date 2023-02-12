using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Animation : MonoBehaviour
{
    [SerializeField] GameObject buttonImage;
    [SerializeField] float scalingFactor;

    Vector3 orScale;
    Vector3 targetScale;
    Vector3 imageOrScale;
    Vector3 targetImageScale;


    private void Start()
    {
        orScale = transform.localScale;
        targetScale = orScale * scalingFactor;

        if (buttonImage.Equals(null)) return; //Guardian
        imageOrScale = buttonImage.transform.localScale;
        targetImageScale = imageOrScale * scalingFactor;

    }
    public void Highlight()
    {
        
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, targetScale * 1.1f, 0.2f).setEaseOutBounce();
        if (buttonImage.Equals(null)) return;
        LeanTween.cancel(buttonImage);
        LeanTween.scale(buttonImage, targetImageScale, 2f) ;
    }
    public void UnHighlight()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, orScale, 0.1f);
        if (buttonImage.Equals(null)) return;
        LeanTween.cancel(buttonImage);
        LeanTween.scale(buttonImage, imageOrScale, 0.1f);
    }
}
