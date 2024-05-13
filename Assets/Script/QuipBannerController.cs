using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuipBannerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bannerDescription;
    [SerializeField] private Image bannerBackdrop;
    [SerializeField] private TextMeshProUGUI bannerTitle;
    [SerializeField] private CanvasGroup canvasGroup;

    public void StartBannerQuip(string description, string titleText, float toDuration, float lingerDuration, float outDuration)
    {
        Debug.Log("Start Quip Banner");
        StartCoroutine(BannerRoutine(description, titleText, toDuration, lingerDuration, outDuration));
    }

    public IEnumerator BannerRoutine(string description, string titleText,  float toDuration, float lingerDuration, float outDuration)
    {
        bannerDescription.text = description;
        bannerTitle.text = titleText;
        GetComponent<CanvasGroup>().alpha = 1.0f;
        //GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, toDuration);
        //LeanTween.size(GetComponent<RectTransform>(), new Vector2(2000,200), toDuration);


        yield return new WaitForSeconds(lingerDuration);

        GetComponent<CanvasGroup>().alpha = 0.0f;
        //LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, outDuration);
        //LeanTween.size(GetComponent<RectTransform>(), new Vector2(1, 0), outDuration);
    }
}
