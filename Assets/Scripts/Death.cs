using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	public float delay = 4.0f;
	void Start()
	{
		StartCoroutine(KillPumpkin());
	}
	IEnumerator KillPumpkin()
	{
		yield return new WaitForSeconds(delay);

		Instantiate(Resources.Load("death"), gameObject.transform.position, Quaternion.identity);

		Destroy(gameObject);

	}
}