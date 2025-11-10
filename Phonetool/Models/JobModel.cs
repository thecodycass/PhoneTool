using Phonetool.Data;
using Phonetool.Entities;

namespace Phonetool.Models;

public class JobModel(AppDbContext database)
{
    public void Add(Job job)
    {
        var existingJob = database.Jobs.Find(job.JobId);
        if (existingJob != null) return;

        job.DateCreated = DateTime.UtcNow;
        job.DateModified = DateTime.UtcNow;

        database.Jobs.Add(job);
        database.SaveChanges();
    }

    public void Update(int jobId, Job job)
    {
        var jobToUpdate = database.Jobs.Find(jobId);
        if (jobToUpdate == null) return;

        jobToUpdate.Title = job.Title;
        jobToUpdate.JobLevel = job.JobLevel;
        jobToUpdate.DateModified = DateTime.UtcNow;

        database.Jobs.Update(jobToUpdate);
        database.SaveChanges();
    }

    public void Delete(int jobId)
    {
        var jobToDelete = database.Jobs.Find(jobId);
        if (jobToDelete == null) return;
        database.Jobs.Remove(jobToDelete);
        database.SaveChanges();
    }
    
    public Job? GetJobByJobId(int jobId) => database.Jobs.Find(jobId);
    public Job? GetJobByTitle(string title) => database.Jobs.Find(title);
}
