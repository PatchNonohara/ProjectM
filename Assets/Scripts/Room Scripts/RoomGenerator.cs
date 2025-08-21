using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject RoomTemplate;
    public GameObject RoomTemplateRight;
    public GameObject RoomTemplateForward;
    public GameObject RoomTemplateLeftAndRight;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 GeneratorPos = transform.position;
        Instantiate(RoomTemplate,GeneratorPos,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
