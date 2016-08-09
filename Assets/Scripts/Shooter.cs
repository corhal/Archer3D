using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public GameObject ShootPoint;
	public GameObject ArrowPrefab;
	public GameObject Target;
	float g = Physics.gravity.magnitude;
	float v0sq = 10.0f;
	float l;
	float h;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.LookRotation (new Vector3(Target.transform.position.x, 0.0f, Target.transform.position.z));
		float radius = Target.transform.localScale.x / 2;
		float t = 2 * Mathf.PI * Random.Range (0, radius);
		float u = Random.Range (0, radius) + Random.Range (0, radius);
		float r;
		if (u > radius) {
			r = 2 - u;
		} else {
			r = u;
		}
		float x = r * Mathf.Cos (t);
		float z = r * Mathf.Sin (t);
		x = Target.transform.position.x + x;
		z = Target.transform.position.z + z;
		Debug.Log (x + ":" + z);
		Vector3 distance = new Vector3 (x, transform.position.y, z);
		l = Vector3.Distance (transform.position, distance);
		h = ShootPoint.transform.position.y;
		Debug.Log ("l: " + l + ", h: " + h);
		FindAngle ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Instantiate (ArrowPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
		}	
	}

	void FindAngle() {
		float a = (g * Mathf.Pow (l, 2)) / (2 * v0sq);
		float b = -l;
		float c = b - h;
		float alpha = SquareSolve (a, b, c);

		Debug.Log ("alpha: " + alpha.ToString ());
	}

	float SquareSolve(float a, float b, float c) {
		float D = Mathf.Pow (b, 2.0f) - 4 * a * c;
		float x1 = 0;
		float x2 = 0;
		if (D >= 0) {
			x1 = (-b + Mathf.Sqrt (Mathf.Pow (b, 2) - 4 * a * c)) / (2 * a);
			x2 = (-b - Mathf.Sqrt (Mathf.Pow (b, 2) - 4 * a * c)) / (2 * a);
		}
		float alpha1 = Mathf.Atan (x1);
		float alpha2 = Mathf.Atan (x2);
		alpha1 = alpha1 * Mathf.Rad2Deg;
		alpha2 = alpha2 * Mathf.Rad2Deg;
		Debug.Log ("alpha1: " + alpha1 + ", alpha2: " + alpha2);
		if (Mathf.Min(alpha1, alpha2) > 0) {
			return Mathf.Min (alpha1, alpha2);
		} else {
			return Mathf.Max (alpha1, alpha2);
		}

	}
}
