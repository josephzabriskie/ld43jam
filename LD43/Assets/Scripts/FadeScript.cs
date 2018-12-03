using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

	Image img;
	void Start () {
		this.img = GetComponent<Image>();
	}
	
	public void Fade(bool appear, float fadetime){
		StartCoroutine(FadeIE(appear, fadetime));
	}

	IEnumerator FadeIE(bool appear, float fadetime){
		float aValue = appear ? 1.0f : 0.0f;
		float alpha = this.img.color.a;
    	for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadetime)
    	{
        	Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
        	this.img.color = newColor;
        	yield return null;
    	}
	}
	
}
