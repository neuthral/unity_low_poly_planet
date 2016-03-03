using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Noise;


public class icosa : MonoBehaviour {

    public float scale = 0.25f;
    public float modifier = 2f;
    public double pScale = 0.02;
    public byte pOctaves = 1;
    public float noiseDensity = 3f;

    [Range(0.1f, 10.0f)]
    public float waterScale = 0.1f;

    public NoiseGen noiseGen;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    float xOrg = 1f;
    float yOrg = 1f;

    const int textureSize = 32;

    // Use this for initialization
    void Start () {

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        rend = GetComponent<Renderer>();
        noiseTex = new Texture2D(textureSize, textureSize);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;

        noiseGen = new NoiseGen(pScale, pOctaves);

        float random = Random.Range(0f, 1000f);
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            v.Scale(new Vector3(noiseDensity, noiseDensity, noiseDensity));
            
            float noise1 = noiseGen.GetNoise(v.x + random, v.y + random, v.z + random) * scale;
            float factor = 1f - (scale / modifier) + noise1;
            vertices[i] = Vector3.Scale(vertices[i], new Vector3(factor, factor, factor));
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        //CalcNoise();
    }
    
    void CalcNoise()
    {
        float y = 0.0f;
        while (y < noiseTex.height)
        {
            float x = 0.0f;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = noiseGen.GetNoise(xCoord, yCoord, 0);
                pix[(int)(y * noiseTex.width + x)] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
    
    // Update is called once per frame
    void Update () {
        foreach (Transform child in transform)
        {
            child.transform.localScale = new Vector3(waterScale, waterScale, waterScale);
        }

    }

    
}

/*
        Color[,] colors2D = new Color[textureSize, textureSize]; // a nice 2d array for our texture
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                double noise = noiseGen.GetNoise(x, y, 0); // get the noise at the current position (a value from 0.0 - 1.0)         
                byte value = (byte)(255 * noise); // turn that value into a byte (0 - 255)         
                colors2D[x, y] = new Color(value, value, value); // turn that value into a color     
            }
        }
        Color[] colors1D = new Color[textureSize * textureSize]; // convert the 2d array into a 1d array - this code will normally be in your infrastructure 
        for (int x = 0; x < textureSize; x++) {
            for (int y = 0; y < textureSize; y++) {
                colors1D[x + y * textureSize] = colors2D[x, y];
                noiseTexture.SetPixels(colors1D); // update the texture with our new values
            }
        }

        noiseTexture.Apply();
        */
