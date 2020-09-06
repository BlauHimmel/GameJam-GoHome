using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story Node")]
public class Story : ScriptableObject
{
    // There must be BranchNode after a series of StoryNodes.
    public List<Node> Nodes;
    public bool IsMainQuest;

    public enum Attribute
    {
        None, Happy, Hungry, Money
    }

    public enum NodeType
    {
        None, Story, Branch, Action, Game
    }

    [Serializable]
    public struct Node
    {
        public GameNode GameNode;
        public StoryNode StoryNode;
        public BranchNode BranchNode;
        public ActionNode ActionNode;
    }

    [Serializable]
    public struct GameNode
    {
        public bool Enable;

        [Multiline(8)]
        public string Text;
        public string AudioPath;
        public List<string> RequestItems;
        public PropertyNode FailurePropertyCast;
        public PropertyNode SuccessPropertyCast;
        public string FailureText;

        public Sprite BackgroundImage;
        public string BGMPath;
    }

    [Serializable]
    public struct StoryNode
    {
        public bool Enable;

        public string Text;
        public string CharacterName;
        public Sprite Icon;
        public string AudioPath;

        public Sprite BackgroundImage;
        public string BGMPath;
    }

    [Serializable]
    public struct PropertyNode
    {
        public bool Enable;

        public Attribute CastAttribute;
        public int AttributeNumber;
    }

    [Serializable]
    public struct BranchNode
    {
        public bool Enable;

        public string LeftBranchText;
        public string RightBranchText;
        public PropertyNode LeftBranchPropertyCast;
        public PropertyNode RightBranchPropertyCast;

        public Sprite BackgroundImage;
        public string BGMPath;
    }

    [Serializable]
    public struct ActionNode
    {
        public bool Enable;

        public string LeftActionText;
        public string RightActionText;

        public float LeftActionTime;
        public float RightActionTime;

        public PropertyNode LeftActionPropertyCast;
        public PropertyNode RightActionPropertyCast;

        public Sprite BackgroundImage;
        public string BGMPath;
    }
}
