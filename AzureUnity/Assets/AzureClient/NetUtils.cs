using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetUtils{
	public class NetObj {

		public string contentType = "application/json"; // default content is json.
		public string resourcePath = "";
		public string payload = "{}";
		public Dictionary<string, string> headers;
		public WWW responce;

		// Constructor
		public NetObj () 
		{
			headers = new Dictionary<string, string>();
		}
		public WWW Post () 
		{
			headers.Add("Content-Type", contentType);
			byte[] body = Encoding.UTF8.GetBytes(payload);
			responce = new WWW(resourcePath, body, headers);
			return responce;
		}
	}
}

