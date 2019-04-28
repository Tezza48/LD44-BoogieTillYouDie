using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float zOffset = 10.0f;
	public float orthoSizeZoom = 3.0f;
	public float orthoSizeNormal = 5.0f;
	public float rotZoom = -10.0f * Mathf.Deg2Rad;
	public bool isZooming = false;

	new private Camera camera;

	void Start()
	{
		camera = GetComponent<Camera>();
	}

	void Update()
	{
		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, isZooming ? orthoSizeZoom : orthoSizeNormal, Time.deltaTime);

		float rot = isZooming ? rotZoom : 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(rot, Vector3.back), Time.deltaTime * (isZooming ? 0.2f : 2.0f));

		transform.position = target.position + Vector3.back * zOffset;
	}
}