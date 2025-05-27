using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewNewsCycle", menuName = "News/NewsCycle")]
public class NewsCycle : ScriptableObject
{
    public string cycleName;
    public List<Article> articles;
}
