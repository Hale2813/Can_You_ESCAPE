using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameoverManager : MonoBehaviour
{
    public GameObject buttonRetry;     //�{�^���F���g���C
    // Start is called before the first frame update
    void Start()
    {
        buttonRetry.SetActive(true);       //��b��\�� 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushButtonRetry()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
