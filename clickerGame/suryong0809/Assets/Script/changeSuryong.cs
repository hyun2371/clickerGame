using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class changeSuryong : MonoBehaviour
{
    //������ �̹��� 11�� (������ �̹����� ����� ����)
    public GameObject[] states = new GameObject[11];

    public GameObject limitbutton;

    public int ind = 0;
    public bool haveADrink = false;
    public int limit = 0;

    void Update()
    {
        // ������ ������Ʈ
        for (int i = 0; i < 11; i++)
        {
            if (i == ind)
                states[i].SetActive(true);
            else
                states[i].SetActive(false);
        }

        if (haveADrink) // haveADrink �������� ��
        {
            limitbutton.SetActive(true);
            //�������� ind������ 0~9����. 10�� �̻��� ���� �Ծ��� ��.

            if (limit < 15)
            {
                for (int i = 0; i < 10; i++)
                    states[i].SetActive(false);
                states[10].SetActive(true);
            }
            else
            {
                haveADrink = false;
                limit = 0;
            }
        }
        else
            limitbutton.SetActive(false);

    }
    public void touchlimit()
    {
        limit++;
    }
}
