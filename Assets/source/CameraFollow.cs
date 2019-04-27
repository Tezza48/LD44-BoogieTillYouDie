using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float zOffset = 10.0f;

	void Update()
	{
		transform.position = target.position + Vector3.back * zOffset;
	}
}