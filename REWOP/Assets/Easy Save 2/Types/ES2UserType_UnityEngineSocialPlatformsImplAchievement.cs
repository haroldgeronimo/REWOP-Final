
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ES2UserType_UnityEngineSocialPlatformsImplAchievement : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.SocialPlatforms.Impl.Achievement data = (UnityEngine.SocialPlatforms.Impl.Achievement)obj;
		// Add your writer.Write calls here.

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.SocialPlatforms.Impl.Achievement data = new UnityEngine.SocialPlatforms.Impl.Achievement();
		Read(reader, data);
		return data;
	}
	
	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.SocialPlatforms.Impl.Achievement data = (UnityEngine.SocialPlatforms.Impl.Achievement)c;
		// Add your reader.Read calls here to read the data into the object.

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineSocialPlatformsImplAchievement():base(typeof(UnityEngine.SocialPlatforms.Impl.Achievement)){}
}
