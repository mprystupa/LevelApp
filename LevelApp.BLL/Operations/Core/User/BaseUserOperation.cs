using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Helpers;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Core;

namespace LevelApp.BLL.Operations.Core.User
{
    public class BaseUserOperation<TParameter, TResult> : BaseOperation<TParameter, TResult>
    {
        protected void ValidateUserPassword(DAL.Models.Core.AppUser user, string password)
        {
            if (!CryptoHelper.ValidatePassword(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BusinessValidationException("Password is invalid.");
            }
        }
    }
}