using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float zOffset = 10.0f;
	public float orthoSizeZoom = 3.0f;
	public float orthoSizeNormal = 5.0f;
	public bool isZooming = false;

	private Camera camera;

	void Start()
	{
		camera = GetComponent<Camera>();
	}

	void Update()
	{
		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, isZooming ? orthoSizeZoom : orthoSizeNormal, Time.deltaTime);

		transform.position = target.position + Vector3.back * zOffset;
	}
}