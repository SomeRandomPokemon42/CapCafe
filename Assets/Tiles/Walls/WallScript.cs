using UnityEngine;

public class WallScript : MonoBehaviour
{
	private bool Shown = true;
	private Transform DisplayObject;
	[SerializeField] private float MoveDistance = 1.0f;
	[SerializeField] private float Speed = 2;
	private Vector3 defaultPosition;
	private Vector3 hiddenPosition;
	private float progress = 1;
	private void Start()
	{
		DisplayObject = transform.GetChild(0);
		defaultPosition = DisplayObject.position;
		hiddenPosition = DisplayObject.position - new Vector3(0, MoveDistance, 0);
	}
	public void ShowWall()
	{
		Shown = true;
	}
	public void HideWall()
	{
		Shown = false;
	}
	private void LateUpdate()
	{
		if (Shown)
		{
			progress += Time.deltaTime * Speed;
		}
		else
		{
			progress -= Time.deltaTime * Speed;
		}
		progress = Mathf.Clamp01(progress);
		DisplayObject.position = Vector3.Lerp(hiddenPosition, defaultPosition, progress);
	}
}
