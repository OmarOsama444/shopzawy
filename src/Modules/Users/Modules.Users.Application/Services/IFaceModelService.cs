using Microsoft.AspNetCore.Http;

namespace Modules.Users.Application.Abstractions;

public interface IFaceModelService
{
    public Task<ICollection<float>> FaceImgToEmbeding(byte[] imgBytes);
    public float acceptanceValue { get; }
}
