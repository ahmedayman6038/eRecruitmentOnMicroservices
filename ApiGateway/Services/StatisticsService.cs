using ApiGateway.Interfaces;
using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IJobsService _jobsService;
        private readonly IApplyingService _applyingService;

        public StatisticsService(IJobsService jobsService, IApplyingService applyingService)
        {
            _jobsService = jobsService;
            _applyingService = applyingService;
        }

        public async Task<Response<JobsStatisticsViewModel>> GetTopJobApplied()
        {
            var jobs = await _jobsService.GetAll();
            var applies = await _applyingService.GetAll();
            var appliedJobsCount = (from apply in applies
                                    group apply by apply.JobId into newGroup
                                    select new
                                    {
                                        JobId = newGroup.First().JobId,
                                        Count = newGroup.Count()
                                    }).OrderByDescending(a => a.Count).Take(5);
            var jobStatisticsVm = new JobsStatisticsViewModel();
            jobStatisticsVm.JobsName = new List<string>();
            jobStatisticsVm.ApplierCount = new List<int>();
            foreach (var item in appliedJobsCount)
            {
                var jobName = jobs.Where(j => j.Id == item.JobId).First().Name;
                jobStatisticsVm.JobsName.Add(jobName);
                jobStatisticsVm.ApplierCount.Add(item.Count);
            }
            return new Response<JobsStatisticsViewModel>(jobStatisticsVm);
        }
    }
}
