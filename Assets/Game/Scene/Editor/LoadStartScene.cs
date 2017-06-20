using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadStartScene : EditorWindow
{
	// Eidtor에 메뉴를 만들라는 의미. %h == Ctrl + H 단축키
	[MenuItem("Play/PlayStartScene %h")]

	public static void PlayStartScene()
	{
		//+ 현재 Scene이 수정되었으면 저장할 것인지 확인 후 Play 하도록.
		if (EditorSceneManager.GetActiveScene().isDirty)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		}

		EditorSceneManager.OpenScene("Assets/Scene/Test.unity");
		//scene 파일이 저장되어있는 Assets 경로를 넣어주면 됨.
		EditorApplication.isPlaying = true;
	}
}