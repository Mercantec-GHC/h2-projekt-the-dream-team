using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Commands
{
    public class CreateGuestCommand
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
