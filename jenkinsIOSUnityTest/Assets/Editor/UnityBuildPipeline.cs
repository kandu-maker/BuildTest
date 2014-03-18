using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class UnityBuildPipeline {
	static string[] SCENES = FindEnabledEditorScenes();
	static string APP_NAME = "kandu";
	
	[MenuItem ("Custom/Build/Build IOS")]
	static void PerformIOSBuild ()
	{
		BuildOptions buildOptions;
		string baseProjectPath = (Application.dataPath.Replace(GetProjectName()+"/Assets",""));
		string outputPath = baseProjectPath+APP_NAME;
		
		Debug.Log ("basePath:"+baseProjectPath);
		Debug.Log ("OutputPath:"+outputPath);
		
		if(Directory.Exists(outputPath))
		{
			Debug.Log((baseProjectPath+APP_NAME)+" Directory Exists = true > append to existing xcode project");
			buildOptions = BuildOptions.AcceptExternalModificationsToPlayer | BuildOptions.Development;
		}
		else
		{
			Debug.Log((baseProjectPath+APP_NAME)+" Directory Exists = FALSE > build new xcode project");
			buildOptions = BuildOptions.None | BuildOptions.Development;
		}
		
		GenericBuild(SCENES, outputPath, BuildTarget.iPhone, buildOptions);
	}
	
	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		EditorUserBuildSettings.appendProject = true;
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
	
	static public string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		string projectName = s[s.Length - 2];
		Debug.Log("project = " + projectName);
		return projectName;
	}
}