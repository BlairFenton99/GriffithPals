using UnityEngine;

public class FadeInSphere : MonoBehaviour
{
    public float fadeDuration = 5f;    // Slower fade-in (5 seconds)
    public float targetAlpha = 0.5f;

    private Material mat;
    private Color startColor;
    private Color targetColor;
    private float timer = 0f;
    private bool fadingIn = false;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        mat = renderer.material;

        // Setup transparency for URP Lit shader
        mat.SetFloat("_Surface", 1);
        mat.SetFloat("_Blend", 1);
        mat.SetFloat("_ZWrite", 0);
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.renderQueue = 3000;

        startColor = mat.color;
        startColor.a = 0f;
        targetColor = mat.color;
        targetColor.a = targetAlpha;

        mat.color = startColor;
        fadingIn = true;
    }

    void Update()
    {
        if (fadingIn)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);
            mat.color = Color.Lerp(startColor, targetColor, t);

            if (t >= 1f)
                fadingIn = false;
        }
    }
}

