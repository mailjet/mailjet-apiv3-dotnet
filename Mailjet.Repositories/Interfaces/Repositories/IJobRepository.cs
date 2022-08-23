using Mailjet.Repositories.Models.DataContracts.Contact;
using Mailjet.Repositories.Models.DataContracts.Job;

namespace Mailjet.Repositories.Interfaces.Repositories
{
    public interface IJobRepository
    {
        JobDataContract CreateMergeContactsIntoListJob<T>(Int64 contactslistId, IList<ContactBasicDataContract<T>> contacts)
        where T : class, new();
        JobDataContract ReadContactListJobStatus(long jobId, long contactslistId);
    }
}