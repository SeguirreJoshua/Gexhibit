using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float rotateSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,0,rotateSpeed,Space.World);
    }
}
