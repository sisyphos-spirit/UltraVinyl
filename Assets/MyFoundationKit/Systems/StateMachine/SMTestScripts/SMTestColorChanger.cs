using UnityEngine;

public class SMTestColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    public void ChangeColor(Color color)
    {
        foreach (var go in gameObjects)
        {
            var renderer = go.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = color;
            }
        }
    }
}
