using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogParser {
	
	/*
	*	Dialogs are written with the following syntax:
	*	-[id_string]	// use -[ and ] to delimit the id
	*	Content
	*	Content
	*	...
	*/
	
	public static Dictionary<string, string> Parse() {
		Dictionary<string, string> list = new Dictionary<string, string>();

		string text = (Resources.Load("dialogs") as TextAsset).text;
		Debug.Log(text);

		foreach (var item in text.Split('-')) {
			if (item != "") {
				var block = item.Split(']');
				string id = block[0].Substring(1); // Removes the [
				// Removes the newline at the beginning and at the end
				string content = block[1].Substring(1);
				content = content.Remove(content.Length - 1);

				list.Add(id, content);
			}
		}

		return list;
	}
}