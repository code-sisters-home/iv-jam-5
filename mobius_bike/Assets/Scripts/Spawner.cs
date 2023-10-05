using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CatmullRomSpline path;
    [SerializeField] private GameObject[] items;
	public float dropPeriod = 2;
    public string tag = "drop";	
	public float roadWidth = 5;
	
    private void Start()
	{
		//foreach (var item in items)
		//{
		//	item.tag = tag;
		//	SphereCollider sc = item.AddComponent<SphereCollider>();
		//	sc.isTrigger = true;
		//}
		
	}

    public void DropSomething()
    {
        //max is exclusive
		PositionAndRotation pr = new PositionAndRotation();
		float s = Random.Range(0.0f, path.controlPointsList.Length);
        pr = path.GetPositionAndRotation(s);
		
		int i = Random.Range(0, items.Length);
        var item = Instantiate(items[i], pr.position, pr.rotation, transform);
		float shift = Random.Range(-1.0f, 1.0f)*roadWidth;
		Vector3 lr = new Vector3(shift, 0, 0);
		item.transform.position += item.transform.TransformDirection(lr);

		float scale = 2;
		item.transform.localScale = new Vector3(scale, scale, scale);
		
		item.tag = tag;
		SphereCollider sc = item.AddComponent<SphereCollider>();
		sc.isTrigger = true;
	}

    float timer = 0.0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dropPeriod)
        {
            DropSomething();
            timer = 0;
        }
    }
}
