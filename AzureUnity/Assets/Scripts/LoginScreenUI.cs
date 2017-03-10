using UnityEngine;
using System.Collections;
using NetUtils;

public class LoginScreenUI : MonoBehaviour {

	public GameObject API_CLIENT_OBJ;
	private APIClient API_CLIENT;

	public bool loggedIn = false;
	
	public GameObject loginScreen;
	public GameObject loginButton;
	public GameObject successScreen;
	public GameObject connectionSprite;
	public GameObject offlineText;

	public string usernameField;
	public string passwordField;

	void Start()
	{
		API_CLIENT = API_CLIENT_OBJ.GetComponent<APIClient> ();
	}

	public void usernameAssign(string inputField)
	{
		usernameField = inputField;
	}

	public void passwordAssign(string inputField)
	{
		passwordField = inputField;
	}

	public void LoginCoroutine()
	{
		StartCoroutine(Login(usernameField, passwordField));
	}

	IEnumerator Login (string username, string password) 
	{

		NetObj loginRequest = new NetObj();
		loginRequest.resourcePath = API_CLIENT.serverLocation + "api/login";
		loginRequest.payload = "{\n\"username\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";

		loginScreen.SetActive (false);
		loginButton.SetActive (false);
		connectionSprite.SetActive (true);

		yield return loginRequest.Post();

		connectionSprite.SetActive (false);

		if (loginRequest.responce.responseHeaders ["STATUS"] == API_CLIENT.okCode) {
			loggedIn = true;
			successScreen.SetActive(true);
			Debug.Log(loginRequest.responce.text);
		} else {
			Debug.Log(loginRequest.responce.text);
			loginScreen.SetActive (true);
			loginButton.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (API_CLIENT.isServerAlive == false) {
			offlineText.SetActive (true);
		} else {
			offlineText.SetActive (false);
		}
	}
}
