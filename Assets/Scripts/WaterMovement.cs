using UnityEngine;
using System.Collections;

public class WaterMovement : MonoBehaviour
{
	
	public float scale = 0.01f;
	public float speed = 1f;
	public float noiseStrength = 0.1f;
	public float noiseWalk = 0.1f;
	public float moveX = 0.1f;
	public float moveY = 0.1f;
	public float curX = 0.1f;
	public float curY = 0.1f;
	public Renderer rend;



	private Vector3[] baseHeight;

	void Awake(){
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		curX = rend.material.mainTextureOffset.x;
		curY = rend.material.mainTextureOffset.y;

	}


	void Update () {
		curX+=Time.deltaTime*moveX;
		curY+=Time.deltaTime*moveY;
		rend.material.SetTextureOffset("_MainTex",new Vector2(curX,curY));

		Mesh mesh = GetComponent<MeshFilter>().mesh;

		if (baseHeight == null)
			baseHeight = mesh.vertices;
		//half offsetVert = v.vertex.x * v.vertex.x + v.vertex.z + v.vertex.z;

		Vector3[] vertices = new Vector3[baseHeight.Length];
		for (int i=0;i<vertices.Length;i++)
		{
			Vector3 vertex = baseHeight[i];
			vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x * baseHeight[i].x + baseHeight[i].z + baseHeight[i].z) * scale;
			vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)    ) * noiseStrength;
			vertices[i] = vertex;
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
	}
}