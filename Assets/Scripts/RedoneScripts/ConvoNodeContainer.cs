using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("ConvoCollection")]
public class ConvoNodeContainer {
	[XmlArray("Convos"), XmlArrayItem("ConvoNode")]

	public ConvoNode[] Convos;

	public void Save(string path){
		var serializer = new XmlSerializer(typeof(ConvoNodeContainer));
		using(var stream = new FileStream(path, FileMode.Create)){
			serializer.Serialize(stream, this);
		}

	}

	public static ConvoNodeContainer Load(string path){
		var serializer = new XmlSerializer(typeof(ConvoNodeContainer));
		using(var stream = new FileStream(path, FileMode.Open)){
			return serializer.Deserialize(stream) as ConvoNodeContainer;
		}
	}

}
