using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

/************************************************************
************************************************************/

public class TextFader : MonoBehaviour {
	/****************************************
	****************************************/
	enum STATE{
		WAIT_TRIGGER,
		OUTPUT_1BY1,
		END,
	};
	
	STATE State = STATE.WAIT_TRIGGER;
	
	string str_WholeText;
	StringBuilder sb_output = new StringBuilder("");
	Text Text_output;
	int id = 0;
	float t_LastUpdate;
	
	[SerializeField]	float t_Refresh = 0.03f;
	[SerializeField]	int RefreshPerTime = 2;
	
	/****************************************
	****************************************/
	
	/******************************
	******************************/
	void Start () {
		Text_output = gameObject.GetComponent<Text>();
		str_WholeText = string.Copy(Text_output.text);
		
		init();
	}

	
	/******************************
	******************************/
	void init () {
		Text_output.text = "";
		sb_output.Length = 0;
		id = 0;
		t_LastUpdate = Time.realtimeSinceStartup;
		
		State = STATE.WAIT_TRIGGER;
	}

	/******************************
	******************************/
	void Update () {
		/********************
		********************/
		/*
		if(Input.GetKeyDown("t")){
			StartOutput();
		}else if(Input.GetKeyDown("c")){
			complete();
		}else if(Input.GetKeyDown("r")){
			Reset();
		}
		*/
		
		/********************
		********************/
		StateChart();
	}
	
	/******************************
	******************************/
	public void StateChart() {
		switch(State){
			case STATE.WAIT_TRIGGER:
				break;
				
			case STATE.OUTPUT_1BY1:
				if(t_Refresh < Time.realtimeSinceStartup - t_LastUpdate){
					t_LastUpdate = Time.realtimeSinceStartup;
					
					for(int i = 0; i < RefreshPerTime; i++){
						sb_output.Append(str_WholeText[id]);
						id++;
						if(str_WholeText.Length <= id){
							State = STATE.END;
							break;
						}
					}
				}
				break;
				
			case STATE.END:
				break;
		}
		
		Text_output.text = sb_output.ToString();
	}
	
	/******************************
	******************************/
	public void StartOutput() {
		switch(State){
			case STATE.WAIT_TRIGGER:
				State = STATE.OUTPUT_1BY1;
				break;
				
			case STATE.OUTPUT_1BY1:
				break;
				
			case STATE.END:
				break;
		}
	}
	
	/******************************
	******************************/
	public void complete() {
		if(State != STATE.END){
			State = STATE.END;
			sb_output.Length = 0;
			sb_output.Append(str_WholeText);
		}
	}
	
	/******************************
	******************************/
	public void Reset() {
		if(State != STATE.WAIT_TRIGGER){
			init();
		}
	}
}
