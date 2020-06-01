using System.Collections.Generic;
using Da3.Core.Entities;

namespace Da3.Core.Interface
{
    interface IEmployer : IUser
    {
        /// <summary>
        /// Publish a job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Job Publish(Job job);
        
        
        /// <summary>
        /// Mark a job finish, it still display on system,
        /// but can't be searched.
        /// an applied employee can get info from this job
        /// </summary>
        /// <param name="job"></param>
        void MarkFinish(Job job);

        
        /// <summary>
        /// list the jobs published and its info
        /// enrich by aggregator module.
        /// </summary>
        /// <returns></returns>
        List<Job> ListJob();
        
        
        /// <summary>
        /// list the reviews published and its info
        /// enrich by aggregator module.
        /// </summary>
        /// <returns></returns>
        List<Review> ListReview();

        /// <summary>
        /// Apply employee for job
        /// </summary>
        /// <param name="apply"></param>
        void Approve(Apply apply);

    }
}