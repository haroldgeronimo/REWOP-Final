
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_CharStats : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		CharStats data = (CharStats)obj;
		// Add your writer.Write calls here.

	}
	
	public override object Read(ES2Reader reader)
	{
		CharStats data = GetOrCreate<CharStats>();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		CharStats data = (CharStats)c;
		// Add your reader.Read calls here to read the data into the object.

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_CharStats():base(typeof(CharStats)){}
}
