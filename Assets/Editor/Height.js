/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// @author : Bloc 
// @date   : 01.08.2015
// @version: 1.0v  
// About : Simple terrain tool which lets you use nearly any texture whether its colored or not. 
// 
// Please be caution while you are editing the code. You are not allowed to sell this asset. For basic info please check documentation yet , 
// I tried my best to explain everything in the comment sections.
// It's a basic code , you have all right to improve it for your own. I would be happy If you let me know If you find any problem or improvement. 
// You can contact with me from cartridgegamestudion@gmail.com / Thanks to Eric Haines (Eric5h5) for his OSP code
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

 
static function ApplyHeightmap (selectedTexture :  Texture2D , colored : boolean , terraHeigh : float , useIt : boolean , flatTer : boolean , redVal : float , greenVal : float , blueVal : float) {
	//Variable for heightmap , we will use this a lot in upcoming lines 
	var heightmap : Texture2D = selectedTexture;
	
	//Checks if heightmap loaded or not
	if (heightmap == null) { 
		EditorUtility.DisplayDialog("Texture isn't selected", "Please Select a Texture", "Cancel"); 
		return; 
	}
	
	Undo.RegisterUndo (Terrain.activeTerrain.terrainData, "Heightmap From Texture");
	
	try {

	var terrain = Terrain.activeTerrain.terrainData;
	var w = heightmap.width;
	var h = heightmap.height;
	var w2 = terrain.heightmapWidth;
	var heightmapData = terrain.GetHeights(0, 0, w2, w2);
	var mapColors = heightmap.GetPixels();
	var map = new Color[w2 * w2];
 
	if (w2 != w || h != w) {
		// Resize using nearest-neighbor scaling if texture has no filtering
		if (heightmap.filterMode == FilterMode.Point) {
			var dx : float = parseFloat(w)/w2;
			var dy : float = parseFloat(h)/w2;
			for (y = 0; y < w2; y++) {
				if (y%20 == 0) {
					EditorUtility.DisplayProgressBar("Resize", "Calculating texture", Mathf.InverseLerp(0.0, w2, y));
				}
				var thisY = parseInt(dy*y)*w;
				var yw = y*w2;
				for (x = 0; x < w2; x++) {
					map[yw + x] = mapColors[thisY + dx*x];
				}
			}
		}
		// Otherwise resize using bilinear filtering
		else {
			var ratioX = 1.0/(parseFloat(w2)/(w-1));
			var ratioY = 1.0/(parseFloat(w2)/(h-1));
			for (y = 0; y < w2; y++) {
				if (y%20 == 0) {
					EditorUtility.DisplayProgressBar("Resize", "Calculating texture", Mathf.InverseLerp(0.0, w2, y));
				}
				var yy = Mathf.Floor(y*ratioY);
				var y1 = yy*w;
				var y2 = (yy+1)*w;
				yw = y*w2;
				for (x = 0; x < w2; x++) {
					var xx = Mathf.Floor(x*ratioX);
 
					var bl = mapColors[y1 + xx];
					var br = mapColors[y1 + xx+1]; 
					var tl = mapColors[y2 + xx];
					var tr = mapColors[y2 + xx+1];
 
					var xLerp = x*ratioX-xx;
					map[yw + x] = Color.Lerp(Color.Lerp(bl, br, xLerp), Color.Lerp(tl, tr, xLerp), y*ratioY-yy);
				}
			}
		}
		EditorUtility.ClearProgressBar();
	}
	else {
		// Use original if no resize is needed
		map = mapColors;
	}
 
    
	// Assign texture data to heightmap
	for (y = 0; y < w2; y++) {
		for (x = 0; x < w2; x++) {
		
		var red   = map[y*w2+x].r;
		var blue  = map[y*w2+x].b;
		var green = map[y*w2+x].g;
		
		var hue = 0f;
		var sat = 0f; 
		var val = 0f;
		
		//Change RGB values to HSV therefore detecting the value range between colors become more possible
		Color.RGBToHSV(map[y*w2+x],hue,sat,val);
		
		//Check if Its colored
		if(colored){
		  //Check if User want to use Flat Terrain
		  if(flatTer)
		  { 
             if(hue*360f< 280f &&  hue*360f > 160f)
		        heightmapData[y,x] = map[y*w2+x].grayscale * (-hue* terraHeigh * blueVal); 
		     else if((val*100f < 70) && (hue*360f >= 45) || (hue*360f <= 327) &&  (val*100f < 70))
		        heightmapData[y,x] = 0.1f  * map[y*w2+x].grayscale * terraHeigh * greenVal;
		     else if((val*100f < 70) && (hue*360f <= 45))
		        heightmapData[y,x] = 0.08f * map[y*w2+x].grayscale * terraHeigh * redVal;
		     else 
		        heightmapData[y,x] = 0.17f * map[y*w2+x].grayscale * terraHeigh;     		  		   
		  }
		  else
		  {
          if(hue*360f< 280f &&  hue*360f > 160f)
		     heightmapData[y,x] = map[y*w2+x].grayscale * (-hue* terraHeigh * blueVal); 
		  else if((hue*360f < 160) && (hue*360f >= 45))
		     heightmapData[y,x] = map[y*w2+x].grayscale * 0.3 * terraHeigh * greenVal;
		  else
		     heightmapData[y,x] = map[y*w2+x].grayscale * 0.4 * terraHeigh * redVal;  

		  }              
		}
		else
		{
		 if((val*100f < 2))
		   heightmapData[y,x] = map[y*w2+x].grayscale * -terraHeigh;
		 heightmapData[y,x] = ((val)*0.3) * terraHeigh ;
		}
		      
		}
	}

	//Set data to terrain
	terrain.SetHeights(0, 0, heightmapData);
	
	//If user want to use image as a texture
	if(useIt)
	{
     var test : SplatPrototype[] = new SplatPrototype[1];
    
     test[0] = new SplatPrototype(); 
     test[0].texture = heightmap;
     test[0].tileOffset = new Vector2(0, 0);
     test[0].tileSize = new Vector2( terrain.size.x   , terrain.size.z );
    
     terrain.splatPrototypes = test;
    }
    
    
    }
    catch (e : UnityException)
    {
         EditorUtility.DisplayDialog("Texture Format Problem!", "Texture should be readable!", "Cancel"); 
		 return; 
    }
}

