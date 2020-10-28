using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Config
{
    public class UrlsConfig
    {
        public class JobsOperations
        {
            public static string GetAllJobs() => $"/api/job";

            public static string GetJobById(int id) => $"/api/job/{id}";
        }

        public class ApplyingOperations
        {
            public static string GetAllApplies() => $"/api/apply";

            public static string GetApplyById(int id) => $"/api/apply/{id}";
        }

        public string Applying { get; set; }

        public string Jobs { get; set; }

        public string Identity { get; set; }
    }
}
