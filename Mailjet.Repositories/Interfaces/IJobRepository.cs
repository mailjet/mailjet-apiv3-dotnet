using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.DataContracts.Job;

namespace Mailjet.Repositories.Interfaces
{
    public interface IJobRepository
    {
        JobDataContract CreateMergeContactsIntoListJob<T>(long contactslistId, IList<ContactBasicDataContract<T>> contacts)
        where T : class, new();
        JobDataContract ReadContactListJobStatus(long jobId, long contactslistId);
    }
}