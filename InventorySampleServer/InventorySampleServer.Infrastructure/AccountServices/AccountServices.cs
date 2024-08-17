using Common;
using Common.Enum;
using System.Text;
using Common.Common;
using Model.Custom.Account;
using System.Security.Claims;
using static Dapper.SqlMapper;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.AccountServices
{
    public class AccountServices
    {
        private readonly string ConnectionString;

        public AccountServices(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public async Task<ResultDto> Login(LoginDto Entity, HttpContext HttpContext, string Key, double Minutes) 
        {
            #region Login
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();
            try
            {
                Entity.Password = new SymCryptography().Encrypt(Entity.Password);
                var userInfo = await CheckLoginInfo(Entity, Connection, Transaction);

                if (userInfo == null)
                    return new Return().ReturnException(new Exception(MessageEnum.کد_پرسنلی_اشتباه_است.EnumToString()), MessagesType.هشدار.EnumToString(), StatusCode.Ok);
                else
                {
                    userInfo = await CheckUserPassword(Entity, Connection, Transaction);
                    if (userInfo == null)
                        return new Return().ReturnException(new Exception(MessageEnum.رمز_عبور_اشتباه_است.EnumToString()), MessagesType.هشدار.EnumToString(), StatusCode.Ok);
                    else
                    {
                        if (!userInfo.IsActive)
                            return new Return().ReturnException(new Exception(MessageEnum.اکانت_کاربر_غیرفعال_می_باشد.EnumToString()), MessagesType.هشدار.EnumToString(), StatusCode.Ok);
                    }

                    #region GetUserDetails
                    var userDetails = new UserDetailDto()
                    {
                        Id = userInfo.Id,
                        Code = userInfo.Code,
                        Mobile = userInfo.Mobile,
                        FullName = userInfo.FullName,
                        IsActive = userInfo.IsActive,
                        Picture = userInfo.Picture ?? string.Empty,
                        UserOperationRoleList = new List<OperationRoleDto>()
                    };

                    var rolls = await GetUserRolls(userInfo.Id, Connection, Transaction);
                    foreach (var roll in rolls)
                    {
                        userDetails.UserOperationRoleList.Add(new OperationRoleDto()
                        {
                            OperationRoleTypeEnumId = roll.OperationRoleTypeEnumId,
                            OperationRoleTypeEnumTitle = roll.OperationRoleTypeEnumTitle
                        });
                    }
                    #endregion

                    #region GenerateToken
                    userDetails.Token = CreateToken(userDetails, Key, Minutes);
                    #endregion

                    #region SaveSession
                    var userSession = new UserSessionEntity()
                    {
                        Agent = HttpContext.GetUserAgent(),
                        IpAddress = HttpContext.GetClientIp(),
                        UserId = userDetails.Id,
                        StartDateTime = await Conversion.GetServerShamsiDateTime(),
                        Token = userDetails.Token,
                    };
                    var sessionId = await SaveSession(userSession, Connection, Transaction);
                    #endregion

                    Transaction.Commit();
                    return new Return().ReturnData(userDetails, StatusType.ثبت, Message: MessageEnum.ورود_موفق.EnumToString());
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex, MessagesType.هشدار.EnumToString());
            }
            #endregion
        }

        public async Task<UserDto?> CheckLoginInfo(LoginDto Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region CheckLoginInfo
            try
            {
                var Command = @"SELECT 
                                    [Id],
									[FullName],
									[IsActive],
									[Picture],
									[Mobile],
									[Code]
								FROM
									[General].[User]
							    WHERE
								    [Code] = @Code";

                return await Connection.QueryFirstOrDefaultAsync<UserDto>(Command, new { Entity.Code }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }

        public async Task<UserDto?> CheckUserPassword(LoginDto Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region CheckLoginInfo
            try
            {
                var Command = @"SELECT 
                                    [Id],
									[FullName],
									[IsActive],
									[Picture],
									[Mobile],
									[Code]
								FROM
									[General].[User]
							    WHERE
								    [Code] = @Code
									AND 
                                    [Password] = @Password";

                return await Connection.QueryFirstOrDefaultAsync<UserDto>(Command, new { Entity.Password, Entity.Code }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }

        public async Task<IEnumerable<UserRollsListDto>> GetUserRolls(int Id, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region GetUserRolls
            try
            {
                var Command = @"SELECT
									ORTE.Id OperationRoleTypeEnumId,
									ORTE.Title OperationRoleTypeEnumTitle,
									UOR.UserId
								FROM
									[General].[UserOperationRole] UOR
									INNER JOIN [General].[OperationRoleTypeEnum] ORTE ON ORTE.Id = UOR.OperationRoleTypeEnumId
									
								WHERE 
								    UOR.UserId = @Id
								    AND 
                                    UOR.IsAcive = 1";

                return await Connection.QueryAsync<UserRollsListDto>(Command, new { Id }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }

        public async Task<int> SaveSession(UserSessionEntity Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region SaveSession
            try
            {
                var Command = @"INSERT INTO [Service].[UserSession]
                                                (
                                                    UserId,
                                                    IpAddress,
                                                    Agent,
                                                    StartDateTime,
                                                    EndDateTime,
                                                    Token
                                                )
                                    OUTPUT INSERTED.Id
                                          VALUES
                                                (
                                                    @UserId,
                                                    @IpAddress,
                                                    @Agent,
                                                    @StartDateTime,
                                                    @EndDateTime,
                                                    @Token
                                                )";

                var Id = await Connection.ExecuteScalarAsync<int>(Command, Entity, transaction: Transaction);
                return Id;
            }
            catch { throw; }
            #endregion
        }

        public async Task<ResultDto> GetMenuListByUserId(int UserId)
        {
            #region GetMenuListByUserId
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var Command = @"SELECT	
                                    M.Id,
                                	M.Title,
                                	M.[Url] RouterLink,
                                	NULL Href,
                                	M.ICON,
                                	NULL [Target],
                                	CAST(ISNULL(PM.Id,0) AS BIT) HasSubMenu, 
                                	ISNULL(M.ParentId,0) ParentId
                                FROM
                                	Service.Menu M
                                	LEFT JOIN 
                                		(
                                			SELECT 
												DISTINCT PM.Id
                                			FROM 
												[Service].Menu PM
                                				INNER JOIN Service.Menu SM ON SM.ParentId = PM.Id
                                
                                		)PM ON PM.Id=M.Id 
                                WHERE
                                	M.Id IN 
										(
                                			SELECT 
												DISTINCT MA.MenuId
                                			FROM	
												[Service].MenuAccess MA 
                                			WHERE   
												@UserId = MA.UserId
												OR
                                				MA.OperationRoleTypeEnumId IN ( SELECT UOR.OperationRoleTypeEnumId FROM General.UserOperationRole UOR WHERE UOR.UserId = @UserId AND UOR.IsAcive = 1 )
                                		)";

                var Data = await Connection.QueryAsync<MenuListDto>(Command, new { UserId }, transaction: Transaction);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }

        public async Task<ResultDto> HasMenuPageAccess(int UserId, int MenuId)
        {
            #region HasMenuPageAccess
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var Command = @"SELECT  
	                                CAST(COUNT(*) AS BIT) HasAccess
                                FROM 
	                                Service.MenuAccess MA 
                                WHERE   
	                                (
		                                @UserId = MA.UserId
		                                OR
		                                MA.OperationRoleTypeEnumId IN ( SELECT UOR.OperationRoleTypeEnumId FROM General.UserOperationRole UOR WHERE UOR.UserId = @UserId AND UOR.IsAcive=1 )
	                                )
	                                AND
	                                MA.MenuId = @MenuId";

                var Data = await Connection.QueryFirstAsync<HasComponentAccessDto>(Command, new { UserId, MenuId }, transaction: Transaction);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }

        public async Task<ResultDto> GetMenuPageItemAccessList(int UserId, int MenuId)
        {
            #region GetMenuPageItemAccessList
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var Command = @"SELECT 
	                                PIT.Id,
	                                PIT.PageItemEnumId,
									PIE.Title PageItemEnumTitle
                                FROM	
	                                General.PageItem PIT
									INNER JOIN General.PageItemEnum PIE ON PIE.Id=PIT.PageItemEnumId
                                WHERE
	                                PIT.Id IN 
										(
        		                                SELECT 
													DISTINCT PIA.PageItemId
        		                                FROM	
													General.PageItemAccess PIA 
        		                                WHERE   
													@UserId = PIA.UserId
              				                        OR
              				                        PIA.OperationRoleTypeEnumId IN ( SELECT UOR.OperationRoleTypeEnumId FROM General.UserOperationRole UOR WHERE UOR.UserId=@UserId AND UOR.IsAcive=1 )
     									)
	                                AND
	                                PIT.MenuId = @MenuIdd";

                var Data = await Connection.QueryAsync(Command, new { UserId, MenuId }, transaction: Transaction);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }

        public async Task<ResultDto> IsLogin(string Token, string Issuer, string Audience, string Key)
        {
            #region IsLogin
            var Result = new ResultDto();
            Result.IsSucceed = await CheckToken(Token, Issuer, Audience, Key);

            if (Result.IsSucceed)
                Result.Message = MessageEnum.ورود_معتبر.EnumToString();
            else
                Result.Message = MessageEnum.ورود_نامعتبر.EnumToString();

            return Result;
            #endregion
        }

        private async Task<bool> CheckToken(string Token, string Issuer, string Audience, string Key)
        {
            #region CheckToken
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false, // Because there is no audiance in the generated token
                    ValidateIssuer = false,   // Because there is no issuer in the generated token
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)) 
                };

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(Token, validationParameters, out validatedToken);
                //return  true; // ValidateToken throws an exception if it's not valid. Therefore, if it gets to this line, it's valid

                return await Task.Run(() => { return true; });
            }
            catch (Exception) { return false; }
            #endregion
        }

        private string CreateToken(UserDetailDto UserDetail, string Key, double Minutes)
        {
            #region Create Token
            try
            {
                var TokenKey = Encoding.UTF8.GetBytes(Key);
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.Name, "Admin"),
                            new Claim("UserId", UserDetail.Id.ToString()),
                            new Claim("FullName", UserDetail.FullName.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(Minutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var Token = TokenHandler.CreateToken(TokenDescriptor);

                return TokenHandler.WriteToken(Token);
            }
            catch (Exception) { throw; }
            #endregion
        }

        public async Task<ResultDto> MenuAccessList(int UserId)
        {
            #region MenuAccessList
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var Command = @"SELECT
	                                M.Id,
									M.Title,
									M.Icon,
									M.ParentId,
									M.[Url],
									MA.Id MenuAccessId,
									MA.OperationRoleTypeEnumId
                                FROM
	                                [Service].MenuAccess MA
	                                INNER JOIN [Service].Menu M ON M.Id = MA.MenuId
                                WHERE
									MA.UserId = @UserId
									OR
									(
										MA.OperationRoleTypeEnumId IN 
												(
													SELECT
														UOR.OperationRoleTypeEnumId
													FROM
														[General].UserOperationRole UOR
							                               
													WHERE
														UOR.UserId = @UserId
														AND
														UOR.IsAcive = 1
												)
									)";

                var Data = await Connection.QueryAsync<MenuAccessListDto>(Command, new { UserId }, transaction: Transaction);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
    }
}