class TerrainHeight extends EditorWindow {
	var groupEnabled = false;
    var texture     : Texture2D;
    var coloredPick : boolean = true;
    var colorUse    : boolean = true;
    var flat        : boolean = false;
    var changeColorVals : boolean = false;
    var terraHeig   : float = 1.0f;
    
    var redVal      : float = 1.0f;
    var greenVal    : float = 1.0f;
    var blueVal     : float = 1.0f;
	
	// Add menu named "My Window" to the Window menu
	@MenuItem ("Tools/Terrain/Open TerraHE")
	static function Init () {
		// Get existing open window or if none, make a new one:		
		var window = GetWindow(TerrainHeight);
	    window.position = Rect(50,50,600, 600);
		window.Show();
	}
	
	function OnGUI () {
	    
	    
        texture = EditorGUI.ObjectField(Rect(10,200,200,150),"Add a Texture:",texture,Texture);
        
        terraHeig   = EditorGUILayout.Slider("Terrain Height : " , terraHeig, 0.1, 2);
        colorUse    = EditorGUILayout.Toggle("Use Image as Texture : ", colorUse);
        
    	coloredPick = EditorGUILayout.BeginToggleGroup("Colored Texture?", coloredPick);
    	   flat        = EditorGUILayout.Toggle("Make Flat Areas : ", flat);
    	EditorGUILayout.EndToggleGroup(); 
    	
    	changeColorVals = EditorGUILayout.BeginToggleGroup("Customize Color Heights" , changeColorVals);
    	    redVal  = EditorGUILayout.Slider("Red Color Height : " , redVal , 0.5 , 3 );
    	    greenVal= EditorGUILayout.Slider("Green Color Height : " , greenVal , 0.5 , 3 );
    	    blueVal = EditorGUILayout.Slider("Blue Color Height : " , blueVal , 0.5 , 3 );
    	EditorGUILayout.EndToggleGroup();      
		
		if(!changeColorVals)
		 {  
		   redVal = 1.0f;
		   greenVal = 1.0f;
		   blueVal = 1.0f;	
		  }		
					
		if(GUI.Button(Rect(208, position.height - 30, position.width/2, 20),"Make It")) {
				//Copy the new texture
		   Height.ApplyHeightmap(texture , coloredPick , terraHeig , colorUse , flat , redVal ,greenVal ,blueVal );
		}
			
		if(texture) {
	       EditorGUI.PrefixLabel(Rect( 280 ,200 ,100,15),0,GUIContent("Selected Texture:"));
		   EditorGUI.DrawPreviewTexture(Rect( 300 , 200 ,50,50),texture);
		} else {
		   EditorGUI.PrefixLabel(
		   Rect(3,position.height - 25,position.width - 6, 20),0,GUIContent("No texture found"));
			}
	}
}