using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class DragItem : UI_Base
{
    [SerializeField] GameObject TempItemObject;

    GameObject SilhouetteItem;

    Map map;
    BuildManager buildManager;
    public ItemData data;
    Data.Item itemData;

    private RaycastHit hit;
    private Vector3 normal_position;
    private Vector3 normal_size;

    Image icon;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        //Debug.Log(gameObject.name);
        itemData = Managers.Data.ItemDict[gameObject.name];
        icon = GetComponentsInChildren<Image>()[6];
        icon.sprite = Managers.Resource.Load<Sprite>($"Icon/{itemData.itemIcon}");

        BindEvent(gameObject, (PointerEventData data) => { OnBeginDrag(data); }, Define.UIEvent.BeginDrag);
        BindEvent(gameObject, (PointerEventData data) => { OnDrag(data); }, Define.UIEvent.Drag);
        BindEvent(gameObject, (PointerEventData data) => { OnEndDrag(data); }, Define.UIEvent.EndDrag);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        normal_position = transform.position;
        normal_size = transform.localScale;
        Managers.Select.SelectItemToUse(gameObject, itemData);
        //buildManager.SelectItemToUse(gameObject, data);
        SilhouetteItem = Managers.Resource.Instantiate("ItemE_Tower");
        //SilhouetteItem = Instantiate(TempItemObject, hit.point, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float size = Mathf.Sqrt((normal_position.x - eventData.position.x) * (normal_position.x - eventData.position.x) + (normal_position.y - eventData.position.y) * (normal_position.y - eventData.position.y));

        if (normal_size.x - size / 100 > 0)
        {
            transform.position = eventData.position;

            transform.localScale = new Vector3(normal_size.x - size / 120, normal_size.y - size / 120, 0);
            SilhouetteItem.transform.localScale = new Vector3(1 - (normal_size.x - size / 100), 1 - (normal_size.x - size / 100), 1 - (normal_size.x - size / 100));
        }
        else
        {
            transform.localScale = new Vector2(0, 0);
            SilhouetteItem.transform.localScale = new Vector3(1.01f, 1.01f, 1.01f);
        }

        //Move
        Physics.Raycast(ray, out hit);
        if (hit.transform.name == "ForestGround01(Clone)")
        {
            //Debug.Log("OnNode");

            if (data.itemId == 0)
            {
                if ((hit.transform.GetComponent<Node>().turret != null || !hit.transform.GetComponent<Node>().turret) && !hit.transform.GetComponent<Node>().enviroment)
                {
                    foreach (Component com in SilhouetteItem.GetComponentsInChildren<Component>())
                        foreach (Renderer ren in com.GetComponentsInChildren<Renderer>())
                            ren.material.color = new Color(0.1375f, 1, 0, 0);
                    //foreach (Renderer mat in SilhouetteItem.GetComponentsInChildren<Renderer>())
                    //    foreach(Renderer mat2 in mat.GetComponentsInChildren<Renderer>())
                    //        mat2.material.color = new Color(0.1375f, 1, 0, 0);
                }
                else
                {
                    foreach (Renderer mat in SilhouetteItem.GetComponentsInChildren<Renderer>())
                        mat.material.color = new Color(1, 0.01f, 0, 0);
                }
            }


            SilhouetteItem.transform.position = hit.transform.position + new Vector3(0, hit.transform.localScale.y, 0);
        }
        else if (hit.transform.name == "Plane")
        {
            if (data.itemId == 0)
            {
                foreach (Renderer mat in SilhouetteItem.GetComponentsInChildren<Renderer>())
                    mat.material.SetColor("_Color", new Color(1, 1, 1));
            }
            SilhouetteItem.transform.position = hit.point;
        }
        else
        {
            if (data.itemId == 0)
            {
                foreach (Renderer mat in SilhouetteItem.GetComponentsInChildren<Renderer>())
                    mat.material.color = new Color(1, 0.01f, 0, 0);
            }
            SilhouetteItem.transform.position = hit.point;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = normal_position;
        transform.localScale = normal_size;
        if (hit.transform.name == "ForestGround01(Clone)")
        {
            if ((hit.transform.GetComponent<Node>().turret != null || !hit.transform.GetComponent<Node>().turret))
            {
                hit.transform.GetComponent<Node>().UseItem();
            }
        }
        Managers.Select.Clear();
        //buildManager.Clear();
        Destroy(SilhouetteItem);
        SilhouetteItem = null;
    }

    public void Onclick()
    {
        //Debug.Log($"{data.itemName} Selected");

        if (data.itemType == ItemData.ItemType.WorldOnlyItem)
        {
            Managers.Sound.Play("Effects/userLife", Define.Sound.Effect);
            //GameManager.instance.soundManager.Play("Effects/userLife", SoundManager.Sound.Effect);
            Managers.Select.SelectItemToUse(gameObject, itemData);
            //buildManager.SelectItemToUse(gameObject, data);
            GameManager.instance.Lives++;
            Managers.Select.DestroyItemUI();
            //buildManager.DestroyItemUI();
        }
    }

    void Start()
    {
        map = GameObject.Find("GameMaster").GetComponent<Map>();
    }
}