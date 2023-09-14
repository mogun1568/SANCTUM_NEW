using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    [HideInInspector] public GameObject turret;

    int upgradedNum; // 원소 적용 3번까지 가능
    string element;    // 적용된 원소
    int countItem = 0;

    BuildManager buildManager;

    public bool enviroment;

    public Vector3 GetBuildPosition()
    {
        return transform.position + new Vector3(0f, transform.localScale.y, 0f);
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Managers.Select.SelectNode(this);
            //buildManager.SelectNode(this);
        }
    }

    public void UseItem()
    {
        if (turret && !turret.activeSelf)
        {
            turret = null;
            countItem = 0;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (enviroment)
        {
            return;
        }

        Data.Item itemData = Managers.Select.getItemData();

        if (turret == null)
        {
            switch (itemData.itemType)
            {
                case "Tower":
                    BuildTurret();
                    break;
                default:
                    Debug.Log("You don't use item!");
                    break;
            }
        }
        else
        {
            switch (itemData.itemType)
            {
                case "Tower":
                    Debug.Log("There's already a tower here!");
                    break;
                case "Element":
                    ApplicateElement(itemData);
                    break;
                case "TowerOnlyItem":
                    UseTowerOnlyItem(itemData);
                    break;
            }
        }

        countItem += itemData.returnExp;
    }

    void BuildTurret()
    {
        Managers.Sound.Play("Effects/Build", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Build", SoundManager.Sound.Effect);
        GameObject _turret = Managers.Resource.Instantiate("Tower/Prefab/BallistaTowerlvl02");
        _turret.transform.SetPositionAndRotation(GetBuildPosition(), Quaternion.identity);
        //GameObject _turret = GameManager.instance.pool.GetTower(0, 0, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        //turret.GetComponent<Turret>().data = data;

        BulldEffect();
        //GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        element = "null";
        upgradedNum = 0;

        Debug.Log("Turret build!");
        //buildManager.itemUITextDecrease();
        Managers.Select.itemUITextDecrease();
    }

    void ApplicateElement(Data.Item itemData)
    {
        if (upgradedNum > 0 && element != itemData.itemName)
        {
            Debug.Log("already ues element!");
            return;
        }
        element = itemData.itemName;

        if (upgradedNum >= 3)
        {
            Debug.Log("Upgrade Done!");
            return;
        }

        Managers.Sound.Play("Effects/Build", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Build", SoundManager.Sound.Effect);
        Debug.Log($"{itemData.itemName} Upgrade {upgradedNum} -> {upgradedNum + 1}");
        upgradedNum++;

        // 원소 타워 생성
        GameObject _turret = Managers.Resource.Instantiate($"Tower/Prefab/{itemData.itemName}Tower/{itemData.itemName}Towerlvl0{upgradedNum.ToString()}");
        _turret.transform.SetPositionAndRotation(GetBuildPosition(), Quaternion.identity);
        //GameObject _turret = GameManager.instance.pool.GetTower(data.itemId, upgradedNum - 1, GetBuildPosition(), Quaternion.identity);

        // 타워 정보 이동
        /*Turret turretComponent = turret.GetComponent<Turret>();
        Turret _turretComponent = _turret.GetComponent<Turret>();

        _turretComponent.range = turretComponent.range;
        _turretComponent.fireRate = turretComponent.fireRate;
        _turretComponent.bulletSpeed = turretComponent.bulletSpeed;
        _turretComponent.bulletDamage = turretComponent.bulletDamage;
        _turretComponent.health = turretComponent.health;

        _turretComponent.health += 50;*/

        TowerStat curTowerStat = turret.GetComponent<TowerControl>()._stat;
        TowerStat newTowerStat = _turret.GetComponent<TowerControl>()._stat;

        newTowerStat.TowerType = element;
        newTowerStat.HP = curTowerStat.HP;
        newTowerStat.Range = curTowerStat.Range;
        newTowerStat.FireRate = curTowerStat.FireRate;
        newTowerStat.BulletDamage = curTowerStat.BulletDamage;
        newTowerStat.BulletSpeed = curTowerStat.BulletSpeed;

        // 타워 변경
        Managers.Resource.Destroy(turret);
        //turret.SetActive(false);
        turret = _turret;

        //turret.GetComponent<Turret>().itemData = itemData;
        turret.GetComponent<TowerControl>().itemData = itemData;

        if (element == "Water")
        {
            Transform healEffect = turret.transform.GetChild(turret.transform.childCount - 1);
            healEffect.localScale = new Vector3(newTowerStat.Range * 2, healEffect.localScale.y, newTowerStat.Range * 2);
        }

        BulldEffect();
        //GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        //Debug.Log("Applicate Element!");
        Managers.Select.itemUITextDecrease();
        //buildManager.itemUITextDecrease();
    }

    void UseTowerOnlyItem(Data.Item itemData)
    {
        Managers.Sound.Play("Effects/Soundiron_Shimmer_Charms_Short_07 [2023-06-13 121009]", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Soundiron_Shimmer_Charms_Short_07 [2023-06-13 121009]", SoundManager.Sound.Effect);
        //Turret turretScript = turret.GetComponent<Turret>();
        TowerStat towerStat = turret.GetComponent<TowerControl>()._stat;

        switch (itemData.itemName)
        {
            case "DamageUp":
                Debug.Log($"Damage Up {towerStat.BulletDamage} -> {towerStat.BulletDamage * itemData.upgradeAmount}");
                towerStat.BulletDamage *= itemData.upgradeAmount;
                break;
            case "RangeUp":
                Debug.Log($"Range Up {towerStat.Range} -> {towerStat.Range * itemData.upgradeAmount}");
                towerStat.Range *= itemData.upgradeAmount;

                if (element == "Water")
                {
                    Transform healEffect = turret.transform.GetChild(turret.transform.childCount - 1);
                    healEffect.localScale = new Vector3(towerStat.Range * 2, healEffect.localScale.y, towerStat.Range * 2);
                }
                break;
            case "FireRateUp":
                Debug.Log($"Range Up {towerStat.FireRate} -> {towerStat.FireRate * itemData.upgradeAmount}");
                towerStat.FireRate *= itemData.upgradeAmount;
                break;
        }
        Managers.Select.itemUITextDecrease();
        //buildManager.itemUITextDecrease();
    }

    void UseWolrdOnlyItem(ItemData data)
    {

    }

    public void FirstPersonMode()
    {
        Debug.Log("First Person Mode");
        //Turret turretScript = turret.GetComponent<Turret>();
        TowerControl towerControl = turret.GetComponent<TowerControl>();
        towerControl.isFPM = true;
        // 비활성화된 오브젝트는 그냥 GetComponent로 못찾음 GetComponents<>(true)로 배열로 찾아서 사용해야 함
        turret.GetComponentsInChildren<Camera>(true)[0].gameObject.SetActive(true);
    }

    public void DemoliteTower()
    {
        // 현재 exp += 이 node의 returnExp; // 이 코드 추가 예정
        int remainExp = GameManager.instance.nextExp;

        for (int i = 1; i < countItem / 2; i++)
        {
            remainExp += Mathf.RoundToInt(remainExp * 1.5f);
            Debug.Log(remainExp);

            // 0/3 -> 0/5 -> 0/8
        }
        if (countItem % 2 != 0)
        {
            remainExp += remainExp / 2;
            Debug.Log(remainExp);

            // 4/8
        }
        GameManager.instance.GetExp(remainExp);
        countItem = 0;

        Managers.Sound.Play("Effects/Explosion", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/Explosion", SoundManager.Sound.Effect);
        BulldEffect();
        //GameObject effect = (GameObject)Instantiate(buildManager.destroyEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        // 제거를 할 지 철거된 오브젝트로 변경할 지 고민 중
        Managers.Resource.Destroy(turret);
        //turret.SetActive(false);
        turret = null;
        //Destroy(turret);
        //turretBlueprint = null;

        Debug.Log("Demolite Tower");
    }

    void BulldEffect()
    {
        GameObject effect = Managers.Resource.Instantiate("Tower/Prefab/Launch Smoke");
        effect.transform.SetPositionAndRotation(GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    void OnMouseEnter()
    {

    }

    void OnMouseExit()
    {

    }
}