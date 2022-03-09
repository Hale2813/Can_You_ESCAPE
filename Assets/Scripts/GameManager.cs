using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int WALL_FRONT = 1;
    public const int WALL_RIGHT = 2;
    public const int WALL_BACK = 3;
    public const int WALL_LEFT = 4;

    public const int COLOR_GREEN = 0;
    public const int COLOR_RED = 1;
    public const int COLOR_BLUE = 2;
    public const int COLOR_WHITE = 3;

    public GameObject paneWalls;    //壁全体

    public GameObject buttonHammer;     //ボタン：トンカチ
    public GameObject buttonKey1;        //ボタン：鍵１
    public GameObject buttonKey2;        //ボタン：鍵２
    public GameObject buttonMemo;        //ボタン：メモ
    public GameObject buttonMemoIcon;     //ボタン：メモアイコン

    public GameObject imageHammerIcon;     //アイコン：トンカチ
    public GameObject imageKey1Icon;     //アイコン：鍵１
    public GameObject imageKey2Icon;     //アイコン：鍵２
    public GameObject imageMemoIcon;     //アイコン：メモ

    public GameObject buttonPig;        //ボタン：ブタの貯金箱
    public GameObject buttonBook;        //ボタン：本
    public GameObject buttonDoor;     //ボタン：ドア

    public GameObject buttonMessage;    //ボタン：メッセージ
    public GameObject buttonMessageText;    //メッセージテキスト

    public GameObject[] buttonLamp = new GameObject[3];     //ボタン：金庫

    public Sprite[] buttonPicture = new Sprite[4];      //ボタンの絵

    public Sprite HammerPicture;        //トンカチの絵
    public Sprite Key1Picture;       //鍵１の絵
    public Sprite Key2Picture;       //鍵２の絵
    public Sprite MemoPicture;       //メモの絵

    private int wallNo;     //現在向いている方向
    private bool doesHaveHammer;        //トンカチを持っているか
    private bool doesHaveKey1;       //鍵１を持っているか
    private bool doesHaveKey2;       //鍵２を持っているか
    private bool doesHaveMemo;       //メモを持っているか

    private int[] buttonColor = new int[3];     //金庫のボタン

    public AudioClip breakSE;       //効果音：破壊
    public AudioClip swichSE;       //効果音：スイッチ
    public AudioClip bottonSE;       //効果音：ボタン
    public AudioClip messageSE;       //効果音：メッセージ
    public AudioClip doorSE;       //効果音：ドア
    public AudioClip boxSE;       //効果音：箱
    private AudioSource audioSource;        //オーディオソース
    bool isAudioStart = false; //曲再生の判定

    // Start is called before the first frame update
    void Start()
    {
        wallNo = WALL_FRONT;    //スタートは前   
        doesHaveHammer = false;     //トンカチは持っていない
        doesHaveKey1 = false;        //鍵１は持っていない
        doesHaveKey2 = false;        //鍵２は持っていない
        doesHaveMemo = false;        //メモは持っていない

        buttonColor[0] = COLOR_GREEN;
        buttonColor[1] = COLOR_RED;
        buttonColor[2] = COLOR_BLUE;

        audioSource = gameObject.GetComponent<AudioSource>();       //オーディオソースを取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ドアをタップ
    public void PushButtonDoor()
    {
        if (doesHaveKey2 == false && doesHaveKey1 == false)
        {
            //鍵を持っていない 
            DisplayMessage("鍵がかかっている。");
        }
        else if(doesHaveKey2 == false && doesHaveKey1 == true)
        {
            //違う鍵を持っている 
            DisplayMessage("この鍵では開かないようだ。");
            StartCoroutine("LoadGameoverScene");          
        }
        else if (doesHaveKey2 == true && doesHaveKey1 == true)
        {
            //鍵を持っている 
            audioSource.PlayOneShot(doorSE); //音再生
            isAudioStart = true;//曲の再生を判定
            SceneManager.LoadScene("ClearScene");
        }
    }

    //ボックスをタップ 
    public　void PushButtonBox()
    {
        if (doesHaveKey1 == false)
        {
            //鍵を持っていない 
            DisplayMessage("鍵がかかっている。");
        }
        else if (doesHaveKey2 == false && doesHaveKey1 == true)
        {
            //鍵を持っている 
            audioSource.PlayOneShot(boxSE); //音再生
            isAudioStart = true;//曲の再生を判定
            DisplayMessage("箱の中に鍵が入っていた。");
            buttonKey2.SetActive(true);       //鍵２の絵を表示 
            imageKey2Icon.GetComponent<Image>().sprite = Key2Picture;
            doesHaveKey2 = true;
        }
    }

    //金庫のボタン１をタップ 
    public void PushButtonLamp1()
    {
        audioSource.PlayOneShot(swichSE); //音再生
        isAudioStart = true;//曲の再生を判定
        ChangeButtonColor(0);
    }

    //金庫のボタン2をタップ 
    public void PushButtonLamp2()
    {
        audioSource.PlayOneShot(swichSE); //音再生
        isAudioStart = true;//曲の再生を判定
        ChangeButtonColor(1);
    }

    //金庫のボタン3をタップ 
    public void PushButtonLamp3()
    {
        audioSource.PlayOneShot(swichSE); //音再生
        isAudioStart = true;//曲の再生を判定
        ChangeButtonColor(2);
    }

    //金庫のボタンの色の変更 
    void ChangeButtonColor(int buttonNo)
    {
        buttonColor[buttonNo]++;
        //白の時は緑に 
        if (buttonColor[buttonNo] > COLOR_WHITE)
        {
            buttonColor[buttonNo] = COLOR_GREEN;
        }
        //ボタンの画像を変更 
        buttonLamp[buttonNo].GetComponent<Image>().sprite = buttonPicture[buttonColor[buttonNo]];

        //ボタンの色順をチェック 
        if ((buttonColor[0] == COLOR_BLUE) && (buttonColor[1] == COLOR_WHITE) && (buttonColor[2] == COLOR_RED))
        {
            //まだトンカチを持っていない 
            if (doesHaveHammer == false)
            {
                audioSource.PlayOneShot(boxSE); //音再生
                isAudioStart = true;//曲の再生を判定
                DisplayMessage("金庫の中にトンカチが入っていた。");
                buttonHammer.SetActive(true);       //トンカチの絵を表示 
                imageHammerIcon.GetComponent<Image>().sprite = HammerPicture;
                doesHaveHammer = true;
            }
        }
    }

    //本をタップ 
    public void PushButtonBook()
    {
        if (doesHaveMemo == false)
        {
            DisplayMessage("本の間からメモが出てきた。\n”パンがなければケーキを食べればいいじゃない”\nと書いてある。");
            buttonMemo.SetActive(true);       //メモの絵を表示
            buttonMemoIcon.SetActive(true);       //メモのアイコンを表示
            doesHaveMemo = true;
        }
    }

    //貯金箱をタップ
    public void PushButtonPig()
    {
        //トンカチを持っているか
        if (doesHaveHammer == false)
        {
            //トンカチを持っていない
            DisplayMessage("素手では割れない。");
        }
        else
        {
            //トンカチを持っている
            audioSource.PlayOneShot(breakSE); //音再生
            isAudioStart = true;//曲の再生を判定
            if (isAudioStart == true)
            //曲の再生が開始されている時
            {
                DisplayMessage("貯金箱が割れて中から鍵が出てきた。");
                buttonPig.SetActive(false);     //貯金箱を消す
                buttonKey1.SetActive(true);      //鍵１を表示
                imageKey1Icon.GetComponent<Image>().sprite = Key1Picture;
                doesHaveKey1 = true;
            }
        }
    }

    //トンカチの絵をタップ
    public void PushButtonHammer()
    {
        buttonHammer.SetActive(false);
    }

    //鍵１の絵をタップ
    public void PushButtonKey1()
    {
        buttonKey1.SetActive(false);
    }

    //鍵２の絵をタップ
    public void PushButtonKey2()
    {
        buttonKey2.SetActive(false);
    }

    //メモの絵をタップ
    public void PushButtonMemo()
    {
        buttonMemo.SetActive(false);
    }

    //メモのアイコンをタップ
    public void PushButtonMemoIcon()
    {
        if (doesHaveMemo == true)
        {
            DisplayMessage("”パンがなければケーキを食べればいいじゃない”\nと書いてある。");
        }
    }

    //メッセージをタップ
    public void PushButtonMessage()
    {
        buttonMessage.SetActive(false);     //メッセージを消す
    }

    //右ボタン
    public void PushButtonRight()
    {
        audioSource.PlayOneShot(bottonSE); //音再生
        isAudioStart = true;//曲の再生を判定

        if (isAudioStart == true)
        //曲の再生が開始されている時
        {
            wallNo++;   //方向を一つ右に
                        //左の一つ右は前
            if (wallNo > WALL_LEFT)
            {
                wallNo = WALL_FRONT;
            }
            DisplayWall();  //壁表示更新
            ClearButtons();     //いらないものを消す
        }      
    }

    //左ボタン
    public void PushButtonLeft()
    {
        audioSource.PlayOneShot(bottonSE); //音再生
        isAudioStart = true;//曲の再生を判定

        if (isAudioStart == true)
        //曲の再生が開始されている時
        {
            wallNo--;   //方向を一つ左に
                        //前の一つ右は左
            if (wallNo < WALL_FRONT)
            {
                wallNo = WALL_LEFT;
            }
            DisplayWall();  //壁表示更新
            ClearButtons();     //いらないものを消す
        }
    }

    //各表示をクリア
    void ClearButtons()
    {
        buttonHammer.SetActive(false);
        buttonKey1.SetActive(false);
        buttonKey2.SetActive(false);
        buttonMemo.SetActive(false);
        buttonMessage.SetActive(false);
    }

    //メッセージを表示
    void DisplayMessage(string mes)
    {
        audioSource.PlayOneShot(messageSE); //音再生
        isAudioStart = true;//曲の再生を判定
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text>().text = mes;
    }

    //向いている壁を表示
    void DisplayWall()
    {
        switch (wallNo)
        {
            case WALL_FRONT:
                paneWalls.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case WALL_RIGHT:
                paneWalls.transform.localPosition = new Vector3(-2000.0f, 0.0f, 0.0f);
                break;
            case WALL_BACK:
                paneWalls.transform.localPosition = new Vector3(-4000.0f, 0.0f, 0.0f);
                break;
            case WALL_LEFT:
                paneWalls.transform.localPosition = new Vector3(-6000.0f, 0.0f, 0.0f);
                break;
        }
    }

    IEnumerator LoadGameoverScene()
    {
        yield return new WaitForSeconds(1.95f);
        SceneManager.LoadScene("GameoverScene");
    }
}
