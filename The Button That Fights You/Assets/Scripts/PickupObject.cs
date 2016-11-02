using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carryin;
	GameObject carriedObject;
	public float distance;
	public float smooth;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (carryin)
		{
			carry(carriedObject);
			checkdrop();
		}
		else
		{
			pickup();
		}
	}

	void carry(GameObject c)
	{
		c.transform.position = Vector3.Lerp (c.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
	}
	void checkdrop()
	{
		if (Input.GetKeyDown (KeyCode.E))
		{
			drop();
		}
	}

	void drop ()
	{
		carryin = false;
		carriedObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject = null;
	}

	void pickup()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			Ray ray = mainCamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if (p != null) {
					carryin = true;
					carriedObject = p.gameObject;
					carriedObject.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
	}

}
