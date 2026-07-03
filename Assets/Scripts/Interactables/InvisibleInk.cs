using UnityEngine;

public class InvisibleInk : MonoBehaviour
{
    public float fadeSpeed = 1f;

    private Material mat;

    private float alpha = 0;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        Color c = mat.color;

        c.a = 0;

        mat.color = c;
    }

    void Update()
    {
        if (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;

            Color c = mat.color;

            c.a = alpha;

            mat.color = c;
        }
    }
}