using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManeger : MonoBehaviour
{
    public AudioClip startSE;       //���ʉ��F�X�^�[�g
    private AudioSource audioSource;        //�I�[�f�B�I�\�[�X
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();       //�I�[�f�B�I�\�[�X���擾
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushStartButton()
    {
        audioSource.PlayOneShot(startSE); //�X�^�[�g���Đ�
        StartCoroutine("LoadGameScene");
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(1.95f);
        SceneManager.LoadScene("GameScene");
    }
}
