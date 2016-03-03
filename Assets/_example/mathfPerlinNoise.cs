using UnityEngine;
using System.Collections;
 
 public class mathfPerlinNoise : MonoBehaviour
{
    public int pixWidth = 1024;
    public int pixHeight = 1024;
    public float xOrg = 1.2F;
    public float yOrg = 0.34F;
    public float scale = 11.0F;
    private Texture2D noiseTex;
    private Color[] pix;
    void Start()
    {
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        GetComponent<Renderer>().material.mainTexture = noiseTex;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
    }
    void CalcNoise()
    {
        float y = 0.0F;
        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)(y * noiseTex.width + x)] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalcNoise();
        }
    }
}