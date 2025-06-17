using System.Windows.Input;
using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Auth.LoginWithRefresh;

public record LoginWithRefreshCommand(string Token) : ICommand<LoginResponse>;
