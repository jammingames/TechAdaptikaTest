using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishScriptableObject")]
public class FishData : ScriptableObject
{
	public string name = "fish";
	public float speed = 1f;
	public float lifetime = 1f;
	public int points = 1;
    public Color myColor;
}
