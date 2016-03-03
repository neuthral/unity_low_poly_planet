using UnityEngine;
using System.Collections;

public class simple : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        float random = Random.Range(100f, 1000f);

        for (int i = 0; i < vertices.Length; i++)
        {
            float noiseDensity = 2f;
            Vector3 v = vertices[i];
            v.Scale(new Vector3(noiseDensity, noiseDensity, noiseDensity));
            float scale = .25f;
            float noise1 = Noise.Noise.GetOctaveNoise(v.x + random, v.y + random, v.z + random, 4) * scale;
            float factor = 1f - (scale / 2f) + noise1;
            vertices[i] = Vector3.Scale(vertices[i], new Vector3(factor, factor, factor));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
