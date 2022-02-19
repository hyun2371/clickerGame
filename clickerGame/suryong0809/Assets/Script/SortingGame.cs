using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class SortingGame : MonoBehaviour
{
    System.Random rand;
    int[] randArr = new int[5];
    int[] ansArr = new int[5];
    public Text[] randText = new Text[5];
    //public Text[] ansText = new Text[5];
    public List<int> userData;
    int count = 0;
    //������ �ش� ���� Ŭ���ϸ� user�迭�� ��⵵�� �Ѵ�.
    public Text alert;      // �˸� �ؽ�Ʈ
    public Text userText;   // ����� �Է� �ؽ�Ʈ


    InGame ingame;
    public GameObject popgame;

    void Start()
    {
        ingame = GameObject.Find("Script").GetComponent<InGame>();

        // �ؽ�Ʈ ���
        userText.text = "�Է�: ";
        alert.text = "";

        makeRand(); // ���� ����
        sortRand(); // ���� ����
        showGame();
    }


    // ���� ����
    public void makeRand()
    {
        //���� ���� �ֱ�
        rand = new System.Random();
        // �������� �ε��� �����ؼ�(0~99) tmp���� ������!
        for (int i = 0; i < 5; i++)
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
            tmp = noDup();
        }
        return tmp;
    }


    //���� ������ ���� ���� �� ����
    public void sortRand()
    {
        for (int i = 0; i < 5; i++)
        {
            ansArr[i] = randArr[i];
        }
        Array.Sort(ansArr);
    }


    //�� ��ư�� �ؽ�Ʈ�� ��´�
    public void showGame()
    {
        //�ؽ�Ʈ ���
        for (int i = 0; i < 5; i++)
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
                    userText.text = userText.text + " " + randArr[0];
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

            if (count==5)
                Invoke("checkAns", 1.6f);

        }
    }
    //����� ��. ������ ���� �޼��� ��� �� ����. Ʋ���� Ʋ�Ƚ��ϴ� �޼��� ���
    public void checkAns()
    {
        bool checkBreak = false;
        if (count == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (userData[i] != ansArr[i])
                {
                    alert.text = "<color=#6B6FAE>Ʋ�Ƚ��ϴ�.\n�ٽ� ������ ������!</color>";

                    for (int j = 0; j < 10000; j++) ; //�ð��� ��

                    count = 0;
                    userData.Clear();
                    userText.text = "�Է�: ";
                    checkBreak = true;
                    break;
                }
            }

            if (!checkBreak)
            {
                alert.text = "<color=#6B6FAE>�����߽��ϴ�!\n100 ������ ȹ���Ͽ����ϴ�.</color>";
                Invoke("exitgame", 1f);
            }
        }
    }
    void exitgame()
    {
        ingame.money += 100;
        popgame.SetActive(false);
        userText.text = "�Է�: ";
        alert.text = "";

        makeRand(); // ���� ����
        sortRand(); // ���� ����
        showGame();
        count = 0;
    }

}
