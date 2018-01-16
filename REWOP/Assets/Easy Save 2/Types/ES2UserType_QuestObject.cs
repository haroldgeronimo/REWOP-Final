
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_QuestObject : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		QuestObject data = (QuestObject)obj;
		// Add your writer.Write calls here.
		writer.Write(data.questNumber);
		writer.Write(data.Title);
		writer.Write(data.Description);
		writer.Write(data.IsCollection);
		writer.Write(data.IsBoss);
		writer.Write(data.EndInstant);
		writer.Write(data.itemTag);
		writer.Write(data.currentCount);
		writer.Write(data.required);

	}
	
	public override object Read(ES2Reader reader)
	{
		QuestObject data = GetOrCreate<QuestObject>();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		QuestObject data = (QuestObject)c;
		// Add your reader.Read calls here to read the data into the object.
		data.questNumber = reader.Read<System.Int32>();
		data.Title = reader.Read<System.String>();
		data.Description = reader.Read<System.String>();
		data.IsCollection = reader.Read<System.Boolean>();
		data.IsBoss = reader.Read<System.Boolean>();
		data.EndInstant = reader.Read<System.Boolean>();
		data.itemTag = reader.Read<System.String>();
		data.currentCount = reader.Read<System.Int32>();
		data.required = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_QuestObject():base(typeof(QuestObject)){}
}
