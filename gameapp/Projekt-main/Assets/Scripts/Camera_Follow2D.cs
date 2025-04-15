using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
	public Transform Hero; // A karakter referenci�ja
	public float smoothSpeed = 5f; // A k�vet�s simas�ga
	public Vector3 offset; // Eltol�s a karakterhez k�pest

	void LateUpdate()
	{
		if (Hero != null)
		{
			Vector3 targetPosition = new Vector3(Hero.position.x + offset.x, Hero.position.y + offset.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
		}
	}
}
