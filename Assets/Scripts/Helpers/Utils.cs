using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Create Text in the world
    public static TextMesh CreateWorldText(string text, Vector3 position, int fontSize = 40, TextAnchor textAnchor = TextAnchor.MiddleCenter)
    {
        GameObject obj = new GameObject("World Text", typeof(TextMesh));
        TextMesh textMesh = obj.GetComponent<TextMesh>();
        obj.transform.localPosition = position;
        textMesh.anchor = textAnchor; 
        textMesh.text = text;
        textMesh.fontSize = fontSize; 
        return textMesh;
    }
}