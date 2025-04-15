using System.ComponentModel.DataAnnotations;

public class UpdateUserDTO
{
    
    public bool ModifyEmail { get; set; }

   
    public bool ModifyPassword { get; set; }

    [EmailAddress(ErrorMessage = "�rv�nytelen email c�m.")]
    public string? NewEmail { get; set; }

    public string? NewPassword { get; set; }
}
