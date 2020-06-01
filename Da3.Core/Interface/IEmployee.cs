using System.Collections.Generic;
using Da3.Core.Entities;

namespace Da3.Core.Interface
{
    public interface IEmployee : IUser
    {
        /// <summary>
        /// Apply for a job
        /// </summary>
        /// <param name="job"></param>
        void Apply(Job job);


        /// <summary>
        /// publish an review
        /// </summary>
        /// <param name="review"></param>
        void Review(Review review);

        /// <summary>
        /// Search with search condition
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<Job> Search(Search search);
    }
}