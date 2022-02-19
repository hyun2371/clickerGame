using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGame : MonoBehaviour
{
    public long money;  // ���� ������
    public long moneyIncreaseAmount; // ������ ������
    public long autoIncreaseAmount; // �ڵ� ������ ������

    public Text textmoney;  // �ؽ�Ʈ ǥ���ϴ� �κ�
    public Text automoney;  // �ڵ� ������ ǥ��

    effectSoundManager effectSM;

    void Start()
    {
        money = 0;
        moneyIncreaseAmount = 1;
        autoIncreaseAmount = 0;

        StartCoroutine(co_timer());     //������ �ڵ� ���� �Լ�

        effectSM = GameObject.Find("Script").GetComponent<effectSoundManager>();
    }
    void Update()
    {
        ShowInfo();         // ������ ǥ�� �Լ�
        MoneyIncrease();    // ������ ���� �Լ�
    }

    // 1. ������ ���� �Լ�
    void MoneyIncrease()
    {
        if (Input.GetMouseButtonDown(0))    // Ŭ�� �Է� ������
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   // UI ��ġ==false�� (UI�� ��ġ�� �� �ƴ϶��)
                money += moneyIncreaseAmount;    // ������=������+������
                effectSM.PlaySound("Touch");
            }
        }
    }

    // ������ �ڵ� ���� �Լ�
    IEnumerator co_timer()
    {
        while (true)
        {
            if (autoIncreaseAmount > 0)
            {
                money += autoIncreaseAmount; //�������� 0���� ū ���, ��������ŭ �����ֱ�.
            }
            yield return new WaitForSeconds(1f); //1�� ������ �ֱ�
        }
    }


    // 2-1. ������ ǥ�� �Լ�
    void ShowInfo()
    {
        long kmoney = money / 1000;
        long kmoney2 = money % 1000;
        long mmoney = money / 1000000;
        long mmoney2 = money % 1000000 / 100000;
        if (money == 0) // ���� 0�̸� "0 ����" ǥ��
            textmoney.text = "0";
        else
        {
            if (kmoney!=0 && mmoney==0)
            {
                textmoney.text = GetThousandCommaText(kmoney)+"."+kmoney2.ToString()+"K";
            }
            else if (mmoney != 0)
            {
                textmoney.text = GetThousandCommaText(mmoney) + "."+mmoney2.ToString()+"M";
            }
            else
                textmoney.text = GetThousandCommaText(money);
        }
        if (autoIncreaseAmount == 0)
            automoney.text = "+ 0";
        else
            automoney.text = "+ "+GetThousandCommaText(autoIncreaseAmount);
    }
    // 2-2. õ������ �޸�(,) ����ִ� ���Խ�
    public string GetThousandCommaText(long data)
    {
        return string.Format("{0:#,###}", data);
    }
}
