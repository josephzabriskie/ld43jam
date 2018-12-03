using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Animator FadeControl;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(FindObjectOfType<Canvas>().gameObject);
    }
    public void FadeToBlack()
    {
        
        FadeControl.SetBool("FadeTime", true);
        
        
    }

    public void ResetFade()
    {
        FadeControl.SetBool("FadeTime", false);
    }
}
