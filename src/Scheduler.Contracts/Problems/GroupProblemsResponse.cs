using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Contracts.Problems;

public record GroupProblemsResponse(
    int Count,
    Guid GroupId,
    List<ProblemResponse> Tasks
);
