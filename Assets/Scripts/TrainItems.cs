using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Train Items")]
public class TrainItems : ScriptableObject
{
    public List<Item> ItemsAtTrain0;
    public List<Item> ItemsAtTrain1;
    public List<Item> ItemsAtTrain2;
    public List<Item> ItemsAtTrain3;

    [Serializable]
    public struct Item
    {
        public string ItemName;
    }
}
