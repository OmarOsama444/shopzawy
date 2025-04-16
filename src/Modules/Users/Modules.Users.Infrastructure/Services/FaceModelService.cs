using System.Text.Json;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Infrastructure.Services;

public class FaceModelService(string url) : IFaceModelService
{
    public float acceptanceValue => 0.9f;

    public async Task<ICollection<float>> FaceImgToEmbeding(byte[] imgBytes)
    {
        using HttpClient client = new HttpClient();
        using ByteArrayContent content = new ByteArrayContent(imgBytes);
        HttpResponseMessage response = await client.PostAsync(url + "/upload", content);
        string json = await response.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        return JsonSerializer.Deserialize<ICollection<float>>(json)!;
    }

}
