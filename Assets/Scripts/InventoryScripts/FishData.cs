using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishing/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public float weight;
    public string rarity;
    public int price;
    public Sprite fishSprite; 
}