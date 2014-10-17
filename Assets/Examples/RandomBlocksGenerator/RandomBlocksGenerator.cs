using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomBlocksGenerator : MonoBehaviour {
  public float minRadius;
  public float maxRadius;
  public int numberOfBlocks;
  public float size;

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
