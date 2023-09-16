/*using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirestoreTest : MonoBehaviour
{
    async void Start()
    {
        await ReadDataAsync();
    }

    public async Task ReadDataAsync()
    {
        // Firebase 초기화
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.Create();

            // Firestore 인스턴스 가져오기
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            try
            {
                // "userid" 컬렉션에서 문서 가져오기
                QuerySnapshot snapshot = await db.Collection("userid").GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    // 문서의 ID와 데이터를 콘솔에 출력
                    Debug.Log($"문서 ID: {document.Id}");

                    // 해당 문서 내에 "userdata" 문서에 대한 참조를 생성
                    DocumentReference userdataRef = db.Collection("userid").Document("userdata");

                    // "userdata" 문서에서 필드값 가져오기
                    DocumentSnapshot userdataSnapshot = await userdataRef.GetSnapshotAsync();
                    if (userdataSnapshot.Exists)
                    {
                        Dictionary<string, object> userdata = userdataSnapshot.ToDictionary();
                        if (userdata.ContainsKey("userid") && userdata.ContainsKey("username"))
                        {
                            string userId = userdata["userid"].ToString();
                            string userName = userdata["username"].ToString();
                            Debug.Log($"사용자 ID: {userId}, 사용자 이름: {userName}");
                        }
                        else
                        {
                            Debug.LogError("필드가 존재하지 않습니다.");
                        }
                    }
                    else
                    {
                        Debug.LogError("userdata 문서가 존재하지 않습니다!");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"데이터 가져오기 오류: {e}");
            }
        }
        else
        {
            Debug.LogError($"Firebase 의존성을 모두 해결할 수 없습니다: {dependencyStatus}");
        }
    }
}*/