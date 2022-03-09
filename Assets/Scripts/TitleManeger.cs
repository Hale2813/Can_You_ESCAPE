using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManeger : MonoBehaviour
{
    public AudioClip startSE;       //効果音：スタート
    private AudioSource audioSource;        //オーディオソース
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();       //オーディオソースを取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushStartButton()
    {
        audioSource.PlayOneShot(startSE); //スタート音再生
        StartCoroutine("LoadGameScene");
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(1.95f);
        SceneManager.LoadScene("GameScene");
    }
}
