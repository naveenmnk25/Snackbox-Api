using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackboxAPI.Models;

[NotMapped]
[Keyless]

public partial class QueryResult
{
    public string? JsonResult { get; set; }
}
