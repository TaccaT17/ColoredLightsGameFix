using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObj : MonoBehaviour
{
    public MeshRenderer fadeGORenderer;

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
        if(fadeGORenderer.material.color.a > 0)
        {
            Color color = fadeGORenderer.material.color;
            color.a = 0;
            fadeGORenderer.material.color = color;
        }
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
            Color color = fadeGORenderer.material.color;
            color.a = normalizedTime;
            fadeGORenderer.material.color = color;
            normalizedTime += Time.deltaTime / fadeOutTime;
            yield return null;
        }

        StartCoroutine(Blackout());
    }

    private IEnumerator Blackout()
    {
        float normalizedTime = 0;
        while (normalizedTime < 1f)
        {
            normalizedTime += Time.deltaTime / fadeOutTime;
            yield return null;
        }

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float normalizedTime = 0;
        while (normalizedTime < 1f)
        {
            Color color = fadeGORenderer.material.color;
            color.a = 1f - normalizedTime;
            fadeGORenderer.material.color = color;
            normalizedTime += Time.deltaTime / fadeOutTime;
            yield return null;
        }
    }
}
