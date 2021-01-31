using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeObj : MonoBehaviour
{
    public Image fadeGORenderer;

    public bool debugStartFade;

    public float fadeInTime = 1, fadeOutTime = 1, blackOutTime = 1;

    public void Update()
    {
        if (debugStartFade)
        {
            Fade();
            debugStartFade = false;
        }
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if(GameManager.S == null)
        {
            print("Error: No GameManager in this scene");
        }

        //if no mesh renderer get one
        GameManager.S.GetOrCreateComponent(out fadeGORenderer, this.gameObject);
        
        //if not invisible make invisible
        /*if(fadeGORenderer.color.a > 0)
        {
            Color color = fadeGORenderer.color;
            color.a = 0;
            fadeGORenderer.color = color;
        }*/
    }

    public void Fade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float normalizedTime = 0;
        while (normalizedTime < 1f)
        {
            Color color = fadeGORenderer.color;
            color.a = normalizedTime;
            fadeGORenderer.color = color;
            normalizedTime += Time.deltaTime / fadeOutTime;
            yield return null;
        }

        StartCoroutine(Blackout());
    }

    private IEnumerator Blackout()
    {
        GameManager.S.LoadNextLevel();

        float normalizedTime = 0;
        while (normalizedTime < 1f)
        {
            normalizedTime += Time.deltaTime / blackOutTime;
            yield return null;
        }

        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            Color color = fadeGORenderer.color;
            color.a = 1f - normalizedTime;
            fadeGORenderer.color = color;
            normalizedTime += Time.deltaTime / fadeInTime;
            yield return null;
        }
    }
}
