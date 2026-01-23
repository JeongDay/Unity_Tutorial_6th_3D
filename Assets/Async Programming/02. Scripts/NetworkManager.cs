using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatLog;

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

            ReceiveLoopAsync(cts.Token).Forget();
        }
        catch (Exception e)
        {
            Debug.LogError($"접속 실패: {e.Message}");
        }
    }

    private async UniTaskVoid ReceiveLoopAsync(CancellationToken token)
    {
        byte[] headerBuffer = new byte[2];

        try
        {
            while (isConnected && client.Connected)
            {
                int headerRead = await stream.ReadAsync(headerBuffer, 0, 2, token);
                if (headerRead <= 0) break;

                short bodySize = BitConverter.ToInt16(headerBuffer, 0);

                byte[] bodyBuffer = new byte[bodySize];
                int totalRead = 0;

                while (totalRead < bodySize)
                {
                    int read = await stream.ReadAsync(bodyBuffer, totalRead, bodySize - totalRead, token);
                    if (read <= 0) break;
                    totalRead += read;
                }

                string receivedText = Encoding.UTF8.GetString(bodyBuffer);
                
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