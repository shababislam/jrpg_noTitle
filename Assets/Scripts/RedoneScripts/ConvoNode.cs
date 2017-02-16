using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ConvoNode {
	public string[] lines;
	public bool hasNext_a = false;
	public bool hasNext_b = false;
	public int linkIndex_a;
	public int linkIndex_b;
	public ConvoNode next_a;
	public ConvoNode next_b;
	//public string[] nextA;
	//public string[] nextB;
	public bool triggersQuest = false;
	public bool returnToStart = false;

	public ConvoNode(string[] dialog){
		lines = dialog;
	}

	public string[] getLines(){
		return lines;
	}

	public void nextNode_a(string[] dialog){
		hasNext_a = true;
		next_a = new ConvoNode(dialog);
	}

	public void nextNode_b(string[] dialog){
		hasNext_b = true;
		next_b = new ConvoNode(dialog);
	}

	public void nextNode_a(ConvoNode a){
		hasNext_a = true;
		next_a = a;
	}

	public void nextNode_b(ConvoNode b){
		hasNext_b = true;
		next_b = b;
	}

	public bool hasNext(){
		return hasNext_a || hasNext_b;
	}

	public ConvoNode getA(){
		return next_a;
	}

	public ConvoNode getB(){
		return next_b;
	}

	public bool hasA(){
		return hasNext_a;
	}

	public bool hasB(){
		return hasNext_b;
	}
		
}
