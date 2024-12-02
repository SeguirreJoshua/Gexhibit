using System.Collections;
using UnityEngine;

public class SplashToGame : MonoBehaviour
{

    public GameObject bgm;
    public GameObject splashBack;
    public GameObject splashText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SplashEnd());
       
    }
     IEnumerator SplashEnd(){

        yield return new WaitForSeconds(4);
        bgm.SetActive(true);
        yield return new WaitForSeconds(1);
        splashText.SetActive(false);
        splashBack.SetActive(false);
     }
    // Update is called once per frame
    
}
