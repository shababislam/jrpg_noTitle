using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ConvoContainer{
	
	public ConvoNode[] convos;

	public void updateList(){
		foreach (ConvoNode convo in convos){
			if(convo.linkIndex_a!=0){
				convo.nextNode_a(convos[convo.linkIndex_a]);

			}
			if(convo.linkIndex_b!=0)
				convo.nextNode_b(convos[convo.linkIndex_b]);
		}
	
	}

}
