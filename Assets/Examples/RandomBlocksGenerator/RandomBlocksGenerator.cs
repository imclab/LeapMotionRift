using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomBlocksGenerator : MonoBehaviour {
  public float minRadius;
  public float maxRadius;
  public int numberOfBlocks;
  public float size;

  private Color[] list_of_colors = 
  {
    new Color(1.00f, 1.00f, 0.94f), // (255,255,240) Ivory
    new Color(0.96f, 0.96f, 0.86f), // (245,245,220) Beige
    new Color(0.96f, 0.87f, 0.70f), // (245,222,179) Wheat
    new Color(0.82f, 0.71f, 0.55f), // (210,180,140) Tan
    new Color(0.76f, 0.69f, 0.57f), // (195,176,145) Khaki
    new Color(0.75f, 0.75f, 0.75f), // (192,192,192) Silver
    new Color(0.50f, 0.50f, 0.50f), // (128,128,128) Gray
    new Color(0.27f, 0.27f, 0.27f), // (070,070,070) Charcoal
    new Color(0.00f, 0.00f, 1.00f), // (000,000,255) Navy Blue
    new Color(0.02f, 0.30f, 0.62f), // (008,076,158) Royal Blue
    new Color(0.00f, 0.00f, 0.80f), // (000,000,205) Medium Blue
    new Color(0.00f, 0.50f, 1.00f), // (000,127,255) Azure
    new Color(0.00f, 1.00f, 1.00f), // (000,255,255) Cyan
    new Color(0.50f, 1.00f, 0.83f), // (127,255,212) Aquamarine
    new Color(0.00f, 0.50f, 0.50f), // (000,127,127) Teal
    new Color(0.13f, 0.55f, 0.13f), // (034,139,034) Forest Green
    new Color(0.50f, 0.50f, 0.00f), // (127,127,000) Olive
    new Color(0.50f, 1.00f, 0.00f), // (127,255,000) Chartreuse
    new Color(0.75f, 1.00f, 0.00f), // (192,255,000) Lime
    new Color(1.00f, 0.84f, 0.00f), // (255,215,000) Golden
    new Color(0.85f, 0.65f, 0.13f), // (218,165,032) Goldenrod
    new Color(1.00f, 0.50f, 0.31f), // (255,127,080) Coral
    new Color(0.98f, 0.50f, 0.45f), // (250,128,114) Salmon
    new Color(0.99f, 0.06f, 0.75f) // (252,015,192) Hot Pink
  };

  private void Reset()
  {
    if (maxRadius < minRadius)
      return;

    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    sphere.transform.parent = transform;
    sphere.transform.localScale = Vector3.one * (maxRadius + size) * 2.0f;
    sphere.transform.localPosition = Vector3.zero;
    Mesh sphereMesh = sphere.GetComponent<MeshFilter>().mesh;
    sphereMesh.triangles = sphereMesh.triangles.Reverse().ToArray();
    sphereMesh.RecalculateNormals();
    sphere.GetComponent<SphereCollider>().enabled = false;
    Destroy(sphere.GetComponent<SphereCollider>());
    sphere.AddComponent<MeshCollider>();
    sphere.renderer.enabled = false;

    for (int i = 0; i < numberOfBlocks; ++i)
    {
      float radius = Random.Range(minRadius, maxRadius);
      float theta = Random.Range(0.0f, 2 * Mathf.PI);
      float phi = Random.Range(0.0f, 2 * Mathf.PI);

      Vector3 position = new Vector3(
        radius * Mathf.Sin(theta) * Mathf.Cos(phi),
        radius * Mathf.Sin(theta) * Mathf.Sin(phi),
        radius * Mathf.Cos(theta)
        );

      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.parent = transform;
      cube.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
      cube.transform.localScale = Vector3.one * size;
      cube.transform.localPosition = position;
      cube.AddComponent<Rigidbody>();
      cube.rigidbody.useGravity = false;

      cube.renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
  }

	// Use this for initialization
	void Start () {
    Reset();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
