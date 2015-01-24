using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionAnimations : MonoBehaviour {

    public Image leftEndGameBackground;
    public Image rightEndGameBackground;

    private float SpriteWidth;

    public float speed;

    private bool isAnimationFinished;

    private RectTransform screenRect;

    void Start()
    {

        screenRect = this.GetComponent<RectTransform>();

        leftEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);
        rightEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);

        leftEndGameBackground.rectTransform.anchoredPosition = new Vector2(-screenRect.rect.width / 2, 0);
        rightEndGameBackground.rectTransform.anchoredPosition = new Vector2(screenRect.rect.width / 2, 0);
    }

    public void LaunchStartGameAnimation()
    {
        leftEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);
        rightEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);

        leftEndGameBackground.rectTransform.anchoredPosition = new Vector2(0, 0);
        rightEndGameBackground.rectTransform.anchoredPosition = new Vector2(0, 0);

        StartCoroutine("StartLevelAnimation");
    }

    public void LaunchEndGameAnimation()
    {
        leftEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);
        rightEndGameBackground.rectTransform.localScale = new Vector3(screenRect.rect.width, screenRect.rect.height, 0);

        leftEndGameBackground.rectTransform.anchoredPosition = new Vector2(-screenRect.rect.width / 2, 0);
        rightEndGameBackground.rectTransform.anchoredPosition = new Vector2(screenRect.rect.width / 2, 0);

        StartCoroutine("EndLevelAnimation");
    }

    IEnumerator EndLevelAnimation()
    {
        yield return new WaitForSeconds(1);
        while (leftEndGameBackground.rectTransform.anchoredPosition.x < 0 || rightEndGameBackground.rectTransform.anchoredPosition.x > 0)
        {
            leftEndGameBackground.rectTransform.anchoredPosition += new Vector2(speed, 0);
            rightEndGameBackground.rectTransform.anchoredPosition -= new Vector2(speed, 0);
            yield return null;
        }

        isAnimationFinished = true;
    }

    IEnumerator StartLevelAnimation()
    {

        yield return new WaitForSeconds(1);
        while (leftEndGameBackground.rectTransform.anchoredPosition.x > -(screenRect.rect.width / 2) || rightEndGameBackground.rectTransform.anchoredPosition.x < screenRect.rect.width / 2)
        {
            leftEndGameBackground.rectTransform.anchoredPosition -= new Vector2(speed, 0);
            rightEndGameBackground.rectTransform.anchoredPosition += new Vector2(speed, 0);
            yield return null;
        }

        isAnimationFinished = true;
    }



}
