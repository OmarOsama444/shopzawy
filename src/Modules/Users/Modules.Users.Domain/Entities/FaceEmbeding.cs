namespace Modules.Users.Domain.Entities;

public class FaceEmbeding
{
    public Guid UserId { get; private set; }
    public ICollection<float> Embeding { get; private set; } = [];
    public static FaceEmbeding Create(Guid userId, ICollection<float> Embeding)
    {
        return new FaceEmbeding() { UserId = userId, Embeding = Embeding };
    }
}
