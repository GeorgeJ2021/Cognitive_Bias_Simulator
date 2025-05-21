using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArticle", menuName = "News/Article")]
public class Article : ScriptableObject
{
    public string headline;
    [TextArea] public string body;
    public Sprite image;

}
