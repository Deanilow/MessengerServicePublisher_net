using MessengerServicePublisher.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class MessagesRepository : BaseRepository, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext context) : base(context)
        {
        }
        private readonly int _commantTimeout = 8000000;
        private string getConnectionString
        {
            get
            {
                return _dbContext.Database.GetDbConnection().ConnectionString;
            }
        }
        //public async Task<List<MessagesPreviews>> GetMessagesBidassoaBd(string definition)
        //{
        //    using (SqlConnection dbConnection = new SqlConnection(getConnectionString))
        //    {
        //        dbConnection.Open();
        //        var result = await dbConnection.QueryAsync<MessagesPreviews>(
        //            "[dbo].[GetAcudaUbicacionFilter]",
        //            new
        //            {
        //                definition
        //            },
        //            commandType: CommandType.StoredProcedure, commandTimeout: _commantTimeout
        //        );

        //        return result.ToList();
        //    }
        //}
    }
}
