using UnityEngine;

public class toggleThisObject : MonoBehaviour
{
    bool toggle;
    public void toggleActive()
    {
        toggle = !toggle;
        this.gameObject.SetActive(toggle);
    }
}
