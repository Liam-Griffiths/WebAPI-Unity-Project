using UnityEngine;
using System.Collections;
using NetUtils;

public class APIClient : MonoBehaviour {

	public string serverLocation = "";
	public bool isServerAlive = false;
	public readonly string okCode = "HTTP/1.1 200 OK";

	// Use this for initialization
	IEnumerator Start () {
		// Test code -- To be moved out into a connection testing function/class.
		// Asks if server is online, via api/status which returns an ok 200 http responce.
		NetObj callServer = new NetObj();
		callServer.contentType = "";
		callServer.resourcePath = serverLocation + "api/status";
		yield return callServer.Post();
		if (callServer.responce.responseHeaders ["STATUS"] == okCode) 
		{
			isServerAlive = true;
		}
	}
}
