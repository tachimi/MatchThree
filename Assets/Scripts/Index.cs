using UnityEngine;

public class Index : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }

    public void ShowIndex()
    {
        Debug.Log(X + " " + Y);
    }
}