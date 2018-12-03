using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {
	
	Text msg;
	Image img;
	public GameObject panel;
	bool running = false;
	void Start () {
		this.msg = GetComponentInChildren<Text>();
		this.msg.text = " ";
		this.img = GetComponentInChildren<Image>();
		this.panel.SetActive(false);
	}

	public void ShowMsgTime(string msg, float time){
		if(!this.running){
			this.running = true;
			StartCoroutine(this.ShowMsgTimeIE(msg, time));
		}
	}

	IEnumerator ShowMsgTimeIE(string msg, float time){
		this.msg.text = msg;
		this.panel.SetActive(true);
		float eTime = 0.0f; // elapsed time
		while(eTime < time){
			eTime += Time.deltaTime;
			yield return null;
		}
		this.panel.SetActive(false);
		running = false;
	}
	

	//!!!!!This code don't work yet...
	// public void Fade(bool appear, float fadetime){
	// 	StartCoroutine(FadeIE(appear, fadetime));
	// }

	// IEnumerator FadeIE(bool appear, float fadetime){
	// 	float aValue = appear ? 1.0f : 0.0f;
	// 	float alphaimg = this.img.color.a;
	// 	float alphatxt = this.msg.color.a;
    // 	for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadetime)
    // 	{
    //     	Color newColor = new Color(1, 1, 1, Mathf.Lerp(alphaimg,aValue,t));
    //     	this.img.color = newColor;
	// 		newColor = new Color(0, 0, 0, Mathf.Lerp(alphatxt,aValue,t));
	// 		this.msg.color = newColor;
    //     	yield return null;
    // 	}
	// }
}
