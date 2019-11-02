using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteListData", menuName = "ScriptableObjects/SpriteListData", order = 2)]
public class IconSpriteListData : ScriptableObject
{
    public Sprite[] spriteList;

    // Start is called before the first frame update
    public Sprite GetRandomSprite()
    {
        int spriteID = Random.Range(0, spriteList.Length);
        return spriteList[spriteID];
    }
}
