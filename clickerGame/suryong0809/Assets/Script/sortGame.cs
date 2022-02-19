using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;




public class sortGame : MonoBehaviour
{
    System.Random rand;
    int[] randArr = new int[5];
    int[] ansArr = new int[5];
    public Text explain;
    public Text[] randText = new Text[5];
    //public Text[] ansText = new Text[5];
    public List<int> userData;
    int count = 0;
    //������ �ش� ���� Ŭ���ϸ� user�迭�� ��⵵�� �Ѵ�.
    public Text alert;
    public Text userText;


    //�ٸ� ���� ���� ��� �������� ����!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    // Start is called before the first frame update
    void Start()
    {

        // �ؽ�Ʈ ���. ���� ���.
        explain.text = "�Ʒ� ���ڵ��� ���� ���ں���\n������� ��ġ�ϼ���!";
        userText.text = "�Է� : ";
        alert.text = "";

        makeRand();
        sortRand();
        showGame();
            
    }

    public void makeRand()
    {
        //���� ���� �ֱ�
        rand = new System.Random();
        
        
        // �������� �ε��� �����ؼ�(0~99) tmp���� ������!
        for(int i = 0; i < 5; i++)
        {
            randArr[i] = noDup();
            //0~99������ �ߺ� ���� ���� ������!
            
        }
    }

    private int noDup()
    {
        rand = new System.Random();
        int tmp = rand.Next() % 100;
        if (randArr.Contains(tmp))
        {
            tmp=noDup();
        }
        return tmp;
       
    }


    //���� ������ ���� ���� �� ����!
    public void sortRand()
    {
        for(int i = 0; i < 5; i++)
        {
            ansArr[i] = randArr[i];
        }
        Array.Sort(ansArr);
    }

    //�� ��ư�� �ؽ�Ʈ�� ��´�
    public void showGame()
    {
        
        //�ؽ�Ʈ ���
        for(int i = 0; i < 5; i++)
        {
            randText[i].text = randArr[i].ToString();
        }
    }

    //������ ���� Ŭ���� �迭�� ��⵵�� 
    public void inputData()
    {
        if (count < 5)
        {
            count++;
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;

            switch (Convert.ToInt32(clickObject.name))
            {
                case 1:
                    userData.Add(randArr[0]);
                    userText.text = userText.text + " "+randArr[0];
                    break;
                case 2:
                    userData.Add(randArr[1]);
                    userText.text = userText.text + " " + randArr[1];
                    break;
                case 3:
                    userData.Add(randArr[2]);
                    userText.text = userText.text + " " + randArr[2];
                    break;
                case 4:
                    userData.Add(randArr[3]);
                    userText.text = userText.text + " " + randArr[3];
                    break;
                case 5:
                    userData.Add(randArr[4]);
                    userText.text = userText.text + " " + randArr[4];
                    break;
            }

            Invoke("checkAns", 1.6f);

        }
    }

    //����� ��. ������ ���� �޼��� ��� �� ����. Ʋ���� Ʋ�Ƚ��ϴ� �޼��� ���
    public void checkAns()
    {
        bool checkBreak = false;
        if (count == 5)
        {
            for(int i = 0; i < 5; i++)
            {
                if (userData[i] != ansArr[i])
                {
                    alert.text = "Ʋ�Ƚ��ϴ�.\n�ٽ� ������ ������!";

                    for (int j = 0; j < 10000; j++) ; //�ð��� ��

                    count = 0;
                    userData.Clear();
                    userText.text = "�Է� : ";
                    checkBreak = true;
                    break;
                }
            }
            if (!checkBreak)
            {
                alert.text = "�����߽��ϴ�!\n100 ������ ȹ���Ͽ����ϴ�.";
                

                Invoke("changeScene", 1f);
            }
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene("1main");
    }
    

        // Update is called once per frame
    void Update()
    {

    }
}

