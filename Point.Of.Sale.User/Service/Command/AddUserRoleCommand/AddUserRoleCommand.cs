using Point.Of.Sale.Abstraction.Message;

namespace Point.Of.Sale.User.Service.Command.AddUserRoleCommand;

public record AddUserRoleCommand(string UserName, string RoleName) : ICommand<bool>
{
}
