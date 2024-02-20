
#if UNITY_IOS

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

using Gofferwall.PostProcessor;

public class BuildPostProcessor
{
	[PostProcessBuildAttribute(1)]
	public static void OnPostProcessBuild(BuildTarget target, string path)
	{
		switch (target)
		{
			case BuildTarget.iOS: BuildPostProcessorForIos.OnPostProcessBuild(path); break;
			default: break;
		}
	}
}

#endif