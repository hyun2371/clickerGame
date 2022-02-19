using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    float speed = 0;
    private int finalAngle;
    [SerializeField]
    public Text winText;
    bool isPressed = false;
    long reward;
    int locked = 1;
    InGame ingame;

    void Start()
    {
        ingame = GameObject.Find("Script").GetComponent<InGame>();
    }
    void Update()
    {
        playspin();
        showReward();
    }
    void playspin()
    {
        if (isPressed) //��ư ������
        {
            Debug.Log("�÷��̽���");
            this.speed = Random.Range(30, 70); //�ش� ���� ���� �귿�� �ȵ��ư�
            locked--;
        }
        transform.Rotate(0, 0, this.speed);
        this.speed *= 0.97f;

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        switch (finalAngle)
        {
            case 0:
                reward = 1000;
                break;
            case 60:
                reward = 2000;
                break;
            case 120:
                reward = 3000;
                break;
            case 180:
                reward = 4000;
                break;
            case 240:
                reward = 5000;
                break;
            case 300:
                reward = 6000;
                break;
        }
        isPressed = false;
        //Debug.Log(speed); �ӵ��� -E���� ���� ���Ѵ�� ����
        if (this.speed <= 0.2 && locked == 0) //�ӵ�-�귿�� ������� ��������+ locked ������ ����(�� �� Ŭ���� �� ��ȭ �ѹ��̵���)
        {
            if (ingame)
            {
                for (int i = 0; i <= 2000; i++) ; //�����ð� ���
                ingame.money += reward;
                locked--;
            }

        }
    }
    void showReward()
    {
        winText.text = "You Win " + reward / 1000 + "K";
        if (ingame.money < 3000)
            winText.text = "������ ����";
    }
    public void ButtonPress() //start��ư ���� �� ȣ��
    {
        if (ingame)
        {
            Debug.Log("��ư����");
            isPressed = true;

            if (ingame.money < 3000)
            {
                isPressed = false;
            }

            else
            {
                ingame.money -= 3000;
            }
            locked = 1; //�ٽ� �� ���� ����(��ư �ѹ� Ŭ���� �ѹ��� �� ����)
        }
    }
}
