using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace schedulingQuartz
{
    public class SQJobMetadata
    {
        public Guid JobId { get; set; }
        public Type JobType { get; set; }
        public string JobName { get; }
        public string CronExpression { get; }
        public SQJobMetadata(Guid id, Type jobType, string jobName, string cronExpression) {
            JobId = id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
