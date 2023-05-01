using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.ToDoAggregate.Records;
public record ToDoCompleteDeleteRecord(Guid UserId, Guid ToDoId);
