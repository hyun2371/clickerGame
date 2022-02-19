using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{

    public long pCrystal;
    public long pRandom;
    public long pClick;
    public long pFever;
    public long pCats;

    public Text alertmsg;

    InGame ingame;
    changeSuryong changesuryong;
    effectSoundManager effectSM;
    int rnum;
    public int numOfCats;
    public GameObject[] cats = new GameObject[3]; //����� �� ���� �����ϱ�!
    public GameObject alert;
    public GameObject fevertime;
    bool isFever;
    long tmp;
    public GameObject shopobj;

    public Text pCrystalText;
    public Text pRandomText;
    public Text pClickText;
    public Text pFeverText;
    public Text pCatsText;

    void Start()
    {

        pCrystal = 10;   // ó�� ���� ���� ����. ���߿� ���� �ʿ�
        pRandom = 10;
        pClick = 10;
        pFever = 10;
        pCats = 50;
        numOfCats = 3;
        isFever = false;

        // �ٸ� ��ũ��Ʈ ���� �����ϱ� ���� �ڵ� 2��
        ingame = GameObject.Find("Script").GetComponent<InGame>();
        changesuryong = GameObject.Find("Script").GetComponent<changeSuryong>();
        effectSM = GameObject.Find("Script").GetComponent<effectSoundManager>();

    }

    void Update()
    {
        showshopinfo();
        displaycat();
    }
    void showshopinfo()
    {
        pCrystalText.text = GetThousandCommaText(pCrystal) + "����";
        pRandomText.text = GetThousandCommaText(pRandom) + "����";
        pClickText.text = GetThousandCommaText(pClick) + "����";
        pFeverText.text = GetThousandCommaText(pFever) + "����";
        pCatsText.text = GetThousandCommaText(pCats) + "����";
    }
    public string GetThousandCommaText(long data)
    {
        return string.Format("{0:#,###}", data);
    }

    public void shopCrystal()   // ������ ����
    {
        if (ingame && changesuryong)    // ���� ����
        {
            if (ingame.money >= pCrystal && changesuryong.ind <= 8 && !changesuryong.haveADrink)
            {
                ingame.money -= pCrystal;
                changesuryong.ind++;
                pCrystal *= 5;   // ���� ����, ���� ���� �ʿ�
                shopobj.SetActive(false);
                effectSM.PlaySound("LvUP");
            }
            else if (changesuryong.haveADrink)
            {
                alert.SetActive(true);
                alertmsg.text = "�����̸� �����·� �����־�� �մϴ�.";
                effectSM.PlaySound("Alert");
            }
            else if (ingame.money<pCrystal)
            {
                alert.SetActive(true);
                alertmsg.text = "�������� �����մϴ�.";
                effectSM.PlaySound("Alert");
            }
            else
            {
                alert.SetActive(true);
                alertmsg.text = "�������� ������ �� �����ϴ�.";
                effectSM.PlaySound("Alert");
            }

        }
    }

    public void shopClick() // Ŭ�� ����
    {
        if (ingame)
        {
            if (ingame.money >= pClick)
            {
                ingame.money -= pClick;
                ingame.moneyIncreaseAmount *= 2;   // ���� ���� �ʿ�
                pClick *= 2; // �������� �����ʿ�
                pFever = pClick;
                effectSM.PlaySound("Buy");
            }
            else
            {
                alert.SetActive(true);
                alertmsg.text = "�������� �����մϴ�.";
                effectSM.PlaySound("Alert");
            }
        }
    }

    public void shopRandom()    // �������� ����
    {
        if (ingame && changesuryong)
        {
            rnum = Random.Range(0, 10); // ���� ���� ����
            if (ingame.money >= pRandom && changesuryong.ind <= 8 && !changesuryong.haveADrink)
            {
                ingame.money -= pRandom;
                pRandom += 5; // �������� �����ʿ�

                if (rnum == 1)  // 1�� ���� ���׷��̵�(10�ۼ�Ʈ Ȯ��)
                {
                    changesuryong.haveADrink = false;
                    changesuryong.limit = 0;
                    changesuryong.ind++;
                    effectSM.PlaySound("LvUP");
                }
                else
                {
                    changesuryong.haveADrink = true;
                    changesuryong.limit = 0;
                    effectSM.PlaySound("Random");
                }
                shopobj.SetActive(false);
            }
            else if (changesuryong.haveADrink)
            {
                alert.SetActive(true);
                alertmsg.text = "�����̸� �����·� �����־�� �մϴ�.";
                effectSM.PlaySound("Alert");
            }
            else if (ingame.money < pRandom)
            {
                alert.SetActive(true);
                alertmsg.text = "�������� �����մϴ�.";
                effectSM.PlaySound("Alert");
            }
            else
            {
                alert.SetActive(true);
                alertmsg.text = "�������� ������ �� �����ϴ�.";
                effectSM.PlaySound("Alert");
            }
        }
    }


    public void shopFever() // �ǹ� ����
    {
        
        if (ingame)
        {
            if (ingame.money >= pFever && !isFever)
            {
                ingame.money -= pFever;
                isFever = true;     // ���� fever������ (�ǹ� ���¿��� �� ������ �����ϴ� �� ����)
                tmp = ingame.moneyIncreaseAmount;
                ingame.moneyIncreaseAmount *= 3; //3���!!
                shopobj.SetActive(false);
                fevertime.SetActive(true);
                

                 Invoke("endFever", 10f);     // 10�� �ð� ���� �� endFever ����.
                 
            }
            else if (ingame.money < pFever)
            {
                alert.SetActive(true);
                alertmsg.text = "�������� �����մϴ�.";
                effectSM.PlaySound("Alert");
            }
            else if (isFever)
            {
                alert.SetActive(true);
                alertmsg.text = "�ǹ�Ÿ�� �߿��� ������ �� �����ϴ�.";
                effectSM.PlaySound("Alert");
            }
        }
    }

    private void endFever() {
        fevertime.SetActive(false);
        ingame.moneyIncreaseAmount = tmp; //�ٽ� ���� ���������� �ǵ���.
        isFever = false;    // Fever���� ����
    }


    public void shopCat()
    {
        if (ingame)
        {
            if (ingame.money >= pCats) //���� �����Ҹ�ŭ ���� �ִ��� �˻�.
            {
                if (numOfCats > 0) //����̴� 3�������� ���� ������! ó�� numOfCats���� 3.
                {
                    // 1. ����� ǥ��
                    // cats[numOfCats - 1].SetActive(true); 
                    //numofCats�� �ε����� Ȱ��.
                    // 3���� �����ؼ� 2, 1 �̷��� �پ��ϱ�
                    // cats[2]�� ���� ���� ǥ�õǰ� �� ���� cats[1], �� ���� cats[0] ����

                    // 2. ������ �ڵ� ����
                    ingame.autoIncreaseAmount += 10; 
                    //����� �ϳ��� 2�� �����ϵ���.
                    //����� �� �� �� �����ϸ� ������ �� 6��ŭ ����.


                    numOfCats--; //�ϳ� ���������ϱ� ����� ���� ���ҽ�Ű��.
                    ingame.money -= pCats; //������ ���ҽ�Ű��

                    pCats += 10; //���� ������ 5������ �� ������ 15. �� ������ 25!
                }
                else
                {
                    //����� ���� �� �ؼ� ���� �Ұ����ϴٴ� �˾�â ����
                    alert.SetActive(true);
                    alertmsg.text = "���̻� ����̸� Ű�� �� �����ϴ�.";
                    effectSM.PlaySound("Alert");
                }
            }
            else
            {
                alert.SetActive(true);
                alertmsg.text = "�������� �����մϴ�.";
                effectSM.PlaySound("Alert");
            }
        }
    }
    void displaycat()
    {
        if (numOfCats<3 && numOfCats >= 0)
        {
            for (int i=3; i>numOfCats; i--)
            {
                cats[i-1].SetActive(true);
            }
        }
        if (numOfCats >= 3)
            for (int i = 0; i > 3; i++)
                cats[i].SetActive(false);
    }

}
