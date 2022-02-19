using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


//ingame�� ������ 3(�� long), suryong�� ������ 2��(int, bool), shop�� ������ 6��(�� int)

public class ingame
{
    public long money;
    public long increaseAmt;
    public long autoIncreaseAmt;

    public ingame(long money, long increaseAmt, long autoIncreaseAmt)
    {
        this.money = money;
        this.increaseAmt = increaseAmt;
        this.autoIncreaseAmt = autoIncreaseAmt;
    }
}
public class suryong
{
    public int ind;
    public bool isMonster;

    public suryong(int ind, bool isMonster)
    {
        this.ind = ind;
        this.isMonster = isMonster;
    }
}
public class shop
{
    public long crystalPrice;
    public long randomPrice;
    public long clickPrice;
    public long feverPrice;
    public long catPrice;
    public int numOfCats;

    public shop(long crystalPrice, long randomPrice, long clickPrice, long feverPrice, long catPrice, int numOfCats)
    {
        this.crystalPrice = crystalPrice;
        this.randomPrice = randomPrice;
        this.clickPrice = clickPrice;
        this.feverPrice = feverPrice;
        this.catPrice = catPrice;
        this.numOfCats = numOfCats;
    }
}

public class SLmanager : MonoBehaviour
{
    public ingame scriptIngame;
    public suryong scriptSuryong;
    public shop scriptShop;

    InGame ig;
    Shop sh;
    changeSuryong sr;

    void Start()
    {
        ig = GameObject.Find("Script").GetComponent<InGame>();
        sh = GameObject.Find("Script").GetComponent<Shop>();
        sr = GameObject.Find("Script").GetComponent<changeSuryong>();

        scriptIngame = new ingame(ig.money, ig.moneyIncreaseAmount, ig.autoIncreaseAmount);
        scriptSuryong = new suryong(sr.ind, sr.haveADrink);
        scriptShop = new shop(sh.pCrystal, sh.pRandom, sh.pClick, sh.pFever, sh.pCats,sh.numOfCats);

        if (File.Exists(Application.dataPath + "/Resources/ingameData.json") &&
            File.Exists(Application.dataPath + "/Resources/suryongData.json") &&
            File.Exists(Application.dataPath + "/Resources/shopData.json"))
        {
            Load();
        }
        else
        {

        }
    }

    public void Save()
    {
        Debug.Log("�����ϱ�");

   
        //for�� �� �� ������ ���� �ٵ�.. shop��ũ��Ʈ�� ���ľ� �ұ�?
        scriptIngame.money = ig.money; 
        scriptIngame.increaseAmt = ig.moneyIncreaseAmount; 
        scriptIngame.autoIncreaseAmt = ig.autoIncreaseAmount;

        scriptSuryong.ind = sr.ind;
        scriptSuryong.isMonster = sr.haveADrink;

        scriptShop.crystalPrice = sh.pCrystal;
        scriptShop.randomPrice = sh.pRandom;
        scriptShop.clickPrice = sh.pClick;
        scriptShop.feverPrice = sh.pFever;
        scriptShop.catPrice = sh.pCats;
        scriptShop.numOfCats = sh.numOfCats;


        JsonData ingameJson = JsonMapper.ToJson(scriptIngame);
        JsonData suryongJson = JsonMapper.ToJson(scriptSuryong);
        JsonData shopJson = JsonMapper.ToJson(scriptShop);

        File.WriteAllText(Application.dataPath + "/Resources/ingameData.json", ingameJson.ToString());
        File.WriteAllText(Application.dataPath + "/Resources/suryongData.json", suryongJson.ToString());
        File.WriteAllText(Application.dataPath + "/Resources/shopData.json", shopJson.ToString());
    }

    //�ҷ����� �Լ� �ۼ�~. ����Ǿ� �ִ� ������ �ҷ��� ���� ������ �ֱ�.
    public void Load()
    {
        Debug.Log("�ҷ�����");

        string JsonStrIngame = File.ReadAllText(Application.dataPath + "/Resources/ingameData.json");
        string JsonStrSuryong = File.ReadAllText(Application.dataPath + "/Resources/suryongData.json");
        string JsonStrShop = File.ReadAllText(Application.dataPath + "/Resources/shopData.json");

        Debug.Log(JsonStrIngame);
        Debug.Log(JsonStrSuryong);
        Debug.Log(JsonStrShop);

        //JsonStr�� jsondata�� �ٲ�
        JsonData ingameData = JsonMapper.ToObject(JsonStrIngame);
        JsonData suryongData = JsonMapper.ToObject(JsonStrSuryong);
        JsonData shopData = JsonMapper.ToObject(JsonStrShop);

        //�� ������ ����� �� ����.
        ig.money = long.Parse( ingameData["money"].ToString());
        ig.moneyIncreaseAmount = long.Parse(ingameData["increaseAmt"].ToString());
        ig.autoIncreaseAmount = long.Parse(ingameData["autoIncreaseAmt"].ToString());
        
        sr.ind = int.Parse(suryongData["ind"].ToString());
        sr.haveADrink = bool.Parse(suryongData["isMonster"].ToString());
        
        sh.pCrystal = long.Parse(shopData["crystalPrice"].ToString());
        sh.pRandom = long.Parse(shopData["randomPrice"].ToString());
        sh.pClick = long.Parse(shopData["clickPrice"].ToString());
        sh.pFever = long.Parse(shopData["feverPrice"].ToString());
        sh.pCats = long.Parse(shopData["catPrice"].ToString());
        sh.numOfCats = int.Parse(shopData["numOfCats"].ToString());

    }

    private void OnApplicationQuit()
    {
        Save();
    }

}
