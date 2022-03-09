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

    public GameObject paneWalls;    //�ǑS��

    public GameObject buttonHammer;     //�{�^���F�g���J�`
    public GameObject buttonKey1;        //�{�^���F���P
    public GameObject buttonKey2;        //�{�^���F���Q
    public GameObject buttonMemo;        //�{�^���F����
    public GameObject buttonMemoIcon;     //�{�^���F�����A�C�R��

    public GameObject imageHammerIcon;     //�A�C�R���F�g���J�`
    public GameObject imageKey1Icon;     //�A�C�R���F���P
    public GameObject imageKey2Icon;     //�A�C�R���F���Q
    public GameObject imageMemoIcon;     //�A�C�R���F����

    public GameObject buttonPig;        //�{�^���F�u�^�̒�����
    public GameObject buttonBook;        //�{�^���F�{
    public GameObject buttonDoor;     //�{�^���F�h�A

    public GameObject buttonMessage;    //�{�^���F���b�Z�[�W
    public GameObject buttonMessageText;    //���b�Z�[�W�e�L�X�g

    public GameObject[] buttonLamp = new GameObject[3];     //�{�^���F����

    public Sprite[] buttonPicture = new Sprite[4];      //�{�^���̊G

    public Sprite HammerPicture;        //�g���J�`�̊G
    public Sprite Key1Picture;       //���P�̊G
    public Sprite Key2Picture;       //���Q�̊G
    public Sprite MemoPicture;       //�����̊G

    private int wallNo;     //���݌����Ă������
    private bool doesHaveHammer;        //�g���J�`�������Ă��邩
    private bool doesHaveKey1;       //���P�������Ă��邩
    private bool doesHaveKey2;       //���Q�������Ă��邩
    private bool doesHaveMemo;       //�����������Ă��邩

    private int[] buttonColor = new int[3];     //���ɂ̃{�^��

    public AudioClip breakSE;       //���ʉ��F�j��
    public AudioClip swichSE;       //���ʉ��F�X�C�b�`
    public AudioClip bottonSE;       //���ʉ��F�{�^��
    public AudioClip messageSE;       //���ʉ��F���b�Z�[�W
    public AudioClip doorSE;       //���ʉ��F�h�A
    public AudioClip boxSE;       //���ʉ��F��
    private AudioSource audioSource;        //�I�[�f�B�I�\�[�X
    bool isAudioStart = false; //�ȍĐ��̔���

    // Start is called before the first frame update
    void Start()
    {
        wallNo = WALL_FRONT;    //�X�^�[�g�͑O   
        doesHaveHammer = false;     //�g���J�`�͎����Ă��Ȃ�
        doesHaveKey1 = false;        //���P�͎����Ă��Ȃ�
        doesHaveKey2 = false;        //���Q�͎����Ă��Ȃ�
        doesHaveMemo = false;        //�����͎����Ă��Ȃ�

        buttonColor[0] = COLOR_GREEN;
        buttonColor[1] = COLOR_RED;
        buttonColor[2] = COLOR_BLUE;

        audioSource = gameObject.GetComponent<AudioSource>();       //�I�[�f�B�I�\�[�X���擾
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�h�A���^�b�v
    public void PushButtonDoor()
    {
        if (doesHaveKey2 == false && doesHaveKey1 == false)
        {
            //���������Ă��Ȃ� 
            DisplayMessage("�����������Ă���B");
        }
        else if(doesHaveKey2 == false && doesHaveKey1 == true)
        {
            //�Ⴄ���������Ă��� 
            DisplayMessage("���̌��ł͊J���Ȃ��悤���B");
            StartCoroutine("LoadGameoverScene");          
        }
        else if (doesHaveKey2 == true && doesHaveKey1 == true)
        {
            //���������Ă��� 
            audioSource.PlayOneShot(doorSE); //���Đ�
            isAudioStart = true;//�Ȃ̍Đ��𔻒�
            SceneManager.LoadScene("ClearScene");
        }
    }

    //�{�b�N�X���^�b�v 
    public�@void PushButtonBox()
    {
        if (doesHaveKey1 == false)
        {
            //���������Ă��Ȃ� 
            DisplayMessage("�����������Ă���B");
        }
        else if (doesHaveKey2 == false && doesHaveKey1 == true)
        {
            //���������Ă��� 
            audioSource.PlayOneShot(boxSE); //���Đ�
            isAudioStart = true;//�Ȃ̍Đ��𔻒�
            DisplayMessage("���̒��Ɍ��������Ă����B");
            buttonKey2.SetActive(true);       //���Q�̊G��\�� 
            imageKey2Icon.GetComponent<Image>().sprite = Key2Picture;
            doesHaveKey2 = true;
        }
    }

    //���ɂ̃{�^���P���^�b�v 
    public void PushButtonLamp1()
    {
        audioSource.PlayOneShot(swichSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�
        ChangeButtonColor(0);
    }

    //���ɂ̃{�^��2���^�b�v 
    public void PushButtonLamp2()
    {
        audioSource.PlayOneShot(swichSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�
        ChangeButtonColor(1);
    }

    //���ɂ̃{�^��3���^�b�v 
    public void PushButtonLamp3()
    {
        audioSource.PlayOneShot(swichSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�
        ChangeButtonColor(2);
    }

    //���ɂ̃{�^���̐F�̕ύX 
    void ChangeButtonColor(int buttonNo)
    {
        buttonColor[buttonNo]++;
        //���̎��͗΂� 
        if (buttonColor[buttonNo] > COLOR_WHITE)
        {
            buttonColor[buttonNo] = COLOR_GREEN;
        }
        //�{�^���̉摜��ύX 
        buttonLamp[buttonNo].GetComponent<Image>().sprite = buttonPicture[buttonColor[buttonNo]];

        //�{�^���̐F�����`�F�b�N 
        if ((buttonColor[0] == COLOR_BLUE) && (buttonColor[1] == COLOR_WHITE) && (buttonColor[2] == COLOR_RED))
        {
            //�܂��g���J�`�������Ă��Ȃ� 
            if (doesHaveHammer == false)
            {
                audioSource.PlayOneShot(boxSE); //���Đ�
                isAudioStart = true;//�Ȃ̍Đ��𔻒�
                DisplayMessage("���ɂ̒��Ƀg���J�`�������Ă����B");
                buttonHammer.SetActive(true);       //�g���J�`�̊G��\�� 
                imageHammerIcon.GetComponent<Image>().sprite = HammerPicture;
                doesHaveHammer = true;
            }
        }
    }

    //�{���^�b�v 
    public void PushButtonBook()
    {
        if (doesHaveMemo == false)
        {
            DisplayMessage("�{�̊Ԃ��烁�����o�Ă����B\n�h�p�����Ȃ���΃P�[�L��H�ׂ�΂�������Ȃ��h\n�Ə����Ă���B");
            buttonMemo.SetActive(true);       //�����̊G��\��
            buttonMemoIcon.SetActive(true);       //�����̃A�C�R����\��
            doesHaveMemo = true;
        }
    }

    //���������^�b�v
    public void PushButtonPig()
    {
        //�g���J�`�������Ă��邩
        if (doesHaveHammer == false)
        {
            //�g���J�`�������Ă��Ȃ�
            DisplayMessage("�f��ł͊���Ȃ��B");
        }
        else
        {
            //�g���J�`�������Ă���
            audioSource.PlayOneShot(breakSE); //���Đ�
            isAudioStart = true;//�Ȃ̍Đ��𔻒�
            if (isAudioStart == true)
            //�Ȃ̍Đ����J�n����Ă��鎞
            {
                DisplayMessage("������������Ē����献���o�Ă����B");
                buttonPig.SetActive(false);     //������������
                buttonKey1.SetActive(true);      //���P��\��
                imageKey1Icon.GetComponent<Image>().sprite = Key1Picture;
                doesHaveKey1 = true;
            }
        }
    }

    //�g���J�`�̊G���^�b�v
    public void PushButtonHammer()
    {
        buttonHammer.SetActive(false);
    }

    //���P�̊G���^�b�v
    public void PushButtonKey1()
    {
        buttonKey1.SetActive(false);
    }

    //���Q�̊G���^�b�v
    public void PushButtonKey2()
    {
        buttonKey2.SetActive(false);
    }

    //�����̊G���^�b�v
    public void PushButtonMemo()
    {
        buttonMemo.SetActive(false);
    }

    //�����̃A�C�R�����^�b�v
    public void PushButtonMemoIcon()
    {
        if (doesHaveMemo == true)
        {
            DisplayMessage("�h�p�����Ȃ���΃P�[�L��H�ׂ�΂�������Ȃ��h\n�Ə����Ă���B");
        }
    }

    //���b�Z�[�W���^�b�v
    public void PushButtonMessage()
    {
        buttonMessage.SetActive(false);     //���b�Z�[�W������
    }

    //�E�{�^��
    public void PushButtonRight()
    {
        audioSource.PlayOneShot(bottonSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�

        if (isAudioStart == true)
        //�Ȃ̍Đ����J�n����Ă��鎞
        {
            wallNo++;   //��������E��
                        //���̈�E�͑O
            if (wallNo > WALL_LEFT)
            {
                wallNo = WALL_FRONT;
            }
            DisplayWall();  //�Ǖ\���X�V
            ClearButtons();     //����Ȃ����̂�����
        }      
    }

    //���{�^��
    public void PushButtonLeft()
    {
        audioSource.PlayOneShot(bottonSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�

        if (isAudioStart == true)
        //�Ȃ̍Đ����J�n����Ă��鎞
        {
            wallNo--;   //�����������
                        //�O�̈�E�͍�
            if (wallNo < WALL_FRONT)
            {
                wallNo = WALL_LEFT;
            }
            DisplayWall();  //�Ǖ\���X�V
            ClearButtons();     //����Ȃ����̂�����
        }
    }

    //�e�\�����N���A
    void ClearButtons()
    {
        buttonHammer.SetActive(false);
        buttonKey1.SetActive(false);
        buttonKey2.SetActive(false);
        buttonMemo.SetActive(false);
        buttonMessage.SetActive(false);
    }

    //���b�Z�[�W��\��
    void DisplayMessage(string mes)
    {
        audioSource.PlayOneShot(messageSE); //���Đ�
        isAudioStart = true;//�Ȃ̍Đ��𔻒�
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text>().text = mes;
    }

    //�����Ă���ǂ�\��
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
