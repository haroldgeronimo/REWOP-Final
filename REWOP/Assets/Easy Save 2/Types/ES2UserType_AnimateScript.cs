
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_AnimateScript : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		AnimateScript data = (AnimateScript)obj;
		// Add your writer.Write calls here.
		writer.Write(data.questNumber);
		writer.Write(data.ParamName);
		writer.Write(data.State);

	}
	
	public override object Read(ES2Reader reader)
	{
		AnimateScript data = GetOrCreate<AnimateScript>();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		AnimateScript data = (AnimateScript)c;
		// Add your reader.Read calls here to read the data into the object.
		data.questNumber = reader.Read<System.Int32>();
		data.ParamName = reader.Read<System.String>();
		data.State = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_AnimateScript():base(typeof(AnimateScript)){}
}
