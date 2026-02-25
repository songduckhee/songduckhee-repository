using UnityEditor;
using UnityEngine;
using System.IO;

public class AdvancedIconMaker : EditorWindow
{
	[MenuItem("Tools/Take Front Icon (Transparent)")]
	static void TakeIcon()
	{
		GameObject prefab = Selection.activeGameObject;
		if (prefab == null) return;

		// 1. "Temp" 레이어 번호 가져오기
		int tempLayer = LayerMask.NameToLayer("Temp");
		if (tempLayer == -1)
		{
			Debug.LogError("'Temp' 레이어가 없습니다. Project Settings에서 레이어를 먼저 추가해주세요!");
			return;
		}

		// 2. 임시 물체 생성 및 레이어 설정
		GameObject tempObj = Instantiate(prefab, new Vector3(0, 100, 0), Quaternion.identity); // 다른 물체와 안 겹치게 공중에 생성
		tempObj.layer = tempLayer;

		// 자식 오브젝트들도 모두 Temp 레이어로 변경 (중요!)
		foreach (Transform child in tempObj.GetComponentsInChildren<Transform>())
		{
			child.gameObject.layer = tempLayer;
		}

		// 모델이 뒤를 보고 있다면 180도 회전 (필요시 조절)
		tempObj.transform.rotation = Quaternion.Euler(0, 0, 0);

		// 3. 카메라 설정
		GameObject camObj = new GameObject("TempIconCam");
		Camera cam = camObj.AddComponent<Camera>();

		cam.backgroundColor = new Color(0, 0, 0, 0);
		cam.clearFlags = CameraClearFlags.SolidColor;
		cam.orthographic = false;
		cam.orthographicSize = 0.2f;

		// ★ 핵심: Culling Mask를 "Temp" 레이어만 찍도록 설정
		cam.cullingMask = 1 << tempLayer;

		// 4. 카메라 위치 및 조준
		camObj.transform.position = new Vector3(0, 101f, 1.5f);
		camObj.transform.LookAt(new Vector3(0, 100.5f, 0));

		// 5. 촬영 및 저장
		RenderTexture rt = new RenderTexture(512, 512, 24);
		cam.targetTexture = rt;
		Texture2D screenShot = new Texture2D(512, 512, TextureFormat.RGBA32, false);

		cam.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
		screenShot.Apply();

		byte[] bytes = screenShot.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/" + prefab.name + "_PerfectIcon.png", bytes);

		// 6. 정리
		DestroyImmediate(camObj);
		DestroyImmediate(tempObj);
		AssetDatabase.Refresh();

		Debug.Log("레이어 필터링 아이콘 저장 완료!");
	}
}
