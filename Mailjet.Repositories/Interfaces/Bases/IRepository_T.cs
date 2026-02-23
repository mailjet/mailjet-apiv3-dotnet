using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepository<T, TKey>: IRepositoryCreate<T>, IRepositoryRead<T, TKey>, IRepositoryUpdate<T>, IRepositoryDelete<T, TKey>, IRepositoryList<T, PagingRequestBaseDataContract>
    {

    }

}