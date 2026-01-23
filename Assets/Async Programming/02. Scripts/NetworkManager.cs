using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using Cysharp.Threading.Tasks; // UniTask 사용을 위해 필수
using System.Threading;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatLog; // 서버 응답을 보여줄 텍스트

    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;
    private CancellationTokenSource cts;

    private void Awake()
    {
        cts = new CancellationTokenSource();
        sendButton.onClick.AddListener(() => SendPacketAsync(inputField.text).Forget());
    }

    async void Start()
    {
        await ConnectToServerAsync();
    }

    private async UniTask ConnectToServerAsync()
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 7979).AsUniTask().AttachExternalCancellation(cts.Token);
            stream = client.GetStream();
            isConnected = true;
            Debug.Log("[Step 1] 접속 성공");

            // [Step 3] 접속 성공 직후 수신 루프 실행
            ReceiveLoopAsync(cts.Token).Forget();
        }
        catch (Exception e)
        {
            Debug.LogError($"접속 실패: {e.Message}");
        }
    }

    // [Step 3] 서버로부터 메시지를 받는 무한 루프
    private async UniTaskVoid ReceiveLoopAsync(CancellationToken token)
    {
        byte[] headerBuffer = new byte[2];

        try
        {
            while (isConnected && client.Connected)
            {
                // 1. 헤더(2바이트) 읽기
                int headerRead = await stream.ReadAsync(headerBuffer, 0, 2, token);
                if (headerRead <= 0) break;

                short bodySize = BitConverter.ToInt16(headerBuffer, 0);

                // 2. 바디(실제 데이터) 읽기
                byte[] bodyBuffer = new byte[bodySize];
                int totalRead = 0;

                // TCP 특성상 바디가 잘려 올 수 있으므로 다 올 때까지 반복 수신
                while (totalRead < bodySize)
                {
                    int read = await stream.ReadAsync(bodyBuffer, totalRead, bodySize - totalRead, token);
                    if (read <= 0) break;
                    totalRead += read;
                }

                // 3. 메시지 해석 및 출력
                string receivedText = Encoding.UTF8.GetString(bodyBuffer);
                
                // 유니티 메인 스레드에서 UI 업데이트
                chatLog.text += $"\n[Server]: {receivedText}";
                Debug.Log($"[Step 3] 수신 완료: {receivedText}");
            }
        }
        catch (Exception e) when (!(e is OperationCanceledException))
        {
            Debug.LogError($"수신 에러: {e.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

    private async UniTaskVoid SendPacketAsync(string message)
    {
        if (!isConnected || string.IsNullOrEmpty(message)) return;

        try
        {
            // 1. 서버로 보내기 전, 내 화면에 먼저 표시
            await UniTask.SwitchToMainThread();
            if (chatLog != null)
            {
                chatLog.text += $"\n[Client]: {message}";
            }
            
            byte[] bodyData = Encoding.UTF8.GetBytes(message);
            short bodyLength = (short)bodyData.Length;
            byte[] headerData = BitConverter.GetBytes(bodyLength);

            byte[] fullPacket = new byte[headerData.Length + bodyData.Length];
            Array.Copy(headerData, 0, fullPacket, 0, headerData.Length);
            Array.Copy(bodyData, 0, fullPacket, headerData.Length, bodyData.Length);

            await stream.WriteAsync(fullPacket, 0, fullPacket.Length, cts.Token);
            inputField.text = "";
        }
        catch (Exception e)
        {
            Debug.LogError($"송신 에러: {e.Message}");
        }
    }

    private void Disconnect()
    {
        isConnected = false;
        stream?.Close();
        client?.Close();
        Debug.Log("연결 종료");
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
        Disconnect();
    }
}