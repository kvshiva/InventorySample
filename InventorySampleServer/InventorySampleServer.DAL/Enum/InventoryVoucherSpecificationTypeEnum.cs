using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL.Enum
{
	public class InventoryVoucherSpecificationTypeEnumDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public InventoryVoucherSpecificationTypeEnumDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[Ratio]
								FROM
									[Store].[InventoryVoucherSpecificationTypeEnum]
								WHERE
									[Id] = @Id ";

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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[Ratio]
								FROM
									[Store].[InventoryVoucherSpecificationTypeEnum]
								WHERE
									[Id] = @Id ";

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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[Ratio],
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecificationTypeEnum]";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList By Parameters
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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[Ratio],
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecificationTypeEnum]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											Title LIKE N'%' + @SearchValue + '%'
											OR
											Ratio LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [Title] END ASC,
									CASE WHEN @SortField = 'Ratio' AND @Direction = 'DESC' THEN [Ratio] END DESC,
									CASE WHEN @SortField = 'Ratio' AND @Direction = 'ASC' THEN [Ratio] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {Offset, Size, SearchValue, SortField, Direction, Language, EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Add(TEntity Entity)
		{
			#region Add
			try
			{
				var Command = @"INSERT INTO [Store].[InventoryVoucherSpecificationTypeEnum]
												(
													[Version],
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[Ratio]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@Version,
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@Ratio
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
				var Command = @"UPDATE [Store].[InventoryVoucherSpecificationTypeEnum] SET
									[Version] = @Version,
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[Ratio] = @Ratio
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
				var Command = @"Delete [Store].[InventoryVoucherSpecificationTypeEnum] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
