 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Transform itemWorld;

    public Sprite morralSprite;
    public Sprite keySprite;
    public Sprite ganzuaSprite;
    public Sprite backpackBoneSprite;
    public Sprite BoneSprite;
    public Sprite backpackBookSprite;

    public string tagMorral = "Morral";
    public string tagGanzua = "Ganzua";
    public string tagKey = "Key";
    public string tagBone = "Bones";
    public string tagBoneOne = "BoneOne";
    public string tagBook = "Book";
}
