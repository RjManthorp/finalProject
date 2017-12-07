using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instantiator : MonoBehaviour {

    public int recurse = 5;
    public int splitNum = 2;
	public float angle = 30;
	public float scale = 0.5f;

	public Button grow;

	public Text splitValue;
	public Text angleValue;
	public Slider mainSlider;
	public Slider secondSlider;

	public Vector3 pivotPosition;

	public void SubmitSliderSetting()
	{
	}

	void Start () 
	{
		Button btn = grow.GetComponent<Button> ();
		btn.onClick.AddListener (activate);

	}


	void activate()
	{

		growTree ();
	}
		
	void growTree()
	{
		recurse -= 1;

			if (recurse > 0)
			{
				var copy = Instantiate(gameObject);
				var recursive = copy.GetComponent<Instantiator>();
			recursive.SendMessage("Generated", new RecursiveBundle() { Index = recurse, Parent = this });

			}
		
	}
	void Update()
	{
		splitNum = (int) mainSlider.value;
		angle = (int)secondSlider.value;
		splitValue.text = splitNum.ToString ();
		angleValue.text = angle.ToString ();


	}

	public void Generated(RecursiveBundle bundle)
	{
		
		this.transform.position += this.transform.up * this.transform.localScale.y;
		this.transform.rotation *= Quaternion.Euler(angle * ((bundle.Index * 2) - 1), angle * (bundle.Index*2)-1, angle * (bundle.Index*2)-1);
		//this.transform.rotation *= Quaternion.Euler(angle * ((bundle.Index * 2) - 1), 0, 0);
		this.transform.localScale *= scale;


	}

}
