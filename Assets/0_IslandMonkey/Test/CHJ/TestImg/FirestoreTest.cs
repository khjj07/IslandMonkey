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
        // Firebase �ʱ�ȭ
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.Create();

            // Firestore �ν��Ͻ� ��������
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            try
            {
                // "userid" �÷��ǿ��� ���� ��������
                QuerySnapshot snapshot = await db.Collection("userid").GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    // ������ ID�� �����͸� �ֿܼ� ���
                    Debug.Log($"���� ID: {document.Id}");

                    // �ش� ���� ���� "userdata" ������ ���� ������ ����
                    DocumentReference userdataRef = db.Collection("userid").Document("userdata");

                    // "userdata" �������� �ʵ尪 ��������
                    DocumentSnapshot userdataSnapshot = await userdataRef.GetSnapshotAsync();
                    if (userdataSnapshot.Exists)
                    {
                        Dictionary<string, object> userdata = userdataSnapshot.ToDictionary();
                        if (userdata.ContainsKey("userid") && userdata.ContainsKey("username"))
                        {
                            string userId = userdata["userid"].ToString();
                            string userName = userdata["username"].ToString();
                            Debug.Log($"����� ID: {userId}, ����� �̸�: {userName}");
                        }
                        else
                        {
                            Debug.LogError("�ʵ尡 �������� �ʽ��ϴ�.");
                        }
                    }
                    else
                    {
                        Debug.LogError("userdata ������ �������� �ʽ��ϴ�!");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"������ �������� ����: {e}");
            }
        }
        else
        {
            Debug.LogError($"Firebase �������� ��� �ذ��� �� �����ϴ�: {dependencyStatus}");
        }
    }
}*/