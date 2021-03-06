using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instantiator : MonoBehaviour {

    public int recurse = 10;
	public int treeAmmount;
	public float angle;
	public float angleMinimum;
	public float angleMaximum;
	public float scale = 0.5f;
	public float branchX;
	public float branchY;

	public Button grow;
	public Toggle addleaf;

	public Text angleMinValue;
	public Text angleMaxValue;
	public Text branchXvalue;
	public Text branchYvalue;
	public Slider angleMin;
	public Slider angleMax;
	public Slider branchXslider;
	public Slider branchYslider;

	public Renderer[] leaf;

	public Material tree1;
	public Material tree2;
	public Material tree3;

	public Material leaf1;
	public Material leaf2;
	public Material leaf3;
	public Material leaf4;

	public bool addLeaves = true;
	public bool active;

	public Vector3 center;
	public Vector3 size;

	public GameObject treeStart;

	void Start () 
	{

		Button btn = grow.GetComponent<Button> ();
		Toggle useLeaf = addleaf.GetComponent<Toggle> ();
		btn.onClick.AddListener (growTree);
		useLeaf.onValueChanged.AddListener (delegate {
			useLeaves();	
		});

		//spawnArea ();

	}
		
	void useLeaves()
	{
		addLeaves = true;
	}
		
	void growTree()
	{
		recurse -= 1;

			if (recurse > 0)
			{ 
				scale = Random.Range (0.5f, 0.6f);
				angle = Random.Range (angleMinimum, angleMaximum);
				var copy = Instantiate(gameObject);
				var recursive = copy.GetComponent<Instantiator>();
				recursive.SendMessage("Generated", new RecursiveBundle() { Index = recurse, Parent = this });


				
			}
		
	}
	void Update()
	{
		angleMinValue.text = angleMin.value.ToString ();
		angleMaxValue.text = angleMax.value.ToString ();

		angleMinimum = angleMin.value;
		angleMaximum = angleMax.value;

		branchXvalue.text = branchXslider.value.ToString ();
		branchYvalue.text = branchYslider.value.ToString ();

		branchX = branchXslider.value;
		branchY = branchYslider.value;


		if (!active)
		this.gameObject.GetComponent<Transform> ().localScale = new Vector3(branchX, branchY, branchX);


	}

	public void changeTextureTree1()
	{
		this.gameObject.GetComponent<Renderer> ().material = tree1;
	}

	public void changeTextureTree2()
	{
		this.gameObject.GetComponent<Renderer> ().material = tree2;
	}

	public void changeTextureTree3()
	{
		this.gameObject.GetComponent<Renderer> ().material = tree3;
	}

	public void changeTextureleaf1()
	{
		for (int i = 0; i < leaf.Length; i++) 
		{
			leaf [i].gameObject.GetComponent<Renderer> ().material = leaf1;
		}

	}
	public void changeTextureleaf2()
	{
		for (int i = 0; i < leaf.Length; i++) 
		{
			leaf [i].gameObject.GetComponent<Renderer> ().material = leaf2;
		}

	}
	public void changeTextureleaf3()
	{
		for (int i = 0; i < leaf.Length; i++) 
		{
			leaf [i].gameObject.GetComponent<Renderer> ().material = leaf3;
		}

	}
	public void changeTextureleaf4()
	{
		for (int i = 0; i < leaf.Length; i++) 
		{
			leaf [i].gameObject.GetComponent<Renderer> ().material = leaf4;
		}

	}

	public void spawnArea()
	{
			Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), 0.3f, Random.Range (-size.z / 2, size.z / 2));
			Instantiate (treeStart, pos, Quaternion.identity);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawCube (center, size);
	}

	public void Generated(RecursiveBundle bundle)
	{
		
		active = true;
		this.transform.position += this.transform.up * this.transform.localScale.y;
		this.transform.rotation *= Quaternion.Euler(angle * ((bundle.Index * 2) +2), angle * (bundle.Index*2)-1, angle * (bundle.Index*2)-1);
		this.transform.localScale *= scale;

		if (this.gameObject.name.Contains ("Clone") && addLeaves == true) 
		{
			for (int i = 0; i < leaf.Length; i++) 
			{
				leaf [i].enabled = true;
			}

			Debug.Log ("found");
		}


	}

}
