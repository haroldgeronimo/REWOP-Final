
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_AchievementMeta : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		AchievementMeta data = (AchievementMeta)obj;
		// Add your writer.Write calls here.
		writer.Write(data.AchID);
		writer.Write(data.Name);
		writer.Write(data.Description);
		writer.Write(data.ReqQuantity);
		writer.Write(data.CurQuantity);
		writer.Write(data.granted);
		writer.Write(data.icon);
		writer.Write(data.tag);

	}
	
	public override object Read(ES2Reader reader)
	{
		AchievementMeta data = new AchievementMeta();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		AchievementMeta data = (AchievementMeta)c;
		// Add your reader.Read calls here to read the data into the object.
		data.AchID = reader.Read<System.Int32>();
		data.Name = reader.Read<System.String>();
		data.Description = reader.Read<System.String>();
		data.ReqQuantity = reader.Read<System.Int32>();
		data.CurQuantity = reader.Read<System.Int32>();
		data.granted = reader.Read<System.Boolean>();
		data.icon = reader.Read<UnityEngine.Sprite>();
		data.tag = reader.Read<System.String>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_AchievementMeta():base(typeof(AchievementMeta)){}
}
