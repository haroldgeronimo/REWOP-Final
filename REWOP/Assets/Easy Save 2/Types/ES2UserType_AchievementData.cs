
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_AchievementData : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		AchievementData data = (AchievementData)obj;
		// Add your writer.Write calls here.

	}
	
	public override object Read(ES2Reader reader)
	{
		AchievementData data = GetOrCreate<AchievementData>();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		AchievementData data = (AchievementData)c;
		// Add your reader.Read calls here to read the data into the object.

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_AchievementData():base(typeof(AchievementData)){}
}
