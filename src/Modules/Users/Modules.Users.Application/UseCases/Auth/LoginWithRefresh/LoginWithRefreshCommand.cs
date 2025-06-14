using System.Windows.Input;
using Modules.Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Auth.LoginWithRefresh;

public record LoginWithRefreshCommand(string Token) : ICommand<LoginResponse>;
