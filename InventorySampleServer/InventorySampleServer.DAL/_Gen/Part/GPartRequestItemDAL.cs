using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Part
{
	public class GPartRequestItemDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GPartRequestItemDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[P].[Title] [PartTitle],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField],
									CAST(1 AS BIT) Editable
								FROM
									[Part].[PartRequestItem] [PRI]
									INNER JOIN [Part].[PartRequest] [PR] ON [PR].[Id] = [PRI].[PartRequestId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PRI].[PartId]
								WHERE
									[PRI].[Id] = @Id ";

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
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField]
								FROM
									[Part].[PartRequestItem] [PRI] 
								WHERE
									[PRI].[Id] = @Id ";

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
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[P].[Title] [PartTitle],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequestItem] [PRI]
									INNER JOIN [Part].[PartRequest] [PR] ON [PR].[Id] = [PRI].[PartRequestId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PRI].[PartId]";

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
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[P].[Title] [PartTitle],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequestItem] [PRI]
									INNER JOIN [Part].[PartRequest] [PR] ON [PR].[Id] = [PRI].[PartRequestId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PRI].[PartId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PRI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [PRI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [PRI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [PRI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [PRI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PRI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PRI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PRI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PRI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PRI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PRI].[JsonField] END ASC
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
				var Command = @"INSERT INTO [Part].[PartRequestItem]
												(
													[CreatedBy],
													[CreatedDateTime],
													[PartRequestId],
													[PartId],
													[Value1],
													[Value2],
													[Comment],
													[SystemComment],
													[JsonField]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@PartRequestId,
													@PartId,
													@Value1,
													@Value2,
													@Comment,
													@SystemComment,
													@JsonField
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
				var Command = @"UPDATE [Part].[PartRequestItem] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[PartRequestId] = @PartRequestId,
									[PartId] = @PartId,
									[Value1] = @Value1,
									[Value2] = @Value2,
									[Comment] = @Comment,
									[SystemComment] = @SystemComment,
									[JsonField] = @JsonField
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
				var Command = @"Delete [Part].[PartRequestItem] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByPartRequestId(int PartRequestId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByPartRequestId
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
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[P].[Title] [PartTitle],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequestItem] [PRI]
									INNER JOIN [Part].[PartRequest] [PR] ON [PR].[Id] = [PRI].[PartRequestId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PRI].[PartId]
								WHERE
									[PRI].[PartRequestId] = @PartRequestId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PRI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [PRI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [PRI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [PRI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [PRI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PRI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PRI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PRI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PRI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PRI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PRI].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {PartRequestId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByPartId(int PartId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByPartId
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
									[PRI].[Id],
									[PRI].[Version],
									[PRI].[CreatedBy],
									[PRI].[CreatedDateTime],
									[PRI].[UpdatedBy],
									[PRI].[UpdatedDateTime],
									[PRI].[PartRequestId],
									[P].[Title] [PartTitle],
									[PRI].[PartId],
									[PRI].[Value1],
									[PRI].[Value2],
									[PRI].[Comment],
									[PRI].[SystemComment],
									[PRI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequestItem] [PRI]
									INNER JOIN [Part].[PartRequest] [PR] ON [PR].[Id] = [PRI].[PartRequestId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PRI].[PartId]
								WHERE
									[PRI].[PartId] = @PartId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PRI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PRI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [PRI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [PRI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [PRI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [PRI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PRI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PRI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PRI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PRI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PRI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PRI].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {PartId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartRequestItemPartRequestList(bool? EditMode = null)
		{
			#region GetPartRequestItemPartRequestList
			try
			{
				var Command = @"SELECT
									[PR].[Id],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequest] [PR] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartRequestItemPartList(bool? EditMode = null)
		{
			#region GetPartRequestItemPartList
			try
			{
				var Command = @"SELECT
									[P].[Id],
									[P].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
