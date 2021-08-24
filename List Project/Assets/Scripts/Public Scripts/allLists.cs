using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class searchItem
{
    public item item;
    public int percent;


    public searchItem(item item, int percent)
    {
        this.item = item;
        this.percent = percent;
    }

    public searchItem(searchItem searchItem)
    {
        this.item = searchItem.item;
        this.percent = searchItem.percent;
    }

}

public class ComparerByItemPercent: IComparer<searchItem>
{
    public int Compare(searchItem x, searchItem y)
    {
        searchItem i1 = x;
        searchItem i2 = y;
        int result = i2.percent.CompareTo(i1.percent);
        if(result == 0)
        {
            result = i2.item.value.CompareTo(i1.item.value);

        }
        return result;
    }
}
[System.Serializable]
public class searchTag
{
    public tag tag;
    public int percent;


    public searchTag(tag tag, int percent)
    {
        this.tag = tag;
        this.percent = percent;
    }

    public searchTag(searchTag searchItem)
    {
        this.tag = searchItem.tag;
        this.percent = searchItem.percent;
    }

}

public class ComparerByTagPercent: IComparer<searchTag>
{
    public int Compare(searchTag x, searchTag y)
    {
        searchTag i1 = x;
        searchTag i2 = y;
        int result = i2.percent.CompareTo(i1.percent);
        if(result == 0)
        {
            result = i2.tag.tagEnum.Count.CompareTo(i1.tag.tagEnum.Count);
            if(result == 0)
            {
                result = i2.tag.tagIndexNo.CompareTo(i1.tag.tagIndexNo);
            }

        }
        return result;
    }
}
[System.Serializable]
public class item
{
    public int indexNo;
    public string name;
    public string desc;
    public List<tag> tags = new List<tag>();
    public int value;
    public Sprite ItemSprite;
    public System.DateTime date;


    public item(int indexNo, string name, string desc, List<tag> tags, int value, System.DateTime date)
    {
        this.indexNo = indexNo;
        this.name = name;
        this.desc = desc;
        this.tags = tags;
        this.value= value;
        ItemSprite = Resources.Load<Sprite>("ItemSprites/item" + indexNo);
        this.date = date;


    }

    public item(item Item)
    {
        this.indexNo    =  Item.indexNo;
        this.name  =  Item.name;
        this.desc  =  Item.desc;
        this.tags  =  Item.tags;
        this.value =  Item.value;
        this.date = Item.date;
    }



}

public class ComparerByName: IComparer<item>
{
    public int Compare(item x, item y)
    {
        item i1 = x;
        item i2 = y;
        int result = i1.name.CompareTo(i2.name);
        if(result == 0)
        {
            result = i1.value.CompareTo(i2.value);
            if(result == 0)
            {
                i1.indexNo.CompareTo(i2.indexNo);
            }
        }
        return result;
    }
}

public class ComparerByDate: IComparer<item>
{
    public int Compare(item x, item y)
    {
        item i1 = x;
        item i2 = y;
        int result = i1.date.CompareTo(i2.date);
        if(result == 0)
        {
            result = i1.value.CompareTo(i2.value);
            if(result == 0)
            {
                i1.indexNo.CompareTo(i2.indexNo);
            }
        }
        return result;
    }
}
public class ComparerByValue: IComparer<item>
{
    public int Compare(item x, item y)
    {
        item i1 = x;
        item i2 = y;
        int result = i1.value.CompareTo(i2.value);
        if(result == 0)
        {
            result = i1.name.CompareTo(i2.name);
            if(result == 0)
            {
                i1.indexNo.CompareTo(i2.indexNo);
            }
        }
        return result;
    }
}

public class ComparerByNameTag: IComparer<tag>
{
    public int Compare(tag x, tag y)
    {
        tag i1 = x;
        tag i2 = y;
        int result = i1.tagname.CompareTo(i2.tagname);
        if(result == 0)
        {
            result = i1.tagdesc.CompareTo(i2.tagdesc);

        }
        return result;
    }
}
public class ComparerByNameTagEnum: IComparer<tagEnum>
{
    public int Compare(tagEnum x, tagEnum y)
    {
        tagEnum i1 = x;
        tagEnum i2 = y;
        int result = i1.enumName.CompareTo(i2.enumName);
        if(result == 0)
        {
            result = i1.enumIndexNo.CompareTo(i2.enumIndexNo);

        }
        return result;
    }
}

[System.Serializable]
public class tagEnum
{
    public string enumName;
    public int enumIndexNo;

    public tagEnum(int enumIndexNo, string enumName)
    {
        this.enumIndexNo = enumIndexNo;
        this.enumName = enumName;

    }

    public tagEnum(tagEnum Tag)
    {
        this.enumIndexNo = Tag.enumIndexNo;
        this.enumName = Tag.enumName;

    }
}

[System.Serializable]
public class tag
{
    public string tagname;
    public int tagIndexNo;
    public string tagdesc;
    public List<tagEnum> tagEnum = new List<tagEnum>();


    public tag(int tagIndexNo, string tagname, string tagdesc, List<tagEnum> tagEnum)
    {
        this.tagIndexNo = tagIndexNo;
        this.tagname = tagname;
        this.tagdesc = tagdesc;
        this.tagEnum = tagEnum;
    }

    public tag(tag Tag)
    {
        this.tagIndexNo = Tag.tagIndexNo;
        this.tagname = Tag.tagname;
        this.tagdesc = Tag.tagdesc;
        this.tagEnum = Tag.tagEnum;
    }
}


public class allLists : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
