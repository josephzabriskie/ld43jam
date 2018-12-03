using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {

    public GameObject MainImage;
    public GameObject Button;

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Main_Menu_Music");
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void Play()
    {
        StartCoroutine(PlayRoutine());
        
    }

    public IEnumerator PlayRoutine()
    {
        FindObjectOfType<GameManager>().FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<GameManager>().ResetFade();
        MainImage.SetActive(false);
        Button.SetActive(false);
        SceneManager.LoadScene("Level1");
        
    }
}
