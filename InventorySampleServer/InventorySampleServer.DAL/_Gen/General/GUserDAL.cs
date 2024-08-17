using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.General
{
	public class GUserDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GUserDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[U].[Id],
									[U].[Version],
									[U].[CreatedBy],
									[U].[CreatedDateTime],
									[U].[UpdatedBy],
									[U].[UpdatedDateTime],
									[U].[FullName],
									'' [Guid],
									CAST(1 AS BIT) [IsImage],
									[U].[Picture],
									[U].[Code],
									[U].[Mobile],
									CAST(1 AS BIT) Editable
								FROM
									[General].[User] [U]
								WHERE
									[U].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<TEntity> GetObjectById(int Id)
		{
			#region GetObjectById
			try
			{
				var Command = @"SELECT
									[U].[Id],
									[U].[Version],
									[U].[CreatedBy],
									[U].[CreatedDateTime],
									[U].[UpdatedBy],
									[U].[UpdatedDateTime],
									[U].[FullName],
									[U].[Picture],
									[U].[Code],
									[U].[Mobile]
								FROM
									[General].[User] [U] 
								WHERE
									[U].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(bool? EditMode = null)
		{
			#region GetList
			try
			{
				var Command = @"SELECT
									[U].[Id],
									[U].[Version],
									[U].[CreatedBy],
									[U].[CreatedDateTime],
									[U].[UpdatedBy],
									[U].[UpdatedDateTime],
									[U].[FullName],
									'' [Guid],
									CAST(1 AS BIT) [IsImage],
									[U].[Picture],
									[U].[Code],
									[U].[Mobile],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[User] [U]";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			try
			{
				var Offset = 0;
				var Size = int.MaxValue;
				if (PageNumber != null && PageSize != null)
				{
					Size = PageSize.Value;
					Offset = OffSet(PageNumber.Value, PageSize.Value);
				}

				var Command = @"SELECT
									[U].[Id],
									[U].[Version],
									[U].[CreatedBy],
									[U].[CreatedDateTime],
									[U].[UpdatedBy],
									[U].[UpdatedDateTime],
									[U].[FullName],
									'' [Guid],
									CAST(1 AS BIT) [IsImage],
									[U].[Picture],
									[U].[Code],
									[U].[Mobile],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[User] [U]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[Code] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[Mobile] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [U].[Id] END ASC,
									CASE WHEN @SortField = 'FullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'FullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'DESC' THEN [U].[Code] END DESC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'ASC' THEN [U].[Code] END ASC,
									CASE WHEN @SortField = 'Mobile' AND @Direction = 'DESC' THEN [U].[Mobile] END DESC,
									CASE WHEN @SortField = 'Mobile' AND @Direction = 'ASC' THEN [U].[Mobile] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Add(TEntity Entity)
		{
			#region Add
			try
			{
				var Command = @"INSERT INTO [General].[User]
												(
													[CreatedBy],
													[CreatedDateTime],
													[FullName],
													[Picture],
													[Code],
													[Mobile]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@FullName,
													@Picture,
													@Code,
													@Mobile
												) ";

				return await Connection.ExecuteScalarAsync<int>(Command, Entity, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Edit(TEntity Entity)
		{
			#region Edit
			try
			{
				var Command = @"UPDATE [General].[User] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[FullName] = @FullName,
									[Picture] = @Picture,
									[Code] = @Code,
									[Mobile] = @Mobile
								WHERE
									[Id] = @Id ";

				return await Connection.ExecuteAsync(Command, Entity, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Delete(int Id)
		{
			#region Delete
			try
			{
				var Command = @"Delete [General].[User] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
