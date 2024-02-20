using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagControllar : MonoBehaviour
{

    [SerializeField] private Transform bagTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("other"))
        {
            AddProductToBag(other.gameObject);
        }
    }

    public void AddProductToBag(GameObject Cube)
    {
        Cube.transform.SetParent(bagTransform, true);

        Cube.transform.localPosition = Vector3.zero;
        Cube.transform.localRotation = Quaternion.identity;
    }
}
