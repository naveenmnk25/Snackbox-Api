using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models;

public partial class User
{
	public int Id { get; set; }

    public string? Username { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public string? UserRole { get; set; }
}
