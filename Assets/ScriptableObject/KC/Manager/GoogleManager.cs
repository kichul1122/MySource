//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Sirenix.OdinInspector;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu(menuName ="Manager/Google")]
//public class GoogleManager : SerializedScriptableObject
//{
//	//public GameEvent onGoogleLoginSuccess;
//	//public GameEvent onGoogleLoginFail;

//	//public StringVariableRx googleID;
//	//public StringVariableRx editorID;

//	//public StringVariableRx logoState;

//	public void Initialize()
//	{
//		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
//			.Builder()
//			.RequestServerAuthCode(false)
//			.RequestIdToken()
//			.Build();
//		//커스텀된 정보로 GPGS 초기화
//		PlayGamesPlatform.InitializeInstance(config);
//		PlayGamesPlatform.DebugLogEnabled = true;
//		//GPGS 시작.
//		PlayGamesPlatform.Activate();
//	}

//	public void GoogleLogin()
//	{

//		// 이미 로그인 된 경우
//		//if (Social.localUser.authenticated == true)
//		//{
//		//	//BackendReturnObject BRO = back.BMember.SignUp(GetTokens(), "google");
//		//	googleID.Value = ((PlayGamesLocalUser)Social.localUser).id;
//		//	onGoogleLoginSuccess?.Raise();
//		//}
//		//else
//		{
//			Social.localUser.Authenticate((bool success) =>
//			{
//				if (success)
//				{
//					// 로그인 성공 -> 뒤끝 서버에 획득한 구글 토큰으로 가입요청
//					//BackendReturnObject BRO = back.BMember.SignUp(GetTokens(), "google");
//					//googleID.Value = ((PlayGamesLocalUser)Social.localUser).id;
//					//onGoogleLoginSuccess?.Raise();
//					//NGUIDebug.Log("Login Success");
//					//logoState.Value = "Login Success";
//				}
//				else
//				{
//#if UNITY_EDITOR
//					//googleID.Value = editorID.Value;
//					//onGoogleLoginSuccess?.Raise();
//					//logoState.Value = "Login Success";
//#else
//					//onGoogleLoginFail?.Raise();
//					//NGUIDebug.Log("Login failed for some reason");
//					//logoState.Value = "Login Fail";
//#endif
//					//NGUIDebug.Log("Login failed for some reason");
//				}
//			});
//		}
//	}

//	public string GetTokens()
//	{
//		if (PlayGamesPlatform.Instance.localUser.authenticated)
//		{
//			// 유저 토큰 받기 첫번째 방법
//			string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
//			// 두번째 방법
//			// string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
//			return _IDtoken;
//		}
//		else
//		{
//			Debug.Log("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
//			return null;
//		}
//	}

//	public void GameQuit()
//	{
//		Application.Quit();
//	}
//}
