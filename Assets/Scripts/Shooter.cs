using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public GameObject ShootPoint;
	public GameObject ArrowPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Instantiate (ArrowPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
		}	
	}
}
