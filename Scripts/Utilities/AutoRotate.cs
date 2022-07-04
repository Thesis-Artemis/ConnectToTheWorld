using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour{
	public Vector3 speed = new Vector3(0,40f,0);
	public bool isRotating;
	void Update ()
	{
		if (isRotating) {
			transform.Rotate(speed * Time.deltaTime);
		}
	}
}
