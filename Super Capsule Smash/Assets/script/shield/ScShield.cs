using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShield : MonoBehaviour
{
    [SerializeField] GameObject shieldPart;
    [SerializeField] int rectCount;
    [SerializeField] int maxAngle;
    [SerializeField] int decreaseRatio;
    [SerializeField] float thickness;   
    [SerializeField] float radius;

    private List<Transform> shieldParts = new List<Transform>();
    private Vector3 partfullSize;
    private Vector2 direction;

    private void Start()
    {
        partfullSize = new Vector3( Mathf.Cos((180-(90+maxAngle/rectCount))*Mathf.Deg2Rad)*radius,thickness,1);
        direction = Vector2.right;
        for (int i = 0; i < rectCount; i++) 
        {
            var tempo = Instantiate(shieldPart,transform);
            shieldParts.Add(tempo.transform);
            shieldParts[i].localScale = partfullSize;
        }//create the parts of the shield
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("sexe");
            shieldParts[0].localScale = new Vector3(thickness,thickness,thickness);
        }
    }

    private void UpdateShieldRelativPosToHolder()
    {

    }
}
