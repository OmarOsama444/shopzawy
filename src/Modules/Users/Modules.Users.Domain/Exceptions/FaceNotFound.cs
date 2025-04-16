using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions;

public class FaceNotFound : NotFoundException
{
    public FaceNotFound() : base("Face.NotFound", "not matching face prints found")
    {
    }

}
